using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TilesCutter : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _borders = new List<GameObject>();

    [SerializeField]
    private SurfaceAreaCalculator _surfaceAreaCalculator;


    public List<Collider> Tiles { get; set; } = new List<Collider>();


    //borders - 4 точки (верхний левый угол, нижний левый угол, верхний правый угол, нижний правый угол) - меняя их, менятеся размер нашей стены
    public void Cut()
    {
        foreach (var k in Tiles.ToList())
            Cut(_borders[0].transform.position, _borders[1].transform.position, k);

        foreach (var k in Tiles.ToList())
            Cut(_borders[3].transform.position, _borders[0].transform.position, k);

        foreach (var k in Tiles.ToList())
            Cut(_borders[2].transform.position, _borders[3].transform.position, k);

        foreach (var k in Tiles.ToList())
            Cut(_borders[1].transform.position, _borders[2].transform.position, k);
    }

    public float CalculateSurface()
    {
        var surfaceArea = 0.0f;

        foreach (var plane in Tiles.ToList())
        {
            var surfaceAreaPart = _surfaceAreaCalculator.CalculateSurface(plane.gameObject);
            surfaceArea += surfaceAreaPart;
        }

        return surfaceArea;
    }

    private void Cut(Vector3 start, Vector3 end, Collider planer)
    {
        var startingPoint = start - Vector3.up*2;
        var _triggerEnterBasePosition = start + Vector3.up*2;

        Vector3 side1 = end - startingPoint;
        Vector3 side2 = end - _triggerEnterBasePosition;

        Vector3 normal = Vector3.Cross(side1, side2).normalized;
        
        Vector3 transformedNormal = ((Vector3)(planer.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        Vector3 transformedStartingPoint = planer.gameObject.transform.InverseTransformPoint(startingPoint);

        Plane plane = new Plane();

        plane.SetNormalAndPosition(
                transformedNormal,
                transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        if (direction < 0)
        {
            plane = plane.flipped;
        }

        GameObject[] slices = Slicer.Slice(plane, planer.gameObject);
        Destroy(slices[0]);
        Tiles.Remove(planer);

        var mesh = slices[1].GetComponent<MeshFilter>().mesh;

        if(mesh.vertices.Count() > 0)
        {
            var collider = slices[1].GetComponent<Collider>();
            Tiles.Add(collider);
        }
        else
            Destroy(slices[1]);

        Destroy(planer.gameObject);
    }

    public void DestroyAllTiles()
    {
        foreach(var tile in Tiles)
        {
            Destroy(tile.gameObject);
        }
    }
}
