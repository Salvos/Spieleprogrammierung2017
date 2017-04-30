using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Range(1, 1000)]
    [Header("Geschwindigkeit")]
    public float playerSpeed = 100;

    [Header("Spielerbewegung erlaubt?")]
    public bool movingEnabled = true;

    private World weltScript;

    Rigidbody rigid_body;

    // Use this for initialization
    void Start()
    {
        // get the worldscript & the rigidbody
        weltScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<World>();
        rigid_body = GetComponent<Rigidbody>();

        // register this player
        weltScript.registerPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") *10, Input.GetAxis("Vertical"));
        if (transform.position.y > 1)
            movement.y = 0;

        if(movingEnabled)
        {
            rigid_body.AddForce(movement * playerSpeed * Time.deltaTime);
        } else
        {
            rigid_body.velocity = Vector3.zero;
        }


        if(transform.position.y < weltScript.bottomLevel)
        {
            weltScript.playerDeath();
            Destroy(gameObject);
        }
    }
}
