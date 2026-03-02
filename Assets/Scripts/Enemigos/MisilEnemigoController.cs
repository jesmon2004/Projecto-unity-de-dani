using UnityEngine;

public class MisilEnemigoController : MonoBehaviour
{
    public float velocidad = 6f;
    public GameObject explosionPrefab;

    void Update()
    {
        // Se mueve constantemente hacia abajo
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        // Si sale de la pantalla, se destruye
        if (transform.position.y < -6f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Colisión con la Nave
        if (collision.CompareTag("Nave") || collision.CompareTag("Player")) 
        {
            NaveController nave = collision.GetComponent<NaveController>();
            if (nave != null)
            {
                nave.RecibirImpacto(1); // Le quitamos 1 vida
            }
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Mostramos explosión
            Destroy(gameObject); // Destruimos misil
        }
        // Colisión con un misil de la nave 
        else if (collision.CompareTag("misil"))
        {
            Destroy(collision.gameObject); // Destruye misil jugador 
            Destroy(gameObject); // Destruye este misil enemigo
        }
    }
}