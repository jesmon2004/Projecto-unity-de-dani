using UnityEngine;

public class MisilController : MonoBehaviour
{
    public float velocidad = 8f;

    void Update()
    {
        // Movimiento a velocidad constante hacia arriba 
        transform.Translate(Vector3.up * velocidad * Time.deltaTime);

        // Se destruye cuando desaparece por la parte superior 
        // (Ajusta el valor "6f" dependiendo del tamaño de tu cámara en Unity)
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }
}