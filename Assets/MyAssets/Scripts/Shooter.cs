using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    ///==================================///
    ///==========Vars & Objects==========///
    ///==================================///

    [Header("Drehgeschwindigkeit des Kopfes")]
    [Range(1f, 200.0f)]
    public float rotationSpeed = 100.0f;

    [Header("Max. Geschossabweichung (Streuung)")]
    [Range(0.0f, 10.0f)]
    public float diversion = 1.2f;

    [Header("Delay zum nächsten Schuss (in s)")]
    [Range(0.0001f, 10.0f)]
    public float shootDelay = 1;

    [Header("Geschoss")]
    public GameObject bulletType;

    [Header("Anzahl Kugeln pro Schuss")]
    [Range(1, 100)]
    public int bulletsPerShot = 1;

    public GameObject firePoint;

    private float lastShot;

    private Player player;



    ///=================================///
    ///==========Unity Methods==========///
    ///=================================///

    private void Start()
    {
        lastShot = 0f;
    }


    void Update()
    {
        if(player != null)
        {
            updateFacing();
        
            if (lastShot+shootDelay < Time.time)
            {
                lastShot = Time.deltaTime;

                GameObject bullet = Instantiate(bulletType);
                bullet.transform.position = firePoint.transform.position;
                bullet.transform.Rotate(new Vector3(90, 0, 0), Space.Self);
                bullet.transform.rotation = firePoint.transform.rotation;
                bullet.GetComponent<Bullet>().shoot(player.transform.position, diversion);
            }

        }
    }

    private void updateFacing()
    {
        float dh = Time.deltaTime * rotationSpeed;

        Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position);
        Quaternion from = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));
        Quaternion to = Quaternion.Euler(new Vector3(0f, rotation.eulerAngles.y, 0f));

        transform.rotation = Quaternion.Slerp(from, to, dh);
    }

    public void setTarget(Player spieler)
    {
        player = spieler;
    }
}
