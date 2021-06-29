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
        void Start()
        {
            InstantiateFlies();
        }

        private void InstantiateFlies()
		{
            for(int i=0; i<startNum_fly; i++)
			{
                Fly fly = Instantiate<Fly>(flyPrefab, Vector3.zero, Quaternion.identity);
                float heightRandom = Random.Range(15f, 18f);
                fly.SetHeight(heightRandom);

                float angleRandomX = Random.Range(0f, 360f);
                float angleRandomY = Random.Range(0f, 360f);
                float angleRandomZ = Random.Range(0f, 360f);
                fly.transform.eulerAngles = new Vector3(angleRandomX, angleRandomY , angleRandomZ);

                flies.Add(fly);
            }
		}
    }

}
