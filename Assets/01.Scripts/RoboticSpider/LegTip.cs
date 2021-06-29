using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class LegTip : MonoBehaviour
    {
        LayerMask climableLayer;
        private float raycastingOriginHeight = 0.5f;
        private Vector3 stickPosition;
        private bool _isMoving;
        public bool isMoving => _isMoving;
        

        public Transform targetTr;
        public Transform poleTr;
        public float movingThreshold = 0.3f;
        public LegTip unsyncLeg;

		public LegTip crossLeg;

		Vector3 stepNormal = Vector3.zero;
		public Vector3 StepNormal => stepNormal;
        Mover mover;
        private void Start() {
            stickPosition = transform.position;
            climableLayer = LayerMask.GetMask("Climable");

            mover = GetComponentInParent<Mover>();
        }


        private float sphereCastRadius = 0.04f;
        void Update()
        {
            transform.position = stickPosition;

			// Debug.DrawRay(targetTr.position + new Vector3(0, 1, 0) * raycastingOriginHeight, -targetTr.up, Color.red);
			Vector3 rayDir = (targetTr.position + mover.transform.TransformDirection(mover.movingVelocity) * 0.02f) - poleTr.position;
			Debug.DrawRay(poleTr.position, rayDir.normalized, Color.green);
			// if (Physics.Raycast(poleTr.position, rayDir.normalized, out RaycastHit raycastHit, rayDir.magnitude * 2.0f, climableLayer))
			// {
			// 	targetTr.position = raycastHit.point;
			// 	stepNormal = raycastHit.normal;
			// }

			
			
			if(Physics.SphereCast(poleTr.position, sphereCastRadius, rayDir.normalized, out RaycastHit raycastHit, Mathf.Infinity, climableLayer))	
			{
				targetTr.position = raycastHit.point;
				stepNormal = raycastHit.normal;
			}
	

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

		private void OnDrawGizmos() {
			Gizmos.DrawSphere(targetTr.position, sphereCastRadius);
			Gizmos.color = Color.black;
		}
    }

}