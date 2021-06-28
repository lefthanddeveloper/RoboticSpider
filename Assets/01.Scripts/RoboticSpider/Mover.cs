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
		[SerializeField] private Transform[] targetPoses;

		[SerializeField] private Transform earth;
		//private float gravity = -9.81f;

		LayerMask climableLayer;
		Rigidbody rigid;

		//Vector3 curPos;
		//Vector3 prePos;
		//Vector3 movingVelocity;
		private void Start()
		{
			climableLayer = LayerMask.GetMask("Climable");
			rigid = GetComponent<Rigidbody>();
		}
		void Update()
		{
			CalculateOrientation();

			//Moving
			float vertical = Input.GetAxisRaw("Vertical");
			//Rotation
			float horizontal = Input.GetAxisRaw("Horizontal");

			if (vertical != 0 || horizontal != 0)
			{

				transform.Translate(Vector3.forward * vertical * movingSpeed * Time.deltaTime);
				transform.Rotate(transform.up * horizontal * rotatingSpeed, Space.World);
				
				transform.position = new Vector3(transform.position.x, GetAverageHeight(), transform.position.z);
			}



			//curPos = transform.position;
			//movingVelocity = curPos - prePos;
			//prePos = curPos;



			//Vector3 diffFront = (legTips[1].transform.position - legTips[0].transform.position).normalized;
			//float angleInRad = Mathf.Atan2(diffFront.y, diffFront.x);
			//float angleInDeg = Mathf.Rad2Deg * angleInRad;
			//print(angleInDeg);


	
		}

		private void CalculateOrientation()
		{
			Vector3 up = Vector3.zero;
			Vector3 targetPosForEach;
			Vector3 a, b, c; //a: normal from TargetPos to Mover Body
							//b: normal from TargetPos to CrossTip TargetPos;
							//c: Cross Project of a and b;
			for(int i=0; i<legTips.Length;i++)
			{
				targetPosForEach = legTips[i].targetTr.position;
				a = (transform.position - targetPosForEach).normalized;
				b = (legTips[i].crossLeg.targetTr.position - targetPosForEach).normalized;
				c = Vector3.Cross(a, b);

				up += c * 0.1f + (legTips[i].StepNormal == Vector3.zero ? transform.forward : legTips[i].StepNormal);
			}

			up /= legTips.Length;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up), 1.0f * Time.deltaTime);

		}
		

		//public Vector3 GetGlobalVelocity()
		//{
		//	return movingVelocity;
		//}

		private float GetAverageHeight()
		{
			//float sum = 0;
			//for(int i=0; i< legTips.Length;i++)
			//{
			//	sum += legTips[i].transform.position.y;
			//}
			//return sum/legTips.Length;

			float sum = 0;
			for (int i = 0; i < targetPoses.Length; i++)
			{
				sum += targetPoses[i].transform.position.y;
			}
			return sum / targetPoses.Length;
		}
	}

}