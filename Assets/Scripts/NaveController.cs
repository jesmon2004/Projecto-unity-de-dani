using UnityEngine;

public class NaveController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 10f;
    private float limiteIzquierdo;
    private float limiteDerecho;

    SpriteRenderer spriteRenderer;

    //tamaño sprite
    private float spriteWidth;

    void Start()
    {
        
        CalcularLimitesPantalla();
    }

    void Update()
    {
        MoverNave();
    }

    void CalcularLimitesPantalla()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        // Obtener el tamaño del sprite (mitad ancho y alto)
        spriteWidth = spriteRenderer.bounds.extents.x;
        

        // Calculamos los límites del mundo basados en la cámara principal
        Camera cam = Camera.main;
        float distanciaZ = transform.position.z - cam.transform.position.z;
        
        // Esquina inferior izquierda (0,0) y superior derecha (1,1) en viewport
        limiteIzquierdo = cam.ViewportToWorldPoint(new Vector3(0, 0, distanciaZ)).x + spriteWidth;
        limiteDerecho = cam.ViewportToWorldPoint(new Vector3(1, 0, distanciaZ)).x - spriteWidth;
    }

    void MoverNave()
    {
        float inputHorizontal = 0f;

        //Entrada por Teclado (A/D o Flechas)
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        //Entrada por Pantalla Táctil o clic del ratón para pruebas
        if (Input.GetMouseButton(0)) 
        {
            // Dividimos la pantalla en dos mitades
            if (Input.mousePosition.x < Screen.width / 2)
                inputHorizontal = -1f; // Izquierda
            else
                inputHorizontal = 1f;  // Derecha
        }

        // Calcular nueva posición
        float nuevaX = transform.position.x + (inputHorizontal * velocidad * Time.deltaTime);

        //Limitar el movimiento
        nuevaX = Mathf.Clamp(nuevaX, limiteIzquierdo, limiteDerecho);

        // Aplicar posición
        transform.position = new Vector3(nuevaX, transform.position.y, transform.position.z);
    }
}