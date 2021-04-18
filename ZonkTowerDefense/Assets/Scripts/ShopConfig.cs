using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopConfig : MonoBehaviour
{
    BuildConfig buildConfig;

    private void Start() {
        buildConfig = BuildConfig.instance;
    }
    public void PurchaseStandardTurret(){
        buildConfig.SetTurretToBuild(buildConfig.standardTurretPrefab);
    }
    public void PurchaseMissileLauncher(){
        buildConfig.SetTurretToBuild(buildConfig.missileLauncherPrefab);
    }
}
