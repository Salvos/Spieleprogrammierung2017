using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    [Header("Zu triggerndes Object")]
    public GameObject triggerObject;

    [Range(1, 20)]
    [Header("Trigger-Delay (in sec.)")]
    public float triggerDelay = 1;




    /// <summary>
    /// triggers on collision with the player
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        // if the collision object has a player-tag -> notificate the gamecontroller
        if (col.gameObject.CompareTag("Player"))
        {
            Door doorTrigger = triggerObject.GetComponent<Door>();

            if(doorTrigger != null)
            {
                doorTrigger.trigger();
            }
        }
    }


}
