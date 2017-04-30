using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(-100, 0)]
    [Header("Minimalhöhe / Player death")]
    public float bottomLevel = -1;

    private Spawner Spawner;
    private Player player;


    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// gets the Spawner object, trigger the animation and spawns a player
    /// </summary>
    void Start () {
        Spawner = GameObject.FindObjectOfType<Spawner>();
        Spawner.spawnPlayer();
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// respawns the player
    /// gets triggered by playerDeath- and playerFinish-method
    /// later by the checkpoint object(?)
    /// </summary>
    private void respawnPlayer()
    {
        // Hole den letzten Checkpoint und spawne den Spieler DORT!

        Debug.Log("Ein neuer Versuch :).");
        Spawner.spawnPlayer();
    }



    ///==================================///
    ///==========PUBLIC METHODS==========///
    ///==================================///

    /// <summary>
    /// register the playerobject for this script
    /// gets triggered by the Playerscript ("Start"-method)
    /// </summary>
    /// <param name="player">Playerobject</param>
    public void registerPlayer(Player player)
    {
        this.player = player;
    }

    /// <summary>
    /// method defines what happens if the player dies
    /// - print to console
    /// - respawn the player
    /// </summary>
    public void playerDeath()
    {
        // print to console && destroy the player
        Debug.Log("Leider verloren");
        Destroy(player.gameObject);

        // [OPTIONAL] respawn the player
        Invoke("respawnPlayer", 5);
    }

    /// <summary>
    /// method defines what happens if the player reach the finish
    /// - disable moving and print to console
    /// - destroy the player
    /// - respawn the player
    /// </summary>
    public void playerFinish()
    {
        // disable moving and print to console
        player.GetComponent<Player>().movingEnabled = false;
        Debug.Log("Ziel erreicht");

        // [OPTIONAL] destroy player
        Destroy(player.gameObject);
        Debug.Log("Neustart in 5 Sekunden");

        // [OPTIONAL] respawn the player
        Invoke("respawnPlayer", 5);
    }
}