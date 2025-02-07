using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 5, -10); // Posici�n relativa a jugador
    public float cameraSpeed = 5f; // Velocidad de suavizado
    public float zoomOutDistance = 10f; // Distancia a alejar en Z cuando se presiona Space
    private bool isZoomedOut = false; // Estado de la c�mara
    private Vector3 targetOffset; // Offset din�mico

    private void Start()
    {
        targetOffset = offset; // Inicializar el offset en su posici�n normal
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // Detectar si el jugador presiona Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isZoomedOut = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isZoomedOut = false;
        }

        // Modificar el offset si est� en modo zoom out
        targetOffset = isZoomedOut ? offset + new Vector3(0, 0, -zoomOutDistance) : offset;

        // Suavizar la transici�n de la c�mara
        Vector3 desiredPosition = player.position + targetOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, cameraSpeed * Time.deltaTime);

        // Mirar siempre al jugador
        transform.LookAt(player);
    }
}
