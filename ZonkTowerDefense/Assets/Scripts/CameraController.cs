using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool allowMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;

    //We add Scroll and Pan limits so the player can't go very far away from the map.
    [Header("Scroll Limits")]
    public float lowerLimit = 10f;
    public float upperLimit = 80f;

    [Header("Pan Limits")]
    public float forwardLimit = 0f;
    public float backLimit = -100f;
    public float rightLimit = 100f;
    public float leftLimit = 0f;

    // Update is called once per frame
    void Update()
    {
        //Esc is a toggle that allows player camera movement.
        if (Input.GetKeyDown(KeyCode.Escape)) allowMovement = !allowMovement;

        if (!allowMovement) return;

        //Key and mouse camera controls.
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness){
            if (transform.position.z >= forwardLimit) return;
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness){
            if (transform.position.z <= backLimit) return;
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness){
            if (transform.position.x >= rightLimit) return;
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness){
            if (transform.position.x <= leftLimit) return;
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }


        //Mouse scroll controls.
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        Vector3 position = transform.position;
        position.y -= 1000 * scroll * scrollSpeed * Time.deltaTime;
        position.y = Mathf.Clamp(position.y, lowerLimit, upperLimit);
        transform.position = position;
    }
}
