
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int damage = 50;
    public float explosionRadius = 0f;
    public GameObject boomEffect;

    public void Chase(Transform _target) {
        target = _target;
    } //target is set to the target of the turret in Turret.cs

    void Update()
    {
        if (target == null) {
            Destroy(gameObject);
            return; // return because Destroy() takes awhile
        }

        Vector3 dir = target.position - transform.position; // direction the bullet needs to travel in
        float distance = speed * Time.deltaTime; // distance between bullet and the enemy

        if (dir.magnitude <= distance) { // chekcs if bullet hits the enemy 
            HitEnemy();
            return;
        }

        // move the bullet more if nothing hits
        transform.Translate(dir.normalized * distance, Space.World); // move relative to the world space not local space
                                                                     // normalized becasue we want contant speed
        transform.LookAt(target); // make bullet point towards the enemy
    }

    void HitEnemy() {
        // Remember to destroy the gameobject after instantiating it
        GameObject effect = (GameObject)Instantiate(boomEffect, transform.position, transform.rotation);
        Destroy(effect, 2f); // destroy after 2 seconds

        if (explosionRadius > 0f) { // if radius is not 0, cause exploding area damage
            Explode();
        }
        else {
            Damage(target); // else only damage the single target
        }

        // destroy the bullet
        Destroy(gameObject);
    }

    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius); // check for all colliders with the sphere that is sent out 
                                                                                           // gets an array of all colliders that are in the sphere
        foreach (Collider collider in colliders) { // for each things we have hit, area damage
            if (collider.tag == "Enemy") { // if the bullet hits an enemy
                Damage(collider.transform);
            }

        }
    }

    void Damage(Transform enemy) { // damage/ destroy one single enemy

        Enemy e = enemy.GetComponent<Enemy>(); // get components of type Enemy and store in a variable e
        if (e != null) {
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmoSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
