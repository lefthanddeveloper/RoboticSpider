using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class InsectFactory : MonoBehaviour
    {
        [SerializeField] private Fly flyPrefab;
        public int startNum_fly = 10;
        private List<Fly> flies = new List<Fly>();
        [SerializeField] private Transform flyContainer;
        void Start()
        {
            InstantiateFlies();
        }

        private void InstantiateFlies()
		{
            for(int i=0; i<startNum_fly; i++)
			{
                Fly fly = Instantiate<Fly>(flyPrefab, flyContainer);
                float heightRandom = Random.Range(15f, 18f);
                fly.SetHeight(heightRandom);

                float randomSpeed = Random.Range(0.5f, 1.5f);
                fly.SetAnimationSpeed(randomSpeed);


                float angleRandomX = Random.Range(0f, 360f);
                float angleRandomY = Random.Range(0f, 360f);
                float angleRandomZ = Random.Range(0f, 360f);
                fly.transform.eulerAngles = new Vector3(angleRandomX, angleRandomY , angleRandomZ);

                flies.Add(fly);
            }
		}
    }

}
