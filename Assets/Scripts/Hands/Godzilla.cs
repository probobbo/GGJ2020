using System;
using System.Collections;
using System.Collections.Generic;
using Statue;
using UnityEngine;

public class Godzilla : RoboHand
{
    private bool activated = false;

    public override void ActivateHand()
    {
        activated = !activated;
    }

    public override void ResetHand()
    {
        base.ResetHand();
        activated = false;
    }

    private void Update()
    {
        if (activated)
        {
           if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 100f))
           {
               var statuePiece = hit.collider.gameObject.GetComponent<StatuePiece>();
               if (statuePiece == null) return;
               var direction = statuePiece.transform.position - hit.point;
               if (statuePiece.IsOnTheFloor)
                   direction = new Vector3(direction.x, -direction.y, direction.z);
               else
                   direction = new Vector3(direction.x, direction.y, direction.z);
               statuePiece.ApplyForce(direction * strength);
           }
           remainingUsages -= Time.deltaTime;
           base.ActivateHand();
        }
    }
}
