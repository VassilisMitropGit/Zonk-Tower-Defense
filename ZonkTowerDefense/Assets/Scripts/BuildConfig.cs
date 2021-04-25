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
    public GameObject missileLauncherPrefab;
    private TurretBlueprint turretToBuild;

    public bool CanBuild { get { return turretToBuild != null; } }

    public void BuildTurretOn(NodeConfig node){
        if (PlayerStats.Money < turretToBuild.cost) return;
        PlayerStats.Money -= turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }
    public void SelectTurretToBuild(TurretBlueprint turret){
        turretToBuild = turret;
    }
}
