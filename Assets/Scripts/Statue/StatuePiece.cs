using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Statue
{
    [RequireComponent(typeof(Rigidbody))]
    public class StatuePiece : MonoBehaviour
    {
        public bool IsOnTheFloor { get; private set; }
        
        [SerializeField] private bool connected;
        private string _id;
        private int _connectedPieces;

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
            if (connected)
            {
                StopPhysics();
                _id = "-";
            }

            _connectedPieces = 0;
        }

        private void Start()
        {
        }

        public void ApplyForce(Vector3 impulse)
        {
            _rb.AddForce(impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
                IsOnTheFloor = true;
            
            if(!other.transform.CompareTag("GameController") || !connected)
                return;
            EventManager.Instance.onPieceDisconnected.Invoke(_id);
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
                IsOnTheFloor = false;
        }

        public bool IsPieceConnected()
        {
            return connected;
        }

        public void UpdateConnections()
        {
            _connectedPieces++;
        }

        public string GetConnectedCount()
        {
            return _connectedPieces + "";
        }

        public string GetId()
        {
            return _id;
        }

        public void ConnectObject(Transform parent, string id)
        {
            if (connected) return;
            connected = true;
            transform.parent = parent;
            _id = id;
            EventManager.Instance.onPieceDisconnected.AddListener(DisconnectObject);
            StopPhysics();
        }

        public void StopPhysics()
        {
            _rb.velocity = Vector3.zero;
            _rb.useGravity = false;
            _rb.isKinematic = true;
        }

        public void DisconnectObject(string id)
        {
            if (!connected || !_id.Contains(id)) return;
            connected = false;
            transform.parent = null;
            _connectedPieces = 0;
            EventManager.Instance.onPieceDisconnected.RemoveListener(DisconnectObject);
        }
    }
}