using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstruo_Inicio : MonoBehaviour
{
    //Una fila de enemigos se mueve de izquierda y vuelve a la posicion inicial y otra fila de enemigos se mueve de derecha y vuelve a la posicion inicial, esto se repite constantemente
    
    public float velocidad = 2f; // Velocidad de movimiento
    public float distancia = 3f; // Distancia a recorrer antes de cambiar de dirección
    private Vector3 posicionInicial; // Posición inicial del monstruo
    public bool moviendoDerecha = true; // Dirección de movimiento

    void Start()
    {
        posicionInicial = transform.position; // Guardamos la posición inicial
    }

    void Update()
    {
        // Calculamos la dirección de movimiento
        Vector3 direccion = moviendoDerecha ? Vector3.right : Vector3.left;

        // Movemos el monstruo
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Verificamos si hemos alcanzado la distancia máxima para cambiar de dirección
        if (Vector3.Distance(transform.position, posicionInicial) >= distancia)
        {
            moviendoDerecha = !moviendoDerecha; // Cambiamos la dirección
            posicionInicial = transform.position; // Actualizamos la posición inicial para el próximo ciclo
        }
    }

}
