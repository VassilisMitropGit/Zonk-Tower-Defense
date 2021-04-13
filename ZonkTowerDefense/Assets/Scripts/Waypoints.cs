using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour{
    public static Transform[] points;
    //Transform: Position, rotation and scale of an object.

    //On Awake, we create a PUBLIC array with all the waypoints so it can be used by the "Enemy" objects.
    void Awake(){
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++){
            points[i] = transform.GetChild(i);
        }
    }
}