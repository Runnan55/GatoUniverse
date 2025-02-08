using UnityEngine;

public class NavePlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador
    public float cameraSpeed = 10f; // Velocidad de suavizado de la cámara

    private void LateUpdate()
    {
        // Calcular la posición deseada
        Vector3 desiredPosition = player.position + offset;

        /*// Interpolar suavemente hacia la posición restringida
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);*/

        // Asignar la posición final a la cámara
        transform.position = player.position;
    }
}
