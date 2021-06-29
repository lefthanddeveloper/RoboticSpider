using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RoboticSpider
{
    public class CameraFollow : MonoBehaviour
    {
        public delegate void CameraSwitchHandler(CameraState newState);
        public event CameraSwitchHandler onCameraSwitched;
        public enum CameraState { Standard, POV }
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
        private float cameraStateSwitchSpeed = 10.0f;
        private Player player;
        private Transform playerTr;
        // private Vector3 offset;
        private Vector3 originalRotation;

        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            playerTr = player.transform;
            // offset = playerTr.position - transform.position;
            originalRotation = transform.eulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            if (cameraState == CameraState.Standard)
            {
                // transform.position = Vector3.Lerp(transform.position, playerTr.position -offset, followSpeed * Time.deltaTime);
            }
            else
            {

            }

            ToggleCameraState();
        }

        private void ToggleCameraState()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (cameraState == CameraState.Standard)
                {
                    cameraState = CameraState.POV;
                }
                else if (cameraState == CameraState.POV)
                {
                    cameraState = CameraState.Standard;
                }
            }
        }

        private void OnCameraStateChanged(CameraState newState)
        {
            if (newState == CameraState.POV)
            {
                if (!isSwitching)
                {
                    StartCoroutine(Switch(player.PovTr, () =>
                    {
                        onCameraSwitched?.Invoke(newState);
                        isSwitching = false;
                    }));
                }
            }
            else if (newState == CameraState.Standard)
            {
                if (!isSwitching)
                {
                    StartCoroutine(Switch(player.StandardTr, () =>
                    {
                        onCameraSwitched?.Invoke(newState);
                        isSwitching = false;
                    }));
                }
            }
        }

        private bool isSwitching;
        IEnumerator Switch(Transform newCameraState, Action completed)
        {
            isSwitching = true;
            float distance = float.MaxValue;

            while (distance > 0.05f)
            {
                yield return null;
                transform.position = Vector3.Lerp(transform.position, newCameraState.position, cameraStateSwitchSpeed * Time.deltaTime);
                distance = Mathf.Abs(Vector3.Distance(transform.position, newCameraState.position));
            }

            transform.SetParent(newCameraState);
            transform.localPosition = Vector3.zero;
            transform.localEulerAngles = Vector3.zero;

            // onCameraSwitched?.Invoke(newCameraState);

            completed?.Invoke();

        }
    }
}
