using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Range(1, 1000)]
    [Header("Geschwindigkeit")]
    public float playerSpeed = 100;

    [Range(-100, 0)]
    [Header("Minimalhöhe")]
    public float bottomLevel = -5;

    Rigidbody rigid_body;

    // Use this for initialization
    void Start()
    {
        rigid_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump") *10, Input.GetAxis("Vertical"));
        if (transform.position.y > 1) movement.y = 0;

        rigid_body.AddForce(movement * playerSpeed * Time.deltaTime);

        if(transform.position.y < bottomLevel)
        {
            Debug.Log("Leider verloren");
            GameObject.FindObjectOfType<Spawner>().respawn();
            Destroy(gameObject);
        }
    }
}
