using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class Gun : RoboHand
{
    public GameObject firePoint;
    public GameObject bullet;
    public float bulletSpeed = 10f;

    public override void ActivateHand()
    {
        var bull = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        if (roboHandType == RoboHandType.Cannon)
        {
            AudioManager.PlayOneShotAudio("event:/SOUNDFX/SFX_Cannon", gameObject);

        }
        else if (roboHandType == RoboHandType.Gun)
        {
            AudioManager.PlayOneShotAudio("event:/SOUNDFX/SFX_Gunshot", gameObject);

        }
        bull.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed, ForceMode.Force);
        remainingUsages--;
        base.ActivateHand();
    }
}
