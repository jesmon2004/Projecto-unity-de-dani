using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    void Start()
    {
        // Destruye la explosión tras 0.5 segundos (ajusta el tiempo si tu animación o sonido es más largo)
        Destroy(gameObject, 0.5f);
    }
}