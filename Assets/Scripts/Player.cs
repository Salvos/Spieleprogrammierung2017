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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rigid_body.AddForce(movement * playerSpeed * Time.deltaTime);

        if(transform.position.y < bottomLevel)
        {
            Debug.Log("Leider verloren");
            Debug.Log("Neuer Versuch!");
            GameObject.FindObjectOfType<Spawner>().spawn();
            Destroy(gameObject);
        }
    }
}
