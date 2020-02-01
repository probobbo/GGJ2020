using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance;
        public float TimeToSwitchHand = 2f;
        public float crumbleDelay = 1f;
        private OVRScreenFade _screenFade;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(this);
        }
        #endregion

        private void Start()
        {
            OVRManager.fixedFoveatedRenderingLevel = OVRManager.FixedFoveatedRenderingLevel.High;
            Application.targetFrameRate = 72;
        }

        private void Update()
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                EventManager.Instance.OnHandActivation.Invoke(OVRInput.Controller.LTouch);
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                EventManager.Instance.OnHandActivation.Invoke(OVRInput.Controller.RTouch);
        }
        
        public void LoadScene(int id)
        {
            StartCoroutine(FadeAndLoadScene(id));
        }
        
        
        private IEnumerator FadeAndLoadScene(int id)
        {
            _screenFade = FindObjectOfType<OVRScreenFade>();
            if (_screenFade == null) yield break;
            _screenFade.FadeOut();
            yield return new WaitForSeconds(_screenFade.fadeTime);
            SceneManager.LoadScene(id);
        }
    }
}