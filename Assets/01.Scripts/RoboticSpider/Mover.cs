using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class Mover : MonoBehaviour
{
    public float movingSpeed = 1.0f;
    public float rotatingSpeed = 1.0f;
    void Start()
    {
        
    }
    void Update()
    {

        //Moving
        float vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.forward * vertical * movingSpeed * Time.deltaTime);


        //Rotation
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Rotate(transform.up * horizontal * rotatingSpeed, Space.World);
        
    }
}

}