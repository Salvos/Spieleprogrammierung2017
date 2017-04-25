using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    public GameObject playerobject;
    private Animator animator;
    public GameObject prePlayer;
    private Color preColor = new Color(1, 1, 1);

	// Use this for initialization
	void Start () {
        //spawn();
        animator = GetComponent<Animator>();

    }
	
    public void spawn()
    {
        GameObject player = GameObject.Instantiate(playerobject, transform.position + Vector3.down, Quaternion.identity);
        player.GetComponent<MeshRenderer>().material.color = preColor;
    }

    public void respawn()
    {
        animator.SetTrigger("respawn");

        MeshRenderer mr = prePlayer.GetComponent<MeshRenderer>();
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        mr.material.color = new Color(r, g, b);
        preColor = new Color(r, g, b);
    }
}
