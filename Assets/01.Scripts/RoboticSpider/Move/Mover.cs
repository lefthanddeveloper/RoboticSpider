using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
	public class Mover : MonoBehaviour
	{
		//public float averageRotationRadius = 1.0f;
		//public bool grounded;
		//Vector3 direction;
		//Vector3 worldVelocity;
		//private float timeBetweenSteps = 0.25f;
		//private float lastStep;
		//private int index;

		public float movingSpeed = 1.0f;
		public float rotatingSpeed = 1.0f;

		[SerializeField] private LegTip[] legTips;

		//LayerMask climableLayer;
		Vector3 movingDir;
		public Vector3 movingVelocity => movingDir * movingSpeed; //transform.Translate에서는 안쓰이고 pole에서 Spherecast 할때만 쓰임. Transform.Translate에선 Horizontal이 안쓰임

		private void Start()
		{
			//for(int i=0; i< legTips.Length;i++)
			//{
			//	averageRotationRadius += legTips[i].restingPosition.z;
			//}
			//averageRotationRadius /= legTips.Length;

			//climableLayer = LayerMask.GetMask("Climable");
		}



		void Update()
		{
			#region different trial
			//CalculateOrientation();

			////MOVE
			//float horizontal = Input.GetAxisRaw("Horizontal");
			//float vertical = Input.GetAxisRaw("Vertical");
			//direction = new Vector3(horizontal, 0, vertical);
			//worldVelocity = direction * movingSpeed;

			//transform.Translate(Vector3.forward * vertical * movingSpeed * Time.deltaTime);
			//transform.Rotate(0, horizontal * rotatingSpeed, 0, Space.World);

			//if(grounded)
			//{
			//	timeBetweenSteps = 0.3f / Mathf.Max(worldVelocity.magnitude, Mathf.Abs(2 * Mathf.PI * rotatingSpeed * Mathf.Deg2Rad * averageRotationRadius));
			//}
			//else
			//{
			//	timeBetweenSteps = 0.25f;
			//}

			//if(Time.time > lastStep + (timeBetweenSteps/legTips.Length))
			//{
			//	index = (index + 1) % legTips.Length;
			//	if (legTips[index] == null) return;

			//	for(int i=0; i< legTips.Length;i++)
			//	{
			//		legTips[i].MoveVelocity(CalculateLegVeloicty(i));
			//	}

			//	legTips[index].stepDuration = Mathf.Min(1f, (timeBetweenSteps / legTips.Length) * 2f); //2 = stepDurationRatio
			//	legTips[index].worldVelocity = CalculateLegVeloicty(index);
			//	legTips[index].Step();
			//	lastStep = Time.time;
			//}
			#endregion

			//Moving
			float vertical = Input.GetAxisRaw("Vertical");
			//Rotation
			float horizontal = Input.GetAxisRaw("Horizontal");

			movingDir = new Vector3(horizontal * 0.5f, 0, vertical);

			CalculateOrientation();

			transform.Translate(Vector3.forward * vertical * movingSpeed * Time.deltaTime);
			transform.Rotate(transform.up * horizontal * rotatingSpeed, Space.World);
		}

		private void CalculateOrientation()
		{
			//Vector3 up = Vector3.zero;
			//float avgSurfaceDist = 0;
			//grounded = false;
			//Vector3 point, a, b, c;

			//for(int i=0; i<legTips.Length;i++)
			//{
			//	point = legTips[i].stepPoint;
			//	avgSurfaceDist += transform.InverseTransformPoint(point).y;
			//	a = (transform.position - point).normalized;
			//	b = ((legTips[i].crossLeg.stepPoint) - point).normalized;
			//	c = Vector3.Cross(a, b);
			//	up += c * 0.5f + (legTips[i].StepNormal == Vector3.zero ? transform.forward : legTips[i].StepNormal);
			//	grounded |= legTips[i].legGrounded;
			//}

			//up /= legTips.Length;
			//avgSurfaceDist /= legTips.Length;


			//transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up), 22.5f * Time.deltaTime);
			//if(grounded)
			//{
			//	transform.Translate(0, avgSurfaceDist * 0.5f, 0, Space.Self);
			//}
			//else
			//{
			//	transform.Translate(0, -3f * Time.deltaTime, 0, Space.World);
			//}


			Vector3 up = Vector3.zero;
			Vector3 targetPosForEach;
			Vector3 a, b, c; //a: normal from TargetPos to Mover Body
							 //b: normal from TargetPos to CrossTip TargetPos;
							 //c: Cross Project of a and b;

			float avgDistance = 0;

			for (int i = 0; i < legTips.Length; i++)
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

			
			// transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up), 50.0f * Time.deltaTime);
			transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up);
			transform.Translate(0, avgDistance * 0.5f, 0, Space.Self);
		}

		//private Vector3 CalculateLegVeloicty(int index)
		//{
		//	Vector3 legPoint = legTips[index].restingPosition;
		//	Vector3 legDirection = legPoint - transform.position;
		//	Vector3 rotationalPoint = ((Quaternion.AngleAxis((rotatingSpeed * timeBetweenSteps) / 2f, transform.up) * legDirection) + transform.position) - legPoint;
		//	return rotationalPoint + (worldVelocity * timeBetweenSteps) / 2f;
		//}
		

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