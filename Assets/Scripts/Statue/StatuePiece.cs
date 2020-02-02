using System;
using System.Collections;
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

        private Rigidbody _rb;
        private ConnectionJoint _joint;

        //initialize joint list and destroy object if none are found
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _joint = transform.GetChild(0).GetComponent<ConnectionJoint>();

            if (transform.childCount <= 0)
                Destroy(gameObject);
            if (_joint == null)
                Destroy(gameObject);
        }

        public void ApplyForce(Vector3 impulse)
        {
            _rb.AddForce(impulse, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
                IsOnTheFloor = true;

            /*if (!other.transform.CompareTag("GameController") || !connected)
                return;
            EventManager.Instance.onPieceDisconnected.Invoke(_id);*/
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Floor"))
                IsOnTheFloor = false;
        }
    }
}