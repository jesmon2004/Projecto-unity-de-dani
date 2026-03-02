using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena
using System.Collections; // Necesario para las Corrutinas
using TMPro; // Necesario para la UI

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
    public Transform firePointIzquierdo; // NUEVO
    public Transform firePointDerecho;   // NUEVO
    private bool dobleDisparo = false;

    [Header("Sonidos y UI")]
    private AudioSource audioSource;
    public AudioClip sonidoDisparo; // Arrastra tu láser aquí
    public AudioClip sonidoDisparoDoble;
    public AudioClip sonidoPowerUp; // Arrastra tu sonido de estrella aquí
    
    [Header("HUD PowerUp")]
    public GameObject hudPowerUp; // Arrastra tu PanelPowerUp
    public TMP_Text textoTiempoPowerUp; // Arrastra tu texto "5s"

    SpriteRenderer spriteRenderer;
    private float spriteWidth;

    private Coroutine corrutinaPowerUp;

    void Start()
    {
        CalcularLimitesPantalla();
        audioSource = GetComponent<AudioSource>();
        if(hudPowerUp != null) hudPowerUp.SetActive(false); // Nos aseguramos de ocultarlo
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
        // Lógica del PowerUp
        if (dobleDisparo)
        {
            Instantiate(misilPrefab, firePointIzquierdo.position, Quaternion.identity);
            Instantiate(misilPrefab, firePointDerecho.position, Quaternion.identity);
        }
        else
        {
            Instantiate(misilPrefab, firePoint.position, Quaternion.identity);
        }
        
        // Usamos PlayOneShot para que no se corten los sonidos si disparamos rápido o cogemos la estrella
        audioSource.PlayOneShot(sonidoDisparo);
    }

    // GESTIÓN DE COLISIONES
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Colisión con el enemigo (Game Over)
        if (collision.CompareTag("Enemy"))
        {
            // Reiniciamos la escena (Puedes cambiarlo por "GameOver" si creas una escena para ello)
            SceneManager.LoadScene("GameOverScene"); 
        }

        // 2. Colisión con el PowerUp
        if (collision.CompareTag("PowerUp"))
        {
            audioSource.PlayOneShot(sonidoPowerUp);
            Destroy(collision.gameObject);

            // Si ya hay una corrutina de tiempo funcionando, la detenemos
            if (corrutinaPowerUp != null)
            {
                StopCoroutine(corrutinaPowerUp);
            }

            //Iniciamos una corrutina nueva y la guardamos en nuestra variable
            corrutinaPowerUp = StartCoroutine(ActivarDobleDisparo(5f)); 
        }
    }

    // CORRUTINA DE TIEMPO DE POWER UP
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