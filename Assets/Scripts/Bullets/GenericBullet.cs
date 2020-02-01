using System;
using Statue;
using UnityEngine;

public class GenericBullet : MonoBehaviour
{
    public float strength = 1f;
    protected void OnCollisionEnter(Collision other)
    {
        CollideWithThings(other);
    }

    protected virtual void CollideWithThings(Collision other)
    {
        var statuePiece = other.gameObject.GetComponent<StatuePiece>();
        if (statuePiece == null) return;

        if (!statuePiece.IsOnTheFloor) return;
        var impulse = new Vector3(other.impulse.x, -other.impulse.y, other.impulse.z);
        statuePiece.ApplyForce(impulse * strength);
    }
}
