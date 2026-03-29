using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class LandscapeGenerator : MonoBehaviour
{
    public int xSize = 50; // Anzahl der Quadrate in X-Richtung
    public int zSize = 50; // Anzahl der Quadrate in Z-Richtung
    public float scale = 0.5f; // Abstand zwischen den Punkten

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        // 1. Punkte berechnen (Die Mathematik der Landschaft)
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float xPos = x * scale;
                float zPos = z * scale;
                
                // hier Loss Function aufrufen / nutzen
                // Beispiel: Sinus-Wellen-Landschaft
                float yPos = LossFunctions.GetLoss(xPos, zPos);

                vertices[i] = new Vector3(xPos, yPos, zPos);
                i++;
            }
        }

        // 2. Dreiecke definieren (Wie werden die Punkte verbunden?)
        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals(); // Wichtig für die korrekte Belichtung
    }
}