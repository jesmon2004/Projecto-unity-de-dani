using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton
    public TMP_Text texto;               // Referencia al texto del canvas
    private int score = 0;               // Puntos acumulados

    void Awake()
    {
        // Configuramos la instancia
        instance = this;
    }

    public void AddScore(int points)
    {
        score += points;
        if (score < 0) 
        {
            score = 0; 
        }
        
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        texto.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
}