using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private MeshFilter _meshFilter;


    [SerializeField]
    private float _fov = 90f, _viewDistance = 50f;

    [SerializeField]
    private int _rayCount = 2;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private void Start()
    {
        Mesh mesh = new Mesh();

        float angle = 0;
        float angleIncrease = _fov / _rayCount;
        
        

        Vector3[] vertices = new Vector3[_rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[_rayCount * 3];

        Vector3 origin = Vector3.zero;
        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= _rayCount; ++i)
        {
            Vector3 rotateVec = GetVectorFromAngle(angle);
            Vector3 vertex;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rotateVec, _viewDistance);

            if(hit.collider == null)
            {
                //No hit
                Debug.Log("No hit");
                vertex = origin + rotateVec * _viewDistance;
            }
            else
            {
                //hit something
                Debug.Log(hit.collider.name);
                vertex = transform.InverseTransformPoint( hit.point);
            }
            vertices[vertexIndex] = vertex;

            if(i > 0 )
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        _meshFilter.mesh = mesh;
    }
}
