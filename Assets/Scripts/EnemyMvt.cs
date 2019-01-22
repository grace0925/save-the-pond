using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Enemy))] // require components of type Enemy (the speed variable)
public class EnemyMvt : MonoBehaviour
{
    private Transform target;

    private int wavepointIndex = 0;

    private Enemy enemy; // private reference to Enemy

    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0]; // set target = the first waypoint.

    }

    void Update()
    {

        Vector3 dir = target.position - transform.position;// direction we need to point towards the target.
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);// normalize make sure there's always 
                                                                                  //same length same speed, our speed is the only speed.

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed; //reset the speed after exitting laser slowing range
    }

    void GetNextWaypoint()
    {

        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }// Destroy gameObject when the gameObject has moved to the end.

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex]; // move onto the second waypoint.
    }

    void EndPath()
    {
        Stats.lives--;
        Destroy(gameObject);
    }
}
