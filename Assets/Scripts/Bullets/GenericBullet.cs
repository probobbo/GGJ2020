using System;
using Statue;
using UnityEngine;

public class GenericBullet : Bullet
{
    protected override void CollideWithThings(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;
        Vector3 impulse;
        if (statuePiece.IsOnTheFloor)
            impulse = new Vector3(other.impulse.x, -other.impulse.y, other.impulse.z);
        else
            impulse = new Vector3(other.impulse.x, other.impulse.y, other.impulse.z);
        statuePiece.ApplyForce(impulse * strength);
        base.CollideWithThings(other);
    }
}
