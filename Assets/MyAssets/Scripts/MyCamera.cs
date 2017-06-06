using System.Collections;
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
    private Quaternion startRotation;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets the startposition of the camera-Object
    /// FIX? sets the startrotation-Object
    /// register this camera object
    /// </summary>
    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().registerCamera(this);
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    /// <summary>
    /// toggles the camera-mode (on k) 
    /// if the target is set the camera-Object should follow the target
    /// - and if the third Person is set - rotate the Third Person object (with third person camera)
    /// </summary>
    void Update()
    {
        if(player != null)
        {
            transform.position = player.transform.position;

            if(thirdPerson)
                transform.FindChild("Third_Person").Rotate(0, Input.GetAxis("Horizontal"), 0, Space.World);
        }
            
        if (Input.GetKeyUp(KeyCode.K))
        {
            toggleCamera();
        }
    }



    ///===================================///
    ///==========PRIVATE METHODS==========///
    ///===================================///

    /// <summary>
    /// change the camera mode (thirdperson / isometric)
    /// if triggered -> change the active camera
    /// </summary>
    private void toggleCamera()
    {
        thirdPerson = !thirdPerson;

        if (thirdPerson)
        {
            transform.FindChild("Isometric_Camera").gameObject.SetActive(false);
            transform.FindChild("Third_Person").gameObject.SetActive(true);
        } else
        {
            transform.FindChild("Third_Person").gameObject.SetActive(false);
            transform.FindChild("Isometric_Camera").gameObject.SetActive(true);
        }
    }



    ///==================================///
    ///==========PUBLIC METHODS==========///
    ///==================================///

    /// <summary>
    /// sets the playerobject and the camera position
    /// </summary>
    public void SetPlayer(Player spieler)
    {
        player = spieler;

        if (player != null)
        {
            transform.position = player.transform.position;
        } else
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
        }

        
    }

    /// <summary>
    /// returns the rotation of the Camera-Object
    /// </summary>
    public Quaternion getRotation()
    {
        if (thirdPerson)
        {
            return transform.FindChild("Third_Person").transform.rotation;
        }
        else
        {
            return transform.FindChild("Isometric_Camera").transform.rotation;
        }
    }
}