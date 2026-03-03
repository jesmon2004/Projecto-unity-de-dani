using UnityEngine;
using UnityEngine.SceneManagement; // Librería obligatoria para gestionar escenas

public class MenuManager : MonoBehaviour
{
    // Método que llamaremos desde el botón "Jugar"
    public void EmpezarJuego()
    {
        // "MainScene" debe coincidir EXACTAMENTE con el nombre de tu escena de juego
        SceneManager.LoadScene("MainScene"); 
    }
    


}