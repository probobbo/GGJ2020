using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Statue
{[RequireComponent(typeof(Rigidbody))]
    public class StatuePiece : MonoBehaviour
    {
        [SerializeField]
        private bool connected;

        private Rigidbody _rb;

        private List<ConnectionJoint> _joints;

        //initialize joint list and destroy object if none are found
        private void Awake()
        {
            _joints = new List<ConnectionJoint>();
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i).GetComponent<ConnectionJoint>();
                if (child != null)
                {
                    _joints.Add(child);
                }
            }

            if (_joints.Count <= 0)
                Destroy(gameObject);
            _rb = GetComponent<Rigidbody>();
        }

        public bool IsPieceConnected()
        {
            return connected;
        }

        public void ConnectObject(Transform parent)
        {
            if (connected) return;
            connected = true;
            transform.parent = parent;
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }

        public void DisconnectObject()
        {
            if (!connected) return;
            connected = false;
            transform.parent = null;
        }
    }
}