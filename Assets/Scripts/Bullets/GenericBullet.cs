using System;
using Statue;
using UnityEngine;

public class GenericBullet : Bullet
{
    protected override void CollideWithThings(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece != null)
        {
            statuePiece.ApplyForce((Vector3.up * strength) + transform.forward);
        }
        base.CollideWithThings(other);
    }
}