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
