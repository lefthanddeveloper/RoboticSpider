using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class MapCameraFollow : MonoBehaviour
    {
        [SerializeField] Transform cameraPivotTr;
        Player player;
        private float followSpeed = 1.0f;
        Vector3 offset;
        void Start()
        {
            player = FindObjectOfType<Player>();
            offset = player.transform.position - transform.position;
        }
        void Update()
        {
            transform.position =  Vector3.Lerp(transform.position , player.transform.position - player.transform.TransformVector(offset), followSpeed * Time.deltaTime);
            transform.LookAt(player.transform.position + new Vector3(0, 1.5f,0),Vector3.up);
		}
    }

}