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
        private float cameraStateSwitchSpeed = 15.0f;
        private Player player;
        private Transform playerTr;
        private Vector3 localOffset;
        private Vector3 localRotation;

        public Transform cameraPivotTr;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Player>();
            playerTr = player.transform;

            localOffset = transform.localPosition;
            localRotation = transform.localEulerAngles;
        }

        // Update is called once per frame
        void Update()
        {
            if (cameraState == CameraState.Standard)
            {
                //transform.position = Vector3.Lerp(transform.position, playerTr.position - offset, followSpeed * Time.deltaTime);
                cameraPivotTr.position = Vector3.Lerp(cameraPivotTr.position, playerTr.position, followSpeed * Time.deltaTime);

                cameraPivotTr.rotation = Quaternion.Lerp(cameraPivotTr.rotation, playerTr.rotation, followSpeed * Time.deltaTime);
                
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
                    StartCoroutine(Switch(newState, () =>
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
					StartCoroutine(Switch(newState, () =>
					{
						onCameraSwitched?.Invoke(newState);
						isSwitching = false;
					}));



				}
            }
        }

        private bool isSwitching;
        IEnumerator Switch(CameraState newCameraState, Action completed)
        {
			yield return null;
			isSwitching = true;
            if(newCameraState == CameraState.Standard)
			{
                transform.SetParent(cameraPivotTr);
                transform.localPosition = localOffset;
                transform.localEulerAngles = localRotation;
            }
            else if(newCameraState == CameraState.POV)
			{
                float distance = float.MaxValue;
                while (distance > 0.02f)
                {
                    yield return null;
                    transform.position = Vector3.Lerp(transform.position, player.PovTr.position, cameraStateSwitchSpeed * Time.deltaTime);
                    distance = Mathf.Abs(Vector3.Distance(transform.position, player.PovTr.position));
                }
                transform.SetParent(player.PovTr);
                transform.localPosition = Vector3.zero;
                transform.localEulerAngles = Vector3.zero;
            }
            completed?.Invoke();
        }
    }
}
