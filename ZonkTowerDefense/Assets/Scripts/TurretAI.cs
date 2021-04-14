using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("UnitySetUp")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public float turnSpeed = 8f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        //We use UpdateTarget(a custom function) instead of Update because we don't need to do calculations every frame
        //as that would take a lot of computing power. Instead we invoke our custom funcion every 0.5s.
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    //a custom function that calculates the nearest enemy to the Turret.
    void UpdateTarget(){
        //Get a list of all the enemies that are in the scene.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //Calculate the distance between the Turret and all the enemies in the above list and determine the one closest to the turret.
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies){
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance){
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range){
            target = nearestEnemy.transform;
        } else{
            //make sure that if we didn't find an enemy we set the target to null.
            target = null;
        }
    }

    //We use the Update function in order to rotate and shoot with our Turret.
    void Update()
    {
        if (target == null) return;

        //Bellow we calculate the rotation we need to make with Quaternions. The only part we rotate is partToRotate as is defined in Unity.
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        //After the Turret is rotated, it shoots at the nearest enemy, the target.
        if (fireCountdown <= 0f){
            turretShoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void turretShoot(){
        //Add a bullet object to the scene.
        GameObject bulletObj = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //set a bullet object.
        BulletAI bullet = bulletObj.GetComponent<BulletAI>();
        
        //Seek is a funcion in BulletAI.cs with which we set the bullet target, aka. the closest enemy.
        if (bullet != null){
            bullet.Seek(target);
        }
    }

    //a custom function that shows the Turret range in Unity.
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
