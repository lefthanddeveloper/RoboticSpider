using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPractice : MonoBehaviour
{
    public Transform targetTR;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quaternion LookRotation
        // Quaternion value = Quaternion.LookRotation(targetTR.position, transform.position);
        // print("in Q :" + value);

        // print("in E : " + value.eulerAngles);
        // transform.rotation = value;

        //Quaternion.Angle
        float value = Quaternion.Angle(transform.rotation, targetTR.rotation);
        print(value);
    }
}
