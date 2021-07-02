using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class LegTip : MonoBehaviour
    {
		#region different Trial
		//public Vector3 optimalRestingPosition = Vector3.forward;
		//public Vector3 restingPosition
		//{
		//	get
		//	{
		//		return transform.TransformPoint(optimalRestingPosition);
		//	}
		//}

		//public Vector3 worldVelocity = Vector3.zero;

		//public Vector3 desiredPosition
		//{
		//	get
		//	{
		//		return restingPosition + worldVelocity + (Random.insideUnitSphere * 0.005f);
		//	}
		//}

		//public Vector3 worldTarget = Vector3.zero;
		//public Vector3 stepPoint;
		//public bool legGrounded;


		//public float stepCoolDown = 1f;
		//public float stepDuration = 0.5f;
		//public float stepOffset;
		//public float lastStep = 0;
		//public float percent
		//{
		//	get
		//	{
		//		return Mathf.Clamp01((Time.time - lastStep) / stepDuration);
		//	}
		//}
		//public AnimationCurve stepHeightCurve;
		//public float stepHeightMultiplier = 0.25f;
		#endregion

		public LayerMask climableLayer;
        private Vector3 stickPosition;
        private bool _isMoving;
        public bool isMoving => _isMoving;
		private bool _isGrounded;
		public bool isGrounded => _isGrounded;
        
        public Transform targetTr;
        public Transform poleTr;
        public float movingThreshold = 0.3f;
        public LegTip unsyncLeg; //legs that are not in sync on purpose
		public LegTip crossLeg; //leg located diagonally

		Vector3 stepNormal = Vector3.zero;
		public Vector3 StepNormal => stepNormal;
        Mover mover;

		Vector3 dirToEarth;
        private void Start() {
            stickPosition = transform.position;
            //climableLayer = LayerMask.GetMask("Climable");

			//lastStep = Time.time + stepCoolDown * stepOffset;
			//targetTr.position = restingPosition;
			//Step();

            mover = GetComponentInParent<Mover>();
			dirToEarth = Vector3.zero - transform.position;
        }

		private float sphereCastRadius = 0.08f;
		void Update()
        {
			//UpdateIKTarget();
			//if(Time.time > lastStep + stepCoolDown)
			//{
			//	Step();
			//}

			transform.position = stickPosition;


			Vector3 rayDir = (targetTr.position + mover.transform.TransformDirection(mover.movingVelocity.normalized) * 0.05f) - poleTr.position;

			// print("rayDir : " + rayDir);
			//Debug.DrawRay(poleTr.position, rayDir.normalized, Color.green);
			//Debug.DrawLine(poleTr.position, poleTr.position + new Vector3(0, -0.5f, 0), Color.black);

			////RayCast 방식
			// if (Physics.Raycast(poleTr.position, rayDir.normalized, out RaycastHit raycastHit, rayDir.magnitude * 2.0f, climableLayer))
			// {
			// 	targetTr.position = raycastHit.point;
			// 	stepNormal = raycastHit.normal;
			// }

			//Spherecast 방식
			if (Physics.SphereCast(poleTr.position, sphereCastRadius, rayDir.normalized, out RaycastHit raycastHit, rayDir.magnitude*1.5f, climableLayer))
			{
				_isGrounded = true;
				targetTr.position = raycastHit.point;
				stepNormal = raycastHit.normal;
			}
			else
			{
				//targetTr.position = Vector3.Lerp(targetTr.position, crossLeg.transform.position, Time.deltaTime);
				_isGrounded = false;

				Vector3 dir = Vector3.zero - poleTr.position;
				if (Physics.SphereCast(poleTr.position, sphereCastRadius, dir.normalized, out RaycastHit raycastHit2, dir.magnitude*1.5f, climableLayer))
				{
					print("candidate 1");
					//targetTr.Translate(0, -0.2f * Time.deltaTime, 0, Space.World);
					targetTr.position = Vector3.Lerp(targetTr.position, raycastHit.point, 0.5f * Time.deltaTime);
				}
				else
				{
					print("candidate 2");
					float radius = sphereCastRadius * 2;
					Collider[] colls = { };
					print("enter");
					while (colls.Length == 0)
					{
						colls = Physics.OverlapSphere(targetTr.position, radius, climableLayer);
						radius += sphereCastRadius / 2;
						print("radius : " + radius);
					}
					Vector3 closestPoint = colls[0].ClosestPointOnBounds(targetTr.position);
					print(colls[0].gameObject.name);
					print("out");

					targetTr.position = closestPoint;
					print("closest : " + closestPoint);

				}
				//targetTr.Translate(0, -0.2f * Time.deltaTime, 0, Space.World); //gravity; prevent floating up
				// targetTr.position = Vector3.MoveTowards(targetTr.position, Vector3.zero, Time.deltaTime);
			}

			float distance = Mathf.Abs(Vector3.Distance(transform.position, targetTr.position));
			if (distance >= movingThreshold && !crossLeg.isMoving)
			{
				_isMoving = true;
				stickPosition = Vector3.Lerp(stickPosition, targetTr.position, 1f);
			}
			else
			{
				_isMoving = false;
			}
		}

		//IEnumerator FindClosestPoint(Vector3 origianlRayDir)
		//{
		//	bool found =false;
		//	float initialRadius = sphereCastRadius / 2;
		//	while (!found)
		//	{
		//		RaycastHit[] hits = Physics.SphereCastAll(transform.position, initialRadius, -origianlRayDir.normalized * 0.001f, climableLayer);
		//		if(hits.Length ==0)
		//		{
		//			initialRadius += sphereCastRadius / 2;
		//			continue;
		//		}

		//		targetTr.position = hits[0].point;

		//	}

		//}
		//private void UpdateIKTarget()
		//{
		//	stepPoint = AdjustPosition(worldTarget + worldVelocity);
		//	targetTr.position = Vector3.Lerp(targetTr.position, stepPoint, percent) + stepNormal * stepHeightCurve.Evaluate(percent) * stepHeightMultiplier;
		//	transform.position = targetTr.position;
		//}

		//public void Step()
		//{
		//	stepPoint = worldTarget = AdjustPosition(desiredPosition);
		//	lastStep = Time.time;
		//}

		//public Vector3 AdjustPosition(Vector3 position)
		//{
		//	Vector3 direction = position - poleTr.position;
		//	if(Physics.SphereCast(poleTr.position, sphereCastRadius, direction, out RaycastHit raycastHit, direction.magnitude * 2f, climableLayer))
		//	{
		//		position = raycastHit.point;
		//		stepNormal = raycastHit.normal;
		//		legGrounded = true;
		//	}
		//	else
		//	{
		//		position = restingPosition;
		//		stepNormal = Vector3.zero;
		//		legGrounded = false;
		//	}
		//	return position;
		//}

		//public void MoveVelocity(Vector3 newVelocity)
		//{
		//	worldVelocity = Vector3.Lerp(worldVelocity, newVelocity, 1f - percent);
		//}

		private void OnDrawGizmos() {
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(targetTr.position, sphereCastRadius);

			Gizmos.color = Color.blue;
			Gizmos.DrawLine(poleTr.position, targetTr.position);

			// Gizmos.color = Color.yellow;
			// Gizmos.DrawLine(poleTr.position, transform.position);

			// Gizmos.color = Color.blue;
			// Gizmos.DrawWireSphere(restingPosition, 0.1f);

			// Gizmos.color = Color.cyan;
			// Gizmos.DrawWireSphere(worldTarget, 0.1f);

			// Gizmos.color = Color.red;
			// Gizmos.DrawWireSphere(stepPoint, .1f);

			// Gizmos.color = Color.black;
			// Gizmos.DrawWireSphere(poleTr.position, sphereCastRadius);
		}
    }

}