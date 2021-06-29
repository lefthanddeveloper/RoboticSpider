using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
	public class Mover : MonoBehaviour
	{
		public float movingSpeed = 1.0f;
		public float rotatingSpeed = 1.0f;

		[SerializeField] private LegTip[] legTips;

		[SerializeField] private Transform earth;

		LayerMask climableLayer;
		Vector3 movingDir;
		public Vector3 movingVelocity => movingDir * movingSpeed; //transform.Translate에서는 안쓰이고 pole에서 Spherecast 할때만 쓰임. Transform.Translate에선 Horizontal이 안쓰임

		private void Start()
		{
			climableLayer = LayerMask.GetMask("Climable");
		}
		void Update()
		{
			CalculateOrientation();

			//Moving
			float vertical = Input.GetAxisRaw("Vertical");
			//Rotation
			float horizontal = Input.GetAxisRaw("Horizontal");
			
			movingDir = new Vector3(horizontal * 0.5f, 0, vertical);
			// if (vertical != 0 || horizontal != 0)
			{

				transform.Translate(Vector3.forward * vertical * movingSpeed * Time.deltaTime);
				transform.Rotate(transform.up * horizontal * rotatingSpeed, Space.World);
				
				// transform.position = new Vector3(transform.position.x, GetAverageHeight(), transform.position.z);
			}
				
		}

		private void CalculateOrientation()
		{
			Vector3 up = Vector3.zero;
			Vector3 targetPosForEach;
			Vector3 a, b, c; //a: normal from TargetPos to Mover Body
							//b: normal from TargetPos to CrossTip TargetPos;
							//c: Cross Project of a and b;

			float avgDistance = 0;
			for(int i=0; i<legTips.Length;i++)
			{
				//orientation
				targetPosForEach = legTips[i].targetTr.position;
				a = (transform.position - targetPosForEach).normalized;
				b = (legTips[i].crossLeg.targetTr.position - targetPosForEach).normalized;
				c = Vector3.Cross(a, b);

				up += c * 0.5f + (legTips[i].StepNormal == Vector3.zero ? transform.forward : legTips[i].StepNormal);

				//distance from ground
				avgDistance += transform.InverseTransformPoint(targetPosForEach).y;
			}


			up /= legTips.Length;
			avgDistance /= legTips.Length;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up), 30.0f * Time.deltaTime);
			transform.Translate(0, avgDistance*0.5f, 0, Space.Self);
		}
		

		// private float GetAverageHeight()
		// {
		// 	float sum = 0;
		// 	for (int i = 0; i < targetPoses.Length; i++)
		// 	{
		// 		sum += targetPoses[i].transform.position.y;
		// 	}
		// 	return sum / targetPoses.Length;
		// }
	}

}