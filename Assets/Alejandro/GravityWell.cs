using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public float gravityStrength = 9.81f; // Intensidad de la gravedad
    private Rigidbody targetRb; // Guarda el Rigidbody del jugador u objeto afectado

    private void FixedUpdate()
    {
        // Aplica gravedad solo si hay un objeto dentro del Trigger
        if (targetRb != null)
        {
            ApplyGravity(targetRb);
        }
    }

    public void ApplyGravity(Rigidbody rb)
    {
        Vector3 gravityDirection = (transform.position - rb.position).normalized;
        rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetRb = other.attachedRigidbody;
            Debug.Log($"Jugador entró en la gravedad de {gameObject.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.attachedRigidbody == targetRb)
            {
                targetRb = null; // Quita la gravedad cuando el jugador sale
                Debug.Log($"Jugador salió de la gravedad de {gameObject.name}");
            }
        }
    }
}
