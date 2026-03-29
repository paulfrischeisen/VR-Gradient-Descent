using UnityEngine;

public class GDAgent : MonoBehaviour
{
    public float learningRate = 0.1f; // Wie groß sind die Schritte?
    
    void Update()
    {
        // 1. Aktuelle Position bestimmen (nur X und Z sind relevant für den Loss)
        float currentX = transform.position.x;
        float currentZ = transform.position.z;

        // 2. Den Gradienten an dieser Stelle berechnen
        Vector2 gradient = LossFunctions.GetGradient(currentX, currentZ);

        // 3. Gradient Descent Regel: x = x - learningRate * gradient
        // Wir gehen GEGEN den Gradienten, um nach UNTEN zu kommen
        float nextX = currentX - learningRate * gradient.x;
        float nextZ = currentZ - learningRate * gradient.y;

        // 4. Die neue Höhe für die Position holen
        float nextY = LossFunctions.GetLoss(nextX, nextZ);

        // 5. Den Ball bewegen
        transform.position = new Vector3(nextX, nextY, nextZ);
        
        // Optional: Den Pfad mit Debug-Linien zeichnen
        Debug.DrawRay(transform.position, new Vector3(-gradient.x, 0, -gradient.y), Color.red);
    }
}