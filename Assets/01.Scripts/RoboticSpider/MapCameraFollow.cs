using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class MapCameraFollow : MonoBehaviour
    {
        Player player;
        Vector3 offset;
        Vector3 rotationOffset;
        private float followSpeed = 1.0f;
        void Start()
        {
            player = FindObjectOfType<Player>();
            offset = player.transform.position - transform.position;
            // rotationOffset = player.transform.eulerAngles - transform.eulerAngles;
        }
        void Update()
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position - offset, followSpeed * Time.deltaTime);
            // transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, player.transform.eulerAngles - rotationOffset, followSpeed*Time.deltaTime);
        }
    }

}