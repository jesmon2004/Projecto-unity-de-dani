using UnityEngine;

public class EnemigoController : MonoBehaviour
{
    public float velocidad = 3f;
    
   
    public GameObject explosionPrefab;

    void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("misil"))
        {
            // Destruimos el misil
            Destroy(collision.gameObject);
            
            //   Instanciamos la explosión en la posición actual del marciano
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            //  Sumamos 10 puntos usando el Singleton del ScoreManager
            ScoreManager.instance.AddScore(10);

            // Destruimos este enemigo
            Destroy(gameObject);
        }
    }
}