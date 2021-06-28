using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoboticSpider
{
    public class CameraFollow : MonoBehaviour
    {
        public static UnityEvent onCameraSwitchedToPOV;
        public enum CameraState{Standard, POV}
        private CameraState _cameraState;
        private CameraState cameraState 
        {
            get
            {
                return _cameraState;
            }
            set
            {
                _cameraState = value;
                OnCameraStateChanged(_cameraState);
                
            }
        }


        public float followSpeed = 2.0f;
        private float cameraStateSwitchSpeed = 2.0f;
        private Player player;
        private Transform playerTr;
        private Vector3 offset;
        private Vector3 originalRotation;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            playerTr = player.transform;
            offset = playerTr.position - transform.position;
            originalRotation = transform.localEulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            if(cameraState == CameraState.Standard)
            {
                transform.position = Vector3.Lerp(transform.position, playerTr.position -offset, followSpeed * Time.deltaTime);
            }
            else
            {

            }

            ToggleCameraState();
        }

        private void ToggleCameraState()
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if(cameraState == CameraState.Standard)
                {
                    cameraState = CameraState.POV;
                }
                else if(cameraState == CameraState.POV)
                {
                    cameraState = CameraState.Standard;
                }
            }
        }

        private void OnCameraStateChanged(CameraState newState)
        {
            if(newState == CameraState.POV)
            {
                if(!isSwitching)
                {
                    StartCoroutine(ChangeToPOV());
                }
            }
            else if(newState == CameraState.Standard)
            {
                transform.parent= null;
                originalRotation = transform.localEulerAngles;
            }
        }

        private bool isSwitching;
        IEnumerator ChangeToPOV()
        {
            isSwitching = true;
            float distance = float.MaxValue;
            while(distance <= 0.00001f)
            {
                transform.position = Vector3.Lerp(transform.position, player.PovTr.position, cameraStateSwitchSpeed * Time.deltaTime);
                yield return null;
            }

            transform.SetParent(player.PovTr);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;

            onCameraSwitchedToPOV?.Invoke();
            isSwitching = false;
        }
    }
}
