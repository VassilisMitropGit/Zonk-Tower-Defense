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
        //Store the starting color of the node so when mouse exits we return it to normal.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        if (turret != null){
            //This means that there is already a turret on this block.
            //TODO: Display a message on screen for the above error.
            return;
        }

        //Build a turret.
        GameObject turretToBuild = BuildConfig.instance.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
