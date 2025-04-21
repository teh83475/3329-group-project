using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class VisionConeGenerator : MonoBehaviour
{
    [Range(0, 360)] public float angle = 130f;
    public float range = 5f;
    public int resolution = 30;

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        GenerateVisionCone();
    }

    void GenerateVisionCone()
    {
        Vector3[] vertices = new Vector3[resolution + 2];
        int[] triangles = new int[resolution * 3];

        vertices[0] = Vector3.zero; // Center point

        float angleIncrement = angle / resolution;
        float currentAngle = -angle / 2;

        for (int i = 1; i <= resolution + 1; i++)
        {
            float rad = currentAngle * Mathf.Deg2Rad;
            vertices[i] = new Vector3(Mathf.Sin(rad) * range, 0, Mathf.Cos(rad) * range);
            currentAngle += angleIncrement;
        }

        for (int i = 0, ti = 0; i < resolution; i++, ti += 3)
        {
            triangles[ti] = 0;
            triangles[ti + 1] = i + 1;
            triangles[ti + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    void OnValidate()
    {
        if (mesh != null)
        {
            GenerateVisionCone();
        }
    }
}
