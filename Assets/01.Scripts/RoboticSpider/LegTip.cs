using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class LegTip : MonoBehaviour
    {
        LayerMask climableLayer;
        private Vector3 stickPosition;
        private bool _isMoving;
        public bool isMoving => _isMoving;
        

        public Transform targetTr;
        public Transform poleTr;
        public float movingThreshold = 0.3f;
        public LegTip unsyncLeg; //legs that are not in sync on purpose
		public LegTip crossLeg; //leg located diagonally

		Vector3 stepNormal = Vector3.zero;
		public Vector3 StepNormal => stepNormal;
        Mover mover;
        private void Start() {
            stickPosition = transform.position;
            climableLayer = LayerMask.GetMask("Climable");

            mover = GetComponentInParent<Mover>();
        }

        private float sphereCastRadius = 0.035f;
        void Update()
        {
            transform.position = stickPosition;

			
			Vector3 rayDir = (targetTr.position + mover.transform.TransformDirection(mover.movingVelocity) * 0.015f) - poleTr.position;
			
			// print("rayDir : " + rayDir);
			Debug.DrawRay(poleTr.position, rayDir.normalized, Color.green);
			Debug.DrawLine(poleTr.position, poleTr.position + new Vector3(0,-0.5f,0), Color.black);

			////RayCast 방식
			// if (Physics.Raycast(poleTr.position, rayDir.normalized, out RaycastHit raycastHit, rayDir.magnitude * 2.0f, climableLayer))
			// {
			// 	targetTr.position = raycastHit.point;
			// 	stepNormal = raycastHit.normal;
			// }

			//Spherecast 방식
			if(Physics.SphereCast(poleTr.position, sphereCastRadius, rayDir.normalized, out RaycastHit raycastHit, 0.5f, climableLayer))	
			{
				targetTr.position = raycastHit.point;
				stepNormal = raycastHit.normal;
			}
			else
			{
				targetTr.Translate(0, -1 * Time.deltaTime, 0, Space.World); //gravity; prevent floating up
			}
	
			float distance = Mathf.Abs(Vector3.Distance(transform.position, targetTr.position));
			if (distance >= movingThreshold && !crossLeg.isMoving)
			{
				// print("distance: " + distance);
				_isMoving = true;
				stickPosition = Vector3.Lerp(stickPosition,targetTr.position, 0.8f);
				
				
				//  stickPosition = Vector3.MoveTowards(stickPosition,targetTr.position, 10.0f * Time.deltaTime); 
			}
			else
			{
				_isMoving = false;
				// transform.Translate(0, -20 * Time.deltaTime, 0, Space.World);
				
			}
		}

		private void OnDrawGizmos() {
			Gizmos.DrawWireSphere(targetTr.position, sphereCastRadius);
			Gizmos.color = Color.cyan;
		}
    }

}