using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(1, 1000)]
    [Header("Geschwindigkeit")]
    public float playerSpeed = 100;

    [Header("Spielerbewegung erlaubt?")]
    public bool movingEnabled = true;

    private World worldScript;
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
        // get the worldscript & the rigidbody
        worldScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<World>();
        rigid_body = GetComponent<Rigidbody>();

        // register this player
        worldScript.registerPlayer(this);
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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") * 10, Input.GetAxis("Vertical"));
        if (transform.position.y > 1)
            movement.y = 0;

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
        if (transform.position.y < worldScript.bottomLevel)
        {
            worldScript.playerDeath();
        }
    }

}
