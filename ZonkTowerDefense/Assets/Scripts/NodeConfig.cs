using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeConfig : MonoBehaviour
{
    BuildConfig buildConfig;
    public Vector3 positionOffset;
    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    private GameObject turret;
    private void OnMouseEnter() {
        if (buildConfig.getTurretToBuild() == null) return;

        //If the player hovers over the Shop, they can't click the board.
        if (EventSystem.current.IsPointerOverGameObject()) return;

        rend.material.color = hoverColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Store the starting color of the node so when mouse exits we return it to normal.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildConfig = BuildConfig.instance;
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        //If the player hovers over the Shop, they can't click the board.
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (buildConfig.getTurretToBuild() == null) return;

        if (turret != null){
            //This means that there is already a turret on this block.
            //TODO: Display a message on screen for the above error.
            return;
        }

        //Build a turret.
        GameObject turretToBuild = buildConfig.getTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
    }
}
