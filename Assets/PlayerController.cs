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
            Debug.LogWarning("No hay ningún GravityWell asignado. El jugador está en el vacío.");
        }

        MovePlayer();
        CheckGrounded();
    }

    void AlignToGravity()
    {
        if (currentGravityWell == null) return;

        Vector3 gravityDirection = (currentGravityWell.transform.position - transform.position).normalized;
        Vector3 playerUp = -gravityDirection;
        Vector3 forward = Vector3.Cross(transform.right, playerUp).normalized;

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
