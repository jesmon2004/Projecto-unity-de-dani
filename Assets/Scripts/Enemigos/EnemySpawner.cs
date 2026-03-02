using UnityEngine;
using System.Collections; 

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyShooterPrefab; // Añadimos el nuevo enemigo
    public float interval = 2f;

    void Start()
    {
        StartCoroutine(GenerarEnemigos());
    }

    IEnumerator GenerarEnemigos()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(interval);
        }
    }

    void SpawnEnemy()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        float limitX = (width / 2f) - 0.5f; 

        float randomX = Random.Range(-limitX, limitX);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        // Elegir aleatoriamente qué enemigo sale
        if (Random.value > 0.6f) 
        {
            Instantiate(enemyShooterPrefab, spawnPosition, Quaternion.identity);
        }
        else 
        {
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}