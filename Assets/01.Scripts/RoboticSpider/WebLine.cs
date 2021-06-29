using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticSpider
{
    [RequireComponent(typeof(LineRenderer))]
    public class WebLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private void Start()
        {
            if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        }

        public void Init(float lineWidth, Vector3 startPos)
        {
            lineRenderer.widthMultiplier = lineWidth;
            lineRenderer.positionCount = 2;
            lineRenderer.numCapVertices = 10;
            lineRenderer.numCornerVertices = 10;

            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, startPos);
        }


        //continuous
        public void UpdateLine(Vector3 updatePos)
        {
            lineRenderer.SetPosition(1, updatePos);
        }

        public void EndLine(Vector3 endPos)
        {
            lineRenderer.SetPosition(1, endPos);
            Mesh mesh = new Mesh();
            lineRenderer.BakeMesh(mesh, true);
            mesh.Optimize();

            MeshCollider collider = this.gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = mesh;

            this.gameObject.layer = LayerMask.NameToLayer("Climable");
        }

        

        
    }


}