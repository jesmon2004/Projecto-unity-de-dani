using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo
    public float interval = 2f;    // Cada cuánto tiempo sale un marciano

    void Start()
    {
        // Repite el método SpawnEnemy cada 'interval' segundos, empezando tras 1 segundo.
        InvokeRepeating("SpawnEnemy", 1f, interval);
    }

    void SpawnEnemy()
    {
        // Calculamos un límite X aleatorio basado en la cámara para que no salgan por fuera
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        float limitX = (width / 2f) - 0.5f; 

        float randomX = Random.Range(-limitX, limitX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        // Instanciamos el enemigo
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}