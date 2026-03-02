using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections; 
using TMPro; 
using UnityEngine.UI; // IMPORTANTE PARA LA BARRA DE VIDA

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
    public Transform firePointIzquierdo; 
    public Transform firePointDerecho;   
    private bool dobleDisparo = false;

    [Header("Sonidos y UI")]
    private AudioSource audioSource;
    public AudioClip sonidoDisparo; 
    public AudioClip sonidoDisparoDoble;
    public AudioClip sonidoPowerUp; 
    
    [Header("HUD PowerUp")]
    public GameObject hudPowerUp; 
    public TMP_Text textoTiempoPowerUp; 

    [Header("Sistema de Vidas")]
    public int vidas = 3; // Atributo público de vidas
    public Image barraVida; // Atributo público para la barra UI

    SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private Coroutine corrutinaPowerUp;

    void Start()
    {
        CalcularLimitesPantalla();
        audioSource = GetComponent<AudioSource>();
        if(hudPowerUp != null) hudPowerUp.SetActive(false); 
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
        if (movTeclado != 0) direccionX = movTeclado;
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) direccionX = 0f;

        float nuevaX = transform.position.x + (direccionX * velocidad * Time.deltaTime);
        nuevaX = Mathf.Clamp(nuevaX, limiteIzquierdo, limiteDerecho);
        transform.position = new Vector3(nuevaX, transform.position.y, transform.position.z);
    }

    public void setMovimiento(float x, float y) { direccionX = x; }
    public void pararMovimiento() { direccionX = 0f; }

    public void fire()
    {
        if (dobleDisparo)
        {
            Instantiate(misilPrefab, firePointIzquierdo.position, Quaternion.identity);
            Instantiate(misilPrefab, firePointDerecho.position, Quaternion.identity);
        }
        else
        {
            Instantiate(misilPrefab, firePoint.position, Quaternion.identity);
        }
        audioSource.PlayOneShot(sonidoDisparo);
    }

    // Nuevo método para recibir impactos
    public void RecibirImpacto(int cantidad)
    {
        vidas -= cantidad; // Restar vidas
        StartCoroutine(EfectoImpacto()); // Efecto de impacto

        if (vidas == 2)
        {
            barraVida.color = new Color(1f, 0.5f, 0f); // Naranja [cite: 285]
            barraVida.fillAmount = 0.66f; // Quita 1/3 [cite: 285]
        }
        else if (vidas == 1)
        {
            barraVida.color = Color.red; // Rojo [cite: 286]
            barraVida.fillAmount = 0.33f; // Quita 2/3 [cite: 286]
        }
        else if (vidas <= 0)
        {
            // Game Over
            if(barraVida != null) barraVida.gameObject.SetActive(false); // Elimina barra de vida
            Destroy(gameObject); // Destruir la nave
            SceneManager.LoadScene("GameOverScene");
        }
    }

    // Coroutine para el color de impacto
    IEnumerator EfectoImpacto()
    {
        spriteRenderer.color = Color.red; // Modificar a rojo
        yield return new WaitForSeconds(0.1f); // Esperar
        spriteRenderer.color = Color.white; // Volver normal
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            RecibirImpacto(3); // Destruye del todo al chocar con una nave enemiga (Game Over)
        }

        if (collision.CompareTag("PowerUp"))
        {
            audioSource.PlayOneShot(sonidoPowerUp);
            Destroy(collision.gameObject);
            if (corrutinaPowerUp != null)
            { 
                StopCoroutine(corrutinaPowerUp); 
            }
            corrutinaPowerUp = StartCoroutine(ActivarDobleDisparo(5f)); 
        }
    }

    IEnumerator ActivarDobleDisparo(float tiempo)
    {
        dobleDisparo = true;
        hudPowerUp.SetActive(true);
        float tiempoRestante = tiempo;

        while (tiempoRestante > 0)
        {
            textoTiempoPowerUp.text = tiempoRestante.ToString("F0") + "s";
            yield return new WaitForSeconds(1f);
            tiempoRestante--;
        }

        dobleDisparo = false;
        hudPowerUp.SetActive(false);
    }
}