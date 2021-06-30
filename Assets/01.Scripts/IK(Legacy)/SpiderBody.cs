using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBody : MonoBehaviour
{
    public Transform[] targetTr;

    public Vector3 originalPos;
    public float offsetY;

    
    
    void Start()
    {
        
        originalPos = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, originalPos.y + GetAverageYOffset(), transform.position.z); 

        // transform.position = new Vector3(transform.position.x, originalPos.y + GetHighestYOffset(), transform.position.z); 
        
    }


    private float GetAverageYOffset()
    {
        float sum =0;
        for(int i=0; i< targetTr.Length;i++)
        {
            sum += targetTr[i].position.y;
        }
        return sum/(float)targetTr.Length;
    }

    private float GetHighestYOffset()
    {
        float highest = 0;
        for(int i=0; i< targetTr.Length; i++)
        {
            if(highest < targetTr[i].position.y)
            {
                highest = targetTr[i].position.y;
            }
        }
        return highest;
    }
}
