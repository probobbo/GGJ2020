using System;
using UnityEngine;

namespace Statue
{
    [RequireComponent(typeof(Rigidbody))]
    public class ConnectionJoint : MonoBehaviour
    {
        private StatuePiece _parentPiece;
        
        private void Awake()
        {
            var parent = transform.parent;
            if (parent == null || parent.GetComponent<StatuePiece>()==null)
                Destroy(gameObject);
            _parentPiece = parent.GetComponent<StatuePiece>();
        }

        private bool IsParentConnected()
        {
            return _parentPiece.IsPieceConnected();
        }

        private void ConnectToParent(Transform parent,string id)
        {
            _parentPiece.ConnectObject(parent,id);
        } 

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Joint"))
            {
                var otherJoint = other.GetComponent<ConnectionJoint>();
                if (otherJoint != null && IsParentConnected() && !otherJoint.IsParentConnected())
                {
                    otherJoint.ConnectToParent(_parentPiece.transform,_parentPiece.GetId()+_parentPiece.GetConnectedCount());
                    _parentPiece.UpdateConnections();
                }
            }
        }
    }
}