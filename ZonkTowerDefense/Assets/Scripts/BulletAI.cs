using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    
    //Seek is called from TurretAI.cs so we can set the closest enemy as the target.
    public void Seek (Transform temp_target){
        target = temp_target;
    }

    // Update is called once per frame
    void Update()
    {
        //If we have no target, destroy the bullet.
        if (target == null){
            Destroy(gameObject);
            return;
        }
        
        //Calculate the distance to the target.
        Vector3 direction = target.position - transform.position;
        //distanceThisFrame is a variable that stores the distance that the bullet is going to travel this frame.
        float distanceThisFrame = speed * Time.deltaTime;

        //if the distance that we are going to travel this frame is greater than the distance to the enemy, that means that we are going to hit it.
        if (direction.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }

        //If we didn't hit an enemy, we continue traveling as normal.
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(){
        //Add a particle effect to the scene.
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 2f);

        //Destroy the enemy. Temporary since enemies have no health and bullet don't have a damage variable.
        Destroy(target.gameObject);

        //Destroy the bullet.
        Destroy(gameObject);
    }
}
