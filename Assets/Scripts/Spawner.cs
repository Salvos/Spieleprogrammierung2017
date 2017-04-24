using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public GameObject playerobject;

	// Use this for initialization
	void Start () {
        spawn();
	}
	
    public void spawn()
    {
        GameObject.Instantiate(playerobject, transform.position + Vector3.up, Quaternion.identity);
    }

}
