using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class Fly : InsectBase
    {
		[SerializeField] private Transform targetToHeight;
		public void SetHeight(float height)
		{
			targetToHeight.localPosition = new Vector3(0, height, 0);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="speed">0.5 ~ 1.5 might be good</param>
		public void SetAnimationSpeed(float speed)
		{
			speed = Mathf.Clamp(speed, 0.5f, 1.5f);

			for(int i=0; i< animators.Length; i++)
			{
				animators[i].speed = speed;
			}
		}

		protected override void OnTouchedWeb(WebLine touchedLine, Vector3 contactPoint, Vector3 contactNormal)
		{
			print("touch!!");
			for(int i=0; i< animators.Length; i++)
			{
                animators[i].StopPlayback();
                animators[i].enabled = false;
			}
		}
    }

}
