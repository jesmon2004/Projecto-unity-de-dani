using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Método para volver a jugar directamente
    public void ReintentarJuego()
    {
        SceneManager.LoadScene("MainScene"); 
    }

    // Método para volver a la pantalla de inicio
    public void IrAlMenu()
    {
        SceneManager.LoadScene("StartScene"); 
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}