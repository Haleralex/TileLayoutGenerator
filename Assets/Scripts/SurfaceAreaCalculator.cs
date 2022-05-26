using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SurfaceAreaCalculator : MonoBehaviour
{
    public float CalculateSurface(GameObject obj)
    {
        var surfaceAreaObject = 0.0f;

        var mesh = obj.GetComponent<MeshFilter>().mesh;

        var triangles = mesh.triangles;
        var vertices = mesh.vertices;

        float surfaceArea = 0.0f;

        for (int i = 0; i < triangles.Count() - 2; i += 3)
        {
            var a = new Vector2(obj.transform.TransformPoint(vertices[triangles[i]]).x, obj.transform.TransformPoint(vertices[triangles[i]]).z);
            var b = new Vector2(obj.transform.TransformPoint(vertices[triangles[i + 1]]).x, obj.transform.TransformPoint(vertices[triangles[i + 1]]).z);
            var c = new Vector2(obj.transform.TransformPoint(vertices[triangles[i + 2]]).x, obj.transform.TransformPoint(vertices[triangles[i + 2]]).z);
            float surfaceAreaTriangle = Mathf.Abs((b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y)) * 0.5f;

            surfaceArea += surfaceAreaTriangle;
        }

        surfaceAreaObject += surfaceArea;
        return surfaceAreaObject;
    }
}
