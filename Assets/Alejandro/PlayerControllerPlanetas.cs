using UnityEngine;

public class PlayerControllerPlanetas : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float jumpForce = 7f;
    public float groundCheckDistance = 1.2f;
    public float groundCheckRadius = 0.5f;

    private Rigidbody rb;
    private GravityWell currentGravityWell;
    private bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Intentar encontrar un GravityWell cercano al iniciar
        GravityWell initialWell = FindFirstObjectByType<GravityWell>();

        if (initialWell != null)
        {
            currentGravityWell = initialWell;
            Debug.Log($"Gravedad inicial establecida en {currentGravityWell.gameObject.name}");
        }
    }

    void FixedUpdate()
    {
        if (currentGravityWell != null)
        {
            currentGravityWell.ApplyGravity(rb);
            AlignToGravity();
        }
        else
        {
            Debug.LogWarning("No hay ning�n GravityWell asignado. El jugador est� en el vac�o.");
        }

        MovePlayer();
        CheckGrounded();
    }

    void AlignToGravity()
    {
        if (currentGravityWell == null) return;

        // Direcci�n de la gravedad (hacia el centro del planeta)
        Vector3 gravityDirection = (currentGravityWell.transform.position - transform.position).normalized;
        Vector3 playerUp = -gravityDirection;

        // Mantener la rotaci�n del jugador en relaci�n con la gravedad
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, playerUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (currentGravityWell == null) return; // Asegurar que hay un GravityWell asignado

        // Obtener la direcci�n de la gravedad basada en el planeta
        Vector3 gravityUp = (transform.position - currentGravityWell.transform.position).normalized;

        // Obtener las direcciones locales alineadas con la superficie correctamente
        Vector3 localForward = Vector3.ProjectOnPlane(transform.forward, gravityUp).normalized;
        Vector3 localRight = Vector3.Cross(gravityUp, localForward).normalized;

        // Direcciones de movimiento ajustadas a la curvatura del planeta
        Vector3 moveDirection = (localForward * vertical + localRight * horizontal).normalized;

        // Aplicar el movimiento
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // Saltar si est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }




    void Jump()
    {
        if (currentGravityWell == null) return;

        Vector3 jumpDirection = (transform.position - currentGravityWell.transform.position).normalized;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void CheckGrounded()
    {
        if (currentGravityWell == null) return;

        Vector3 gravityDirection = (transform.position - currentGravityWell.transform.position).normalized;
        RaycastHit hit;

        isGrounded = Physics.SphereCast(transform.position, groundCheckRadius, -gravityDirection, out hit, groundCheckDistance);
        Debug.DrawRay(transform.position, -gravityDirection * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        GravityWell well = other.GetComponent<GravityWell>();
        if (well != null)
        {
            currentGravityWell = well;
            Debug.Log($"Cambiando gravedad a {currentGravityWell.gameObject.name}");
        }
    }
}
