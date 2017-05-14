﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(1, 1000)]
    [Header("Geschwindigkeit")]
    public float playerSpeed = 125;

    [Range(1, 1000)]
    [Header("Jump-Boost")]
    public float jumpBoost = 100;

    [Header("Spielerbewegung erlaubt?")]
    public bool movingEnabled = true;

    private GameController gameController;
    private Rigidbody rigid_body;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// initialization for the worldscript
    /// get it from the main camera object
    /// also register the player in the worldscript
    /// </summary>
    void Start()
    {
        // get the gamecontroller & the rigidbody
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rigid_body = GetComponent<Rigidbody>();

        // register this player
        gameController.registerPlayer(this);
    }

    /// <summary>
    /// controls the player movement 
    /// & checks the position of the player object
    /// </summary>
    void Update()
    {
        playerMoving();
        positionCheck();
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// controls the player-movements
    /// - also jumping
    /// if moving is Disabled -> force the playerobject to stop
    /// </summary>
    private void playerMoving()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, (float) 0.5))
        {
            if(hit.collider.tag == "Plane")
            {
                movement.y = Input.GetAxis("Jump") * jumpBoost;
            }
        }

        if (movingEnabled)
        {
            rigid_body.AddForce(movement * playerSpeed * Time.deltaTime);
        }
        else
        {
            rigid_body.velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// checks the position of the player
    /// if it below the bottomLevel of the worldScript -> kill the player!
    /// </summary>
    private void positionCheck()
    {
        if (transform.position.y < gameController.bottomLevel)
        {
            gameController.playerDeath();
        }
    }

}
