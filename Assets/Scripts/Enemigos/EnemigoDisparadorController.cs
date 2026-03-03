using UnityEngine;
using System.Collections;

public class EnemigoDisparadorController : MonoBehaviour
{
    public float velocidadDescenso = 2f;
    public float amplitudZigZag = 2f;
    public float frecuenciaZigZag = 2f;
    private float posicionXInicial;

    public GameObject misilEnemigoPrefab;
    public Transform firePoint;
    public GameObject explosionPrefab;
    public int puntos = 20;

    public GameObject powerUpPrefab; // Arrastra aquí el prefab de la estrella

    void Start()
    {
        posicionXInicial = transform.position.x;
        StartCoroutine(DispararRutina()); // Usa coroutine para disparos temporales
    }

    void Update()
    {
        // Movimiento Zig-Zag: Desciende y cambia horizontalmente
        float nuevaX = posicionXInicial + Mathf.Sin(Time.time * frecuenciaZigZag) * amplitudZigZag;
        float nuevaY = transform.position.y - velocidadDescenso * Time.deltaTime;
        transform.position = new Vector3(nuevaX, nuevaY, transform.position.z);

        // Destruir si sale de pantalla
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddScore(-50); 
        }
    }

    IEnumerator DispararRutina()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.5f, 3f)); // Cada X tiempo dispara
            Instantiate(misilEnemigoPrefab, firePoint.position, Quaternion.identity); // Instancia el misil
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("misil"))
        {
            // Sumar puntos y destruir
            if (ScoreManager.instance != null) ScoreManager.instance.AddScore(puntos); 
            if (explosionPrefab != null) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            
            //  20% de probabilidad de soltar el PowerUp
            if (Random.Range(0, 100) < 20) //Acuerdate de ponerlo 20
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}