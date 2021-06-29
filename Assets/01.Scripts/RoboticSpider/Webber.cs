using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    public class Webber : MonoBehaviour
    {
        [SerializeField] private WebLine webLinePrefab;
        [SerializeField] private Transform webOriginTr;
        // [SerializeField] private 
        private float lineWidth = 0.01f;
        private bool isWebbing;

        private WebLine curWebLine;

        LayerMask climableLayer;


        void Start()
        {
            climableLayer = LayerMask.GetMask("Climable");
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(webOriginTr.position, -webOriginTr.up, Color.red);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(webOriginTr.position, -webOriginTr.up, out RaycastHit raycastHit, 1.0f, climableLayer))
                {
                    if (!isWebbing)
                    {
                        WebLine webLine = Instantiate<WebLine>(webLinePrefab);
                        webLine.Init(lineWidth, raycastHit.point);
                        curWebLine = webLine;

                        isWebbing = true;
                    }
                    else
                    {
                        curWebLine.EndLine(raycastHit.point);
                        isWebbing = false;
                        curWebLine = null;
                    }
                }

            }


            if (isWebbing)
            {
                Webbing();
            }

        }

        private void Webbing()
        {
            if (curWebLine == null) return;
            curWebLine.UpdateLine(webOriginTr.position);
        }
    }

}