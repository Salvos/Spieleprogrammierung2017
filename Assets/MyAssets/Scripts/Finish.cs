using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{

    private World weltScript;

    // Use this for initialization
    void Start()
    {
        weltScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<World>();
    }

    /// <summary>
    /// on collision with player
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            weltScript.playerFinish();
        }
    }
}
