using System;
using System.Linq;
using Statue;
using UnityEngine;

public class AreaBullet : Bullet
{
    public float area = 5f;
    protected override void CollideWithThings(Collision other)
    {
        var collisions = Physics.OverlapSphere(transform.position, area);
        foreach (var collision in collisions)
        {
            var statuePiece = collision.gameObject.GetComponentInParent<StatuePiece>();
            if (statuePiece == null) continue;
            var direction = collision.transform.position - transform.position;
            var impulse = Vector3.Distance(collision.transform.position,transform.position);
            statuePiece.ApplyForce((direction * (strength / impulse))+Vector3.up * 2f);
        }
        base.CollideWithThings(other);
    }
}
