using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeConfig : MonoBehaviour
{
    public Vector3 positionOffset;
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    private GameObject turret;
    private void OnMouseEnter() {
        rend.material.color = hoverColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        if (turret != null){
            //display relevant message on screen
            return;
        }

        //Build a turret.
        GameObject turretToBuild = BuildConfig.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
