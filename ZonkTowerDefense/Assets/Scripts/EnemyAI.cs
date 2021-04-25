using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour{

    public int health = 100;
    public int moneyDrop = 50;

    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;

    //On Start, the "Enemy" object starts moving to the first waypoint.
    void Start(){
        target = Waypoints.points[0];
    }

    //On Update (on every frame), we check if the "Enemy" object has reached it's destination. If so, we find the next waypoint.
    void Update(){
        Vector3 dir = target.position - transform.position;
        //Time.deltaTime takes into consideration the varrying framerate of the computer so it can increase object speed accordingly.
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f){
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint(){
        if (wavepointIndex >= Waypoints.points.Length - 1){
            EndPath();
            //return is used so that index can't go out of bounds before the Destroy function is done executing.
            return;
        }
        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath(){
        Destroy(gameObject);
        PlayerStats.Lives--;
    }

    public void takeDamage (int dmgAmount){
        health -= dmgAmount;

        if (health <= 0){
            Die();
        }
    }

    private void Die(){
        PlayerStats.Money += moneyDrop;
        Destroy(gameObject);
    }
}
