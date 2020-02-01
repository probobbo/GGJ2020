using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);
    }
    #endregion

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            EventManager.Instance.OnHandActivation.Invoke(OVRInput.Controller.LTouch);
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
            EventManager.Instance.OnHandActivation.Invoke(OVRInput.Controller.RTouch);
    }
}
