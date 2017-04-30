using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    public GameObject playerobject;
    public GameObject prePlayer;

    private Animator animator;

    // Sets the Color for initialization
    private Color preColor = new Color(1, 1, 1);



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// Use this for initialization
    /// get the animator component for the animation
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// spawn component
    /// gets triggered after the animation
    /// "spawn" the player object up the object
    /// </summary>
    private void spawn()
    {
        GameObject player = GameObject.Instantiate(playerobject, transform.position + Vector3.up, Quaternion.identity);
        player.GetComponent<MeshRenderer>().material.color = preColor;
    }



    ///==================================///
    ///==========PUBLIC METHODS==========///
    ///==================================///

    /// <summary>
    /// respawns the player
    /// also changes the color
    /// </summary>
    public void spawnPlayer()
    {
        // play the animation
        animator.SetTrigger("respawn");

        // change the color (not important - can be removed later)
        MeshRenderer mr = prePlayer.GetComponent<MeshRenderer>();
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        mr.material.color = new Color(r, g, b);
        preColor = new Color(r, g, b);
    }
}