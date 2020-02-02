using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class ActivateHandEvent : UnityEvent<OVRInput.Controller>
    {
    }

    public class ImpulseEvent : UnityEvent<Vector3>
    {
    }

    public class EventManager : MonoBehaviour
    {
        #region Singleton

        public static EventManager Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                onExperienceStart = new UnityEvent();
                OnHandActivation = new ActivateHandEvent();
                onPieceDisconnected = new ImpulseEvent();
                onPieceConnected = new UnityEvent();
            }
            else
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        #endregion

        public ActivateHandEvent OnHandActivation;
        public UnityEvent onExperienceStart;
        public ImpulseEvent onPieceDisconnected;
        public UnityEvent onPieceConnected;
    }
}