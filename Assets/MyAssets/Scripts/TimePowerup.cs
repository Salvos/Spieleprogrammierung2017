using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePowerup : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///

    [Range(1, 10)]
    [Header("Zeitbonus")]
    public float timeBonus = 5;

    /// <summary>
    /// variable for gamecontroller to notificate the script if the player enters the finish block
    /// </summary>
    private GameController gameController;



    ///==================================///
    ///==========Unity Methods==========///
    ///==================================///

    /// <summary>
    /// initialization for the gamecontroller
    /// get it from the main camera object
    /// </summary>

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    /// <summary>
    /// triggers on collision with the player
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        // if the collision object has a player-tag -> notificate the gamecontroller
        if (col.gameObject.CompareTag("Player"))
        {
            gameController.reduceTimer(timeBonus);
            Destroy(gameObject);
        }
    }
}
