
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake ()
    {
        points = new Transform[transform.childCount];//Array of 12 points for 12 waypoints.
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }//put waypoints into points array.
    }
}
