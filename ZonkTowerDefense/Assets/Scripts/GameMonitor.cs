using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonitor : MonoBehaviour
{
    private bool gameEnded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;
        
        if (PlayerStats.Lives <= 0){
            EndGame();
        }
    }

    private void EndGame(){
        gameEnded = true;
        //game over
    }
}
