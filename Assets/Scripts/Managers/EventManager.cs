using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class ActivateHandEvent : UnityEvent<OVRInput.Controller>
    {
    }

    public class IdImpulseEvent : UnityEvent<string,Vector3>
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
                onPieceDisconnected = new IdImpulseEvent();
            }
            else
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        #endregion

        public ActivateHandEvent OnHandActivation;
        public UnityEvent onExperienceStart;
        public IdImpulseEvent onPieceDisconnected;
    }
}