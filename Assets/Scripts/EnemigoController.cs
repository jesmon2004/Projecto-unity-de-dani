using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    public float velocidad = 3f;

    void Update()
    {
        // Movimiento a velocidad constante hacia abajo 
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

    // Se destruye cuando desaparece por la parte inferior de la pantalla
        // (Ajusta el valor "-6f" dependiendo de tu cámara)
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    // Método para detectar colisiones (ambos objetos deben tener Trigger activado) 
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Chequeamos si el objeto con el que chocamos tiene el TAG "misil" 
        if (collision.CompareTag("misil"))
        {
            // Destruimos el misil 
            Destroy(collision.gameObject);
            
            // Destruimos este enemigo 
            Destroy(gameObject);
        }
    }
}