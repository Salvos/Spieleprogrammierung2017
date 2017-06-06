﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Header("Third Person")]
    public bool thirdPerson = false;

    private Player player;
    private Vector3 startPosition;
    private Vector3 offset;
    private GameController gameController;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets the startposition of the camera
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.registerCamera(this);
    }

    /// <summary>
    /// if fixedCamera == true -> do nothing
    /// if fixedCamera == false -> follow the player
    /// </summary>
    void Update()
    {
        if (player != null && thirdPerson == false)
            transform.position = player.transform.position + offset;

        if (player != null && thirdPerson == true)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position - offset);
        }
            
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// LATER USED!
    /// change the camera mode (static / follow)
    /// if triggered -> make a transition!
    /// </summary>
    private void toggleCamera()
    {
        if(thirdPerson == true)
        {


        } else
        {

        }

        thirdPerson = !thirdPerson;
    }



    ///==================================///
    ///==========PUBLIC METHODS==========///
    ///==================================///

    /// <summary>
    /// sets the playerobject and the camera position
    /// </summary>
    public void SetPlayer(Player player)
    {
        transform.position = startPosition;
        this.player = player;

        if (player!= null)
            offset = transform.position - player.transform.position;
    }
}