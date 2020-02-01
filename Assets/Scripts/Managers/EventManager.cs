using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Managers
{
    public class StringEvent : UnityEvent<string>
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
                onPieceDisconnected = new StringEvent();                
            }
            else
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }

        #endregion

        public UnityEvent onExperienceStart;

        public StringEvent onPieceDisconnected;
    }
}