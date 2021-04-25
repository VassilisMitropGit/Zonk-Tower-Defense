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
    [Header("Optional")]
    public GameObject turret;
    private void OnMouseEnter() {
        if (!buildConfig.CanBuild) return;

        //If the player hovers over the Shop, they can't click the board.
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (buildConfig.HasMoney){
            rend.material.color = hoverColor;
        } else {
            rend.material.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Store the starting color of the node so when mouse exits we return it to normal.
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildConfig = BuildConfig.instance;
    }

    public Vector3 GetBuildPosition(){
        return transform.position + positionOffset;
    }

    private void OnMouseExit() {
        rend.material.color = startColor;
    }

    private void OnMouseDown() {
        //If the player hovers over the Shop, they can't click the board.
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (!buildConfig.CanBuild) return;

        if (turret != null){
            //This means that there is already a turret on this block.
            //TODO: Display a message on screen for the above error.
            return;
        }

        buildConfig.BuildTurretOn(this);
    }
}
