using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public float velocidad = 3f;

    void Update()
    {
        transform.Translate(Vector3.down * velocidad * Time.deltaTime);

        if (transform.position.y < -6f) // Ajusta si tu cámara es más grande
        {
            Destroy(gameObject);
        }
    }
}