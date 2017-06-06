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



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    /// <summary>
    /// sets the startposition of the camera
    /// </summary>
    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().registerCamera(this);
    }

    /// <summary>
    /// if fixedCamera == true -> do nothing
    /// if fixedCamera == false -> follow the player
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
    /// LATER USED!
    /// change the camera mode (static / follow)
    /// if triggered -> make a transition!
    /// </summary>
    private void toggleCamera()
    {
        thirdPerson = !thirdPerson;

        if (thirdPerson == true)
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
    public void SetPlayer(Player player)
    {
        if(player != null)
            transform.position = player.transform.position;

        this.player = player;

        /*
        if (player!= null)
            offset = transform.position - player.transform.position;
            */
    }



    public Quaternion getRotation()
    {
        if (thirdPerson == true)
        {
            return transform.FindChild("Third_Person").transform.rotation;
        }
        else
        {
            return transform.FindChild("Isometric_Camera").transform.rotation;
        }
    }
}