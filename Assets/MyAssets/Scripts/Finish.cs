using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///

    /// <summary>
    /// variable for worldscript to notificate the script if the player enters the finish block
    /// </summary>
    private World worldScript;



    ///==================================///
    ///==========Unity Methods==========///
    ///==================================///

    /// <summary>
    /// initialization for the worldscript
    /// get it from the main camera object
    /// </summary>
    void Start()
    {
        worldScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<World>();
    }

    /// <summary>
    /// triggers on collision with the player
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        // if the collision object has a player-tag -> notificate the worldscript
        if (col.gameObject.CompareTag("Player"))
        {
            worldScript.playerFinish();
        }
    }
}