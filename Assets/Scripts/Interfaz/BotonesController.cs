using UnityEngine;
using UnityEngine.EventSystems; // Importante para las interfaces de Pointer

public class BotonesController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Atributos para interaccionar con la nave 
    public NaveController nave; 
    public float direccionX;
    public float direccionY;

    // Cuando se pulsa el botón 
    public void OnPointerDown(PointerEventData eventData)
    {
        nave.setMovimiento(direccionX, direccionY);
    }

    // Cuando se levanta el dedo del botón 
    public void OnPointerUp(PointerEventData eventData)
    {
        nave.pararMovimiento();
    }
}