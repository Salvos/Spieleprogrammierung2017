using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    [Range(-100, 0)]
    [Header("Minimalhöhe / Player death")]
    public float bottomLevel = -1;

    private Spawner Spawner;
    private Player player;

    void Start () {
        Spawner = GameObject.FindObjectOfType<Spawner>();

        Spawner.spawnPlayer();
    }

    private void respawnPlayer()
    {
        // Hole den letzten Checkpoint und spawne den Spieler DORT!

        Debug.Log("Ein neuer Versuch :).");
        Spawner.spawnPlayer();
    }

    public void registerPlayer(Player player)
    {
        this.player = player;
    }

    public void playerDeath()
    {
        Debug.Log("Leider verloren");
        Invoke("respawnPlayer", 4);
    }

    public void playerFinish()
    {
        player.GetComponent<Player>().movingEnabled = false;
        Debug.Log("Ziel erreicht");
    }

}
