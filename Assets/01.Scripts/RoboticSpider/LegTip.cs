using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class LegTip : MonoBehaviour
    {
        private Vector3 stickPosition;
        private bool _isMoving;
        public bool isMoving => _isMoving;
        

        public Transform targetTr;
        public float movingThreshold = 0.3f;
        public LegTip crossLeg;

        private void Start() {
            stickPosition = transform.position;
        }


        void Update()
        {
            transform.position = stickPosition;

            float distance = Mathf.Abs(Vector3.Distance(transform.position, targetTr.position));
            if (distance >= movingThreshold && !crossLeg.isMoving)
            {
                _isMoving = true;
                stickPosition = targetTr.position;
            }
            else
            {
                _isMoving = false;
            }
        }
    }

}