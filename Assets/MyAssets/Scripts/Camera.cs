using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Header("Soll die Kamera folgen?")]
    public bool staticCamera = true;

    private Player player;
    private Vector3 startPosition;
    private Vector3 offset;

    private World worldScript;


    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets the startposition of the camera
    /// and get the worldScript
    /// </summary>
    void Start()
    {
        startPosition = transform.position;
        worldScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<World>();
    }

    /// <summary>
    /// if fixedCamera == true -> do nothing
    /// if fixedCamera == false -> follow the player
    /// </summary>
    void Update()
    {
        if (player != null && staticCamera == false)
            transform.position = player.transform.position + offset;
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// change the camera mode (static / follow)
    /// if triggered -> make a transition!
    /// </summary>
    private void toggleCamera()
    {
        if(staticCamera == true)
        {


        } else
        {

        }

        staticCamera = !staticCamera;
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