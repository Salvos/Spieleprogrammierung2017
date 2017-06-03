using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(1, 1000)]
    [Header("Geschwindigkeit")]
    public float playerSpeed = 25;

    [Range(1, 20)]
    [Header("Distanz")]
    public float distance = 5;

    [Range(0.1f , 20)]
    [Header("PushDelay")]
    public float pushDelay = 5;

    private Player player;
    private float lastPush = 0;
    private GameController gameController;
    private Rigidbody rigid_body;
    private Vector3 startPosition;

    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    void Start () {
        startPosition = transform.position;

        // get the gamecontroller & the rigidbody
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rigid_body = GetComponent<Rigidbody>();
    }

	void Update () {
		if(player != null && (pushDelay+lastPush < Time.time) )
        {
            lastPush = Time.time;
            float playerDistance = Vector3.Distance(player.transform.position, transform.position);
            if (playerDistance <= distance)
            {
                rigid_body.AddForce((player.transform.position-transform.position) * playerSpeed);
                //transform.position = new Vector3(startPosition.x, startPosition.y + 1, startPosition.z);
            }
        }

        if (transform.position.y < gameController.bottomLevel)
        {
            Destroy(gameObject);
        }
    }


    public void setTarget(Player spieler)
    {
        player = spieler;
    }
}
