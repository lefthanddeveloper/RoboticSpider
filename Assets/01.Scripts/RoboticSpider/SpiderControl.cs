using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
	public class SpiderControl : MonoBehaviour
	{
		public Transform targetPoses;
		public Player player;


		private void Update()
		{
			//targetPoses.position = player.transform.position;
			//targetPoses.eulerAngles = player.transform.eulerAngles;
		}
	}

}