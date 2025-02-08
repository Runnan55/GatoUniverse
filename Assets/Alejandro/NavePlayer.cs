using UnityEngine;

public class NavePlayer : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset; // Desplazamiento de la c�mara respecto al jugador
    public float cameraSpeed = 10f; // Velocidad de suavizado de la c�mara

    private void LateUpdate()
    {
        // Calcular la posici�n deseada
        Vector3 desiredPosition = player.position + offset;

        /*// Interpolar suavemente hacia la posici�n restringida
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);*/

        // Asignar la posici�n final a la c�mara
        transform.position = player.position;
    }
}
