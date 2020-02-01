using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : RoboHand
{
    public GameObject firePoint;
    public GameObject bullet;
    public float bulletSpeed = 10f;
    public override void ActivateHand()
    {
        var bull = Instantiate(bullet, firePoint.transform.position,firePoint.transform.rotation);
        bull.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed);
        remainingUsages--;
        base.ActivateHand();
    }
}
