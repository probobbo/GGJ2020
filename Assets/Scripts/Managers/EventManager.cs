using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public class ActivateHandEvent : UnityEvent<OVRInput.Controller> {}
    
    #region Singleton
    public static EventManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            OnHandActivation = new ActivateHandEvent();
        }
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);
    }
    #endregion

    public ActivateHandEvent OnHandActivation;
    public UnityEvent OnExperienceStart;
}
