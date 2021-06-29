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
            transform.position = /*transform.position +*/ player.transform.position +  q * -offset;
            //cameraPivotTr.LookAt(new Vector3(player.transform.position.x, cameraPivotTr.position.y, player.transform.position.z));
            //transform.rotation = Quaternion.Lerp(transform.rot)
            
            //transform.position = player.transform.position - offset;
			//transform.LookAt(player.transform);
			//cameraPivotTr.position = Vector3.Lerp(cameraPivotTr.position, player.transform.position, followSpeed * Time.deltaTime);
			//cameraPivotTr.rotation = Quaternion.Lerp(cameraPivotTr.rotation, player.transform.rotation, followSpeed * Time.deltaTime);

		}
    }

}