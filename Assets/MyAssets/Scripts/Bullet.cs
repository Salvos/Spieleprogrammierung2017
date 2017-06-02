using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///
    [Range(1, 1000)]
    [Header("Schaden")]
    public int bulletDamage = 2;

    [Header("Geschossgeschwindigkeit")]
    [Range(1.0f, 100.0f)]
    public float speed = 10;

    [Header("Anzahl der Sekunden bis zum Despawn")]
    [Tooltip("Das Geschoss wird nach X sekunden entfernt")]
    [Range(1.0f, 60.0f)]
    public float timeToHit = 5.0f;


    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    // Use this for initialization
    void Start()
    {
        // Zerstört das Objekt nach Zeit
        Destroy(gameObject, timeToHit);
    }

    // Wenn die Kugel etwas trifft
    void OnCollisionEnter(Collision col)
    {
        Destroy(gameObject);
    }

    ///=================================///
    ///==========Public Methods=========///
    ///=================================///
    public void shoot(Vector3 direction, float diversion)
    {
        // Erschaffe eine Abweichung von max. 1(m) 
        float targetX = direction.x + Random.Range(-diversion, diversion);
        float targetY = gameObject.transform.position.y;
        float targetZ = direction.z + Random.Range(-diversion, diversion);

        // Berechne die Flugrichtung
        Vector3 dir = new Vector3(targetX, targetY, targetZ) - transform.position;

        GetComponent<Rigidbody>().velocity = dir.normalized * speed;
    }
}
