using UnityEngine;
using System.Collections; // Necesario para Corrutinas

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float interval = 2f;

    void Start()
    {
        // SUSTITUIMOS EL INVOKE POR LA CORRUTINA
        StartCoroutine(GenerarEnemigos());
    }

    IEnumerator GenerarEnemigos()
    {
        // Esperamos 1 segundo antes de empezar a spawnear
        yield return new WaitForSeconds(1f);

        // Bucle infinito que funciona paralelo al juego
        while (true)
        {
            SpawnEnemy();
            // Pausa la ejecución de este bucle durante los segundos del intervalo
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

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}