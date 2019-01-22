
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target; // the targetted enemy
    public Enemy targetEm;

    [Header("Attributes")]
    public float range = 15f; // the range of a turret

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 10;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f; // how many bullets fired per second
    private float fireCountdown = 0f; // fire right away as soon as a target is detected

    [Header("Laser")]
    public bool useLaser = false;
    public int damOverTime = 10;
    public float slowPert = 0.5f;
    public LineRenderer lineRenderer;
    public ParticleSystem ImpactEffect;

    void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

    }

    void UpdateTarget() // search for the closest enemy, check if it's within range, and set target as that object
                        // not done in update for computer performance
    {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); // find eachc game object with the tag enemyTag 
                                                                            // and store them in a GameObject array named enemies
        float shortestDistance = Mathf.Infinity; // default is infinity
        GameObject nearestEnemy = null; 
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position); // get the distance between the 
                                                                                                    // the turret and the enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) // if nearestEnemy is within turret range, set as the target
        {
            target = nearestEnemy.transform;
            targetEm = nearestEnemy.GetComponent<Enemy>();
        }
        else {
            target = null;
        }
        
    }

    void Update()
    {
        if (target == null) { // no traget -> just return
            if (useLaser) {
                if (lineRenderer.enabled) {
                    lineRenderer.enabled = false;
                    ImpactEffect.Stop();
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else {
            //shoot a bullet at the fireRate whenever fireCountdown is 0 
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime; // every second, fireCountdown decreases by 1
    }

    void Laser() {

        //enemy takes more damage every second it is locked on
        targetEm.TakeDamage(damOverTime * Time.deltaTime);
        //slow enemies 
        targetEm.Slow(slowPert);


        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            ImpactEffect.Play(); // enable the laser effect particle system; Play() and Stop()
        }

        lineRenderer.SetPosition(0, firePoint.position); //drag line from position of the firepoint to position of the enemy
        lineRenderer.SetPosition(1, target.position);

        //laser effect
        Vector3 dir = firePoint.position - target.position; // direction from target to firePoint

        ImpactEffect.transform.rotation = Quaternion.LookRotation(dir); // creates a rotation that points in the direction of dir

        ImpactEffect.transform.position = target.position + dir.normalized * 0.9f; // vector math for positioning the position at the edge of the enemy instead of the middle
    }

    void LockOnTarget() {
        //target lock on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        //Lerp from current location, transform.rotation, to lookRotation as time passes by
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot() {
        //Need to do bject casting when storing gameobjects
        // set bulletGo as the instantiated bullet prefab
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>(); // find components of type Bullet on bullet object that is just instantiated

        if (bullet != null) {
            bullet.Chase(target);
        }
    }

    void OnDrawGizmosSelected() // range is drawn for the selected
    {

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, range); // draw a sphere that has center at tranform(the turret) and radius of range

    }

}
