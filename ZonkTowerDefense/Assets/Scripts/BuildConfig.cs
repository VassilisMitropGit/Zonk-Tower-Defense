using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildConfig : MonoBehaviour
{
    //reference to itself.
    public static BuildConfig instance;
    //reference to the build manager publicly available.
    private void Awake() {
        if (instance != null){
            Debug.LogError("More than one GameManager in the scene!");
            return;
        }
        instance = this;
    }
    public GameObject standardTurretPrefab;
    private GameObject turretToBuild;
    public GameObject getTurretToBuild(){
        return turretToBuild;
    }
    // Start is called before the first frame update
    void Start()
    {
        //for now we always build the standard turret.
        turretToBuild = standardTurretPrefab;
    }
}
