using UnityEngine;

public class NaveController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 10f;
    private float limiteIzquierdo;
    private float limiteDerecho;
    public float direccionX = 0f;

    [Header("Configuración de Disparo")]
    public GameObject misilPrefab;
    public Transform firePoint;

    SpriteRenderer spriteRenderer;
    private float spriteWidth;
    
    // Novedad PT3: Componente de audio
    private AudioSource audioSource;

    void Start()
    {
        CalcularLimitesPantalla();
        // Novedad PT3: Obtenemos el componente al empezar
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        MoverNave();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fire();
        }
    }

    void CalcularLimitesPantalla()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = spriteRenderer.bounds.extents.x;
        Camera cam = Camera.main;
        float distanciaZ = transform.position.z - cam.transform.position.z;
        limiteIzquierdo = cam.ViewportToWorldPoint(new Vector3(0, 0, distanciaZ)).x + spriteWidth;
        limiteDerecho = cam.ViewportToWorldPoint(new Vector3(1, 0, distanciaZ)).x - spriteWidth;
    }

    void MoverNave()
    {
        float movTeclado = Input.GetAxisRaw("Horizontal");
        if (movTeclado != 0) 
        {
            direccionX = movTeclado;
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            direccionX = 0f;
        }

        float nuevaX = transform.position.x + (direccionX * velocidad * Time.deltaTime);
        nuevaX = Mathf.Clamp(nuevaX, limiteIzquierdo, limiteDerecho);
        transform.position = new Vector3(nuevaX, transform.position.y, transform.position.z);
    }

    public void setMovimiento(float x, float y)
    {
        direccionX = x;
    }

    public void pararMovimiento()
    {
        direccionX = 0f;
    }

    public void fire()
    {
        Instantiate(misilPrefab, firePoint.position, Quaternion.identity);
        // Novedad PT3: Reproducimos el sonido
        audioSource.Play();
    }
}