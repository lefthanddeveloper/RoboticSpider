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
        //Mover mover;
        private void Start() {
            stickPosition = transform.position;
            climableLayer = LayerMask.GetMask("Climable");

            //mover = GetComponentInParent<Mover>();
        }


        public float stepRadius = 0.25f;
        void Update()
        {
            transform.position = stickPosition;

			Debug.DrawRay(targetTr.position + new Vector3(0, 1, 0) * raycastingOriginHeight, -targetTr.up, Color.red);
			if (Physics.Raycast(targetTr.position + new Vector3(0, 1, 0) * raycastingOriginHeight, -targetTr.up, out RaycastHit raycastHit, 3.0f, climableLayer))
			{
				targetTr.position = raycastHit.point;
				stepNormal = raycastHit.normal;
			}

			//if (mover.GetGlobalVelocity() != Vector3.zero)
			//{
			//             Vector3 dir = (targetTr.position + mover.GetGlobalVelocity()) - (targetTr.position + targetTr.up * 2.0f);

			//             if (Physics.SphereCast(targetTr.position + targetTr.up * 2.0f, stepRadius, dir,out RaycastHit raycastHit, dir.magnitude * 2, climableLayer))
			//	{
			//                 Debug.DrawRay(targetTr.position + targetTr.up * 2.0f, dir, Color.red);
			//                 _isMoving = true;
			//                 stickPosition = targetTr.position;
			//	}
			//             else
			//	{
			//                 _isMoving = false;
			//	}
			//}
			//         else
			//{
			//             _isMoving = false;
			//}


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