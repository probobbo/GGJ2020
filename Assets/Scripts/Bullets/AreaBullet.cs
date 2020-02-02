using System;
using System.Linq;
using Statue;
using UnityEngine;

public class AreaBullet : Bullet
{
    public GameObject explosion;
    public float area = 5f;

    protected override void CollideWithThings(Collision other)
    {
        var collisions = Physics.OverlapSphere(transform.position, area);
        foreach (var collision in collisions)
        {
            var statuePiece = collision.gameObject.GetComponent<StatuePiece>();
            if (statuePiece == null) continue;
            var direction = (collision.transform.position - transform.position).normalized;
            statuePiece.ApplyForce((direction * (strength)) + Vector3.up * 2f);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        base.CollideWithThings(other);
    }
}