using System;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Statue
{
    public class ConnectionJoint : MonoBehaviour
    {
        [SerializeField] private GameObject objectToActivate;
        public bool isFixed;
        [SerializeField] private GameManager.Connection myConnection;
        private OVRGrabbable _grabbable;
        [SerializeField] private ParticleSystem disgustingParticleSystem;

        private void Awake()
        {
            _grabbable = GetComponentInParent<OVRGrabbable>();
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
                if(isFixed)
                    disgustingParticleSystem.Play();
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
                    if (_grabbable.grabbedBy != null)
                        _grabbable.grabbedBy.ForceRelease(_grabbable);
                    transform.parent.gameObject.SetActive(false);
                    EventManager.Instance.onPieceConnected.Invoke();
                }
            }
        }
    }
}