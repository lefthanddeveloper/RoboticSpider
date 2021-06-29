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
            //transform.position = Vector3.Lerp(transform.position,  player.transform.TransformPoint(player.transform.position) - offset, followSpeed * Time.deltaTime);
            transform.position = player.transform.position - offset;
			//transform.LookAt(player.transform);
			//cameraPivotTr.position = Vector3.Lerp(cameraPivotTr.position, player.transform.position, followSpeed * Time.deltaTime);
			//cameraPivotTr.rotation = Quaternion.Lerp(cameraPivotTr.rotation, player.transform.rotation, followSpeed * Time.deltaTime);

		}
    }

}