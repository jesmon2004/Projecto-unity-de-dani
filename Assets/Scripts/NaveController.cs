using UnityEngine;

public class NaveController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 10f;
    private float limiteIzquierdo;
    private float limiteDerecho;

    //La variable dirección pasa a ser un atributo del script 
    public float direccionX = 0f;

    [Header("Configuración de Disparo")]
    // Definir dos atributos públicos para el misil y el punto de disparo 
    public GameObject misilPrefab;
    public Transform firePoint;

    SpriteRenderer spriteRenderer;
    private float spriteWidth;

    void Start()
    {
        CalcularLimitesPantalla();
    }

    void Update()
    {
        MoverNave();

        // Disparo por teclado con la barra espaciadora 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }
    }

    void CalcularLimitesPantalla()
    {
        //Tu lógica para que no se salga de los límites de la pantalla se mantiene intacta 
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.extents.x;
        
        Camera cam = Camera.main;
        float distanciaZ = transform.position.z - cam.transform.position.z;
        
        limiteIzquierdo = cam.ViewportToWorldPoint(new Vector3(0, 0, distanciaZ)).x + spriteWidth;
        limiteDerecho = cam.ViewportToWorldPoint(new Vector3(1, 0, distanciaZ)).x - spriteWidth;
    }

    void MoverNave()
    {
        //Mantenemos el movimiento por teclado, tal y como está ahora 
        float movTeclado = Input.GetAxisRaw("Horizontal");
        
        if (movTeclado != 0) 
        {
            direccionX = movTeclado;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            // Para que no se quede moviéndose sola cuando soltamos la tecla
            direccionX = 0f;
        }

        // Calcular nueva posición basándose en la direccionX
        float nuevaX = transform.position.x + (direccionX * velocidad * Time.deltaTime);

        // Limitar el movimiento para no salir de la pantalla
        nuevaX = Mathf.Clamp(nuevaX, limiteIzquierdo, limiteDerecho);

        // Aplicar posición
        transform.position = new Vector3(nuevaX, transform.position.y, transform.position.z);
    }

    // Método llamado desde los botones UI para mover la nave 
    public void setMovimiento(float x, float y)
    {
        direccionX = x;
    }

    // Método llamado desde los botones UI al levantar el dedo para parar la nave 
    public void pararMovimiento()
    {
        direccionX = 0f;
    }

    // Método encargado de implementar el disparo e instanciar el misil 
    public void fire()
    {
        Instantiate(misilPrefab, firePoint.position, Quaternion.identity); 
    }
}