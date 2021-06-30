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

        float angleBetweenCamAndPlayer;
        Quaternion q;
        float heightDiff;
        void Start()
        {
            player = FindObjectOfType<Player>();

            angleBetweenCamAndPlayer =  Vector3.Angle(player.transform.position, transform.position);

            q = Quaternion.AngleAxis(angleBetweenCamAndPlayer, player.transform.position);
            
            offset = player.transform.position - transform.position;
            heightDiff = transform.position.y - player.transform.position.y;
            
        }
        void Update()
        {
            //transform.position = Vector3.Lerp(transform.position,  player.transform.TransformPoint(player.transform.position) - offset, followSpeed * Time.deltaTime);
            transform.position =  Vector3.Lerp(transform.position , player.transform.position - player.transform.TransformVector(offset), followSpeed * Time.deltaTime);

            transform.LookAt(player.transform.position + new Vector3(0, 1.5f,0),Vector3.up);
            // Quaternion destQuaternion = Quaternion.Euler(transform.eulerAngles.x, player.transform.eulerAngles.y, transform.eulerAngles.z);
            // transform.rotation = Quaternion.Lerp(transform.rotation, destQuaternion, followSpeed * Time.deltaTime);



            //cameraPivotTr.LookAt(new Vector3(player.transform.position.x, cameraPivotTr.position.y, player.transform.position.z));
            //transform.rotation = Quaternion.Lerp(transform.rot)
            
            //transform.position = player.transform.position - offset;
			//transform.LookAt(player.transform);
			//cameraPivotTr.position = Vector3.Lerp(cameraPivotTr.position, player.transform.position, followSpeed * Time.deltaTime);
			//cameraPivotTr.rotation = Quaternion.Lerp(cameraPivotTr.rotation, player.transform.rotation, followSpeed * Time.deltaTime);

		}
    }

}