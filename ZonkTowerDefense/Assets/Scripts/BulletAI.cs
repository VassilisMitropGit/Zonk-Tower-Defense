using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAI : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
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
        //Make so every bullet properly rotates and look to the enemy target.
        transform.LookAt(target);
    }

    void HitTarget(){
        //Add a particle effect to the scene.
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, 5f);

        if (explosionRadius > 0f){
            Explode();
        }else{
            //Destroy the enemy. Temporary since enemies have no health and bullet don't have a damage variable.
            Damage(target);
        }

        //Destroy the bullet.
        Destroy(gameObject);
    }

    void Damage(Transform enemy){
        Destroy(enemy.gameObject);
    }

    void Explode(){
        //Store all the objects that are in the radius in an array.
        Collider [] objectsAffected = Physics.OverlapSphere(transform.position, explosionRadius);
        //Among the above objects, find the enemies through the enemy tag.
        foreach (Collider objectHit in objectsAffected)
        {
            if (objectHit.tag == "Enemy"){
                Damage(objectHit.transform);
            }
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}