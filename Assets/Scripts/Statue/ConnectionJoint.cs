﻿using System;
using Managers;
using UnityEngine;

namespace Statue
{
    public class ConnectionJoint : MonoBehaviour
    {
        [SerializeField] private GameObject objectToActivate;
        public bool isFixed;
        [SerializeField] private GameManager.Connection myConnection;

        private void Awake()
        {
            if (objectToActivate != null)
                objectToActivate.SetActive(false);
        }

        public GameManager.Connection GetConnection()
        {
            return myConnection;
        }

        public void ShowObject()
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Joint"))
            {
                var otherJoint = other.GetComponent<ConnectionJoint>();
                if (otherJoint != null && otherJoint.isFixed && otherJoint.GetConnection() == myConnection)
                {
                    Debug.Log("ITS GOING");
                    otherJoint.ShowObject();
                    transform.parent.gameObject.SetActive(false);
                }
            }
        }
    }
}