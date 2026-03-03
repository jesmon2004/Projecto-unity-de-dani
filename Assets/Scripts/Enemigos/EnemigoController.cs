using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemigoController : MonoBehaviour
{
    public float velocidad = 3f;
    public GameObject explosionPrefab;
    public GameObject powerUpPrefab; // Arrastra aquí el prefab de la estrella

    void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddScore(-50); 
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("misil"))
        {
            Destroy(collision.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            ScoreManager.instance.AddScore(10);
            
            //  20% de probabilidad de soltar el PowerUp
            if (Random.Range(0, 100) < 20)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}