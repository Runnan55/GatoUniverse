using UnityEngine;

public class PlayerController : MonoBehaviour
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
    }

    void FixedUpdate()
    {
        if (currentGravityWell != null)
        {
            currentGravityWell.ApplyGravity(rb);
            AlignToGravity();
        }

        MovePlayer();
        CheckGrounded();
    }

    void AlignToGravity()
    {
        if (currentGravityWell == null) return;

        // Dirección de la gravedad (del jugador hacia el centro de la esfera)
        Vector3 gravityDirection = (currentGravityWell.transform.position - transform.position).normalized;

        // Orientamos la parte superior del jugador en dirección contraria a la gravedad
        Vector3 playerUp = -gravityDirection;

        // Orientamos la parte delantera del jugador en la dirección de movimiento
        Vector3 forward = Vector3.Cross(transform.right, playerUp).normalized;

        // Aplicamos la rotación suavemente para evitar movimientos bruscos
        Quaternion targetRotation = Quaternion.LookRotation(forward, playerUp);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

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

        // Utilizamos SphereCast para detectar el suelo con más precisión
        isGrounded = Physics.SphereCast(transform.position, groundCheckRadius, -gravityDirection, out hit, groundCheckDistance);

        // Debugging visual: dibuja una línea para ver si el raycast funciona correctamente
        Debug.DrawRay(transform.position, -gravityDirection * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo cambiar la gravedad si tocamos un nuevo GravityWell
        GravityWell well = other.GetComponent<GravityWell>();
        if (well != null)
        {
            currentGravityWell = well;
            Debug.Log($"Cambiando gravedad a {currentGravityWell.gameObject.name}");
        }
    }
}
