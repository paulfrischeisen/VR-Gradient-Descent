using UnityEngine;

public static class LossFunctions
{
    // Die eigentliche Funktion f(x, z) = y
    public static float GetLoss(float x, float z)
    {
        // Beispiel: "Hügelige Landschaft"
        // Du kannst hier später jede beliebige Formel einsetzen
        return Mathf.Sin(x * 0.5f) * Mathf.Cos(z * 0.5f) * 2f;
        
        // Alternative (Parabel): return (x*x + z*z) * 0.1f;
    }

    // Der Gradient (Die Ableitung)
    // Er gibt uns die Richtung des steilsten ANstiegs
    public static Vector2 GetGradient(float x, float z)
    {
        float h = 0.01f; // Kleiner Schritt für die numerische Ableitung
        
        // Partielle Ableitung nach x: (f(x+h) - f(x)) / h
        float gradX = (GetLoss(x + h, z) - GetLoss(x, z)) / h;
        
        // Partielle Ableitung nach z: (f(x, z+h) - f(x, z)) / h
        float gradZ = (GetLoss(x, z + h) - GetLoss(x, z)) / h;

        return new Vector2(gradX, gradZ);
    }
}