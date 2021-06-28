using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTipMovement : MonoBehaviour
{
    public Vector3 stayPos;

    public Transform tipTargetTr;

    private float movingSpeed = 50.0f;
    private float movingThreshold = 1.0f;

    LayerMask groundLayer;

    public LegTipMovement crossLeg;
    private bool isMoving;
    private bool moving; //for finish moving
    void Start()
    {
        stayPos = transform.position;
        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Raycasting();




        transform.position = stayPos;

        float distance = Mathf.Abs(Vector3.Distance(stayPos, tipTargetTr.position));
        // print(distance.ToString());
        if( (distance >= movingThreshold&& !crossLeg.IsMoving()) || moving)
        {
            isMoving = true;
            stayPos =  Vector3.Lerp(stayPos, tipTargetTr.position, movingSpeed * Time.deltaTime);
            moving = true;

            if(distance <= 0.1f)
            {
                moving = false;
            }
        }
        else
        {
            if(isMoving) isMoving = false;
        }
    }

    private void Raycasting()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(tipTargetTr.position + Vector3.up, -tipTargetTr.transform.up, out raycastHit, 5.0f,groundLayer))
        {
            tipTargetTr.position = raycastHit.point;
        }
    }

    public bool IsMoving()
    {
        return isMoving;
    }
}
