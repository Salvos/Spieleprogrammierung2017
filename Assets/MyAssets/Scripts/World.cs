﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(-100, 0)]
    [Header("Minimalhöhe / Player death")]
    public float bottomLevel = -1;

    [Range(0, 10)]
    [Header("Respawn nach Tod")]
    public float respawnAfterDeath = 2;

    [Range(-100, 0)]
    [Header("Respawn nach Finish")]
    public float respawnAfterFinish = 5;

    private Spawner Spawner;
    private Player player;
    private MyCamera mycamera;


    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// gets the Spawner object, trigger the animation and spawns a player
    /// </summary>
    void Start () {
        Spawner = GameObject.FindObjectOfType<Spawner>();
        mycamera = GetComponent<MyCamera>();
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

        Spawner.spawnPlayer();
    }

    /// <summary>
    /// Destroys the player & notificate the camera
    /// </summary>
    private void destroyPlayer()
    {
        Destroy(player.gameObject);
        mycamera.SetPlayer(null);
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
        mycamera.SetPlayer(player);
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
        destroyPlayer();

        // [OPTIONAL] respawn the player
        Invoke("respawnPlayer", respawnAfterDeath);
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
        destroyPlayer();

        // [OPTIONAL] respawn the player
        Invoke("respawnPlayer", respawnAfterFinish);
    }
}