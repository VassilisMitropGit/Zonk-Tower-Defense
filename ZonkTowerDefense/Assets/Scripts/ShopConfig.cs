using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopConfig : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    BuildConfig buildConfig;

    private void Start() {
        buildConfig = BuildConfig.instance;
    }
    public void SelectStandardTurret(){
        buildConfig.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissileLauncher(){
        buildConfig.SelectTurretToBuild(missileLauncher);
    }
}
