using UnityEngine;

public class Player2 : MonoBehaviour
{
    private float planetGravity = 9.81f * 2f;
    public Vector3 gravityVector = Vector3.up * -1;
    public float moveForce = 200;

    private float gravityMult = 1;

    private Rigidbody rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Physics.gravity = planetGravity * gravityVector;
    }

    private void Update()
    {
        // Cambiar la gravedad con Right Shift (Shift derecho)
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            gravityMult = 8;
            UpdateGravity();
        }
        else if (Input.GetKeyUp(KeyCode.RightShift) && gravityMult == 8)
        {
            gravityMult = 1;
            UpdateGravity();
        }

        // Movimiento usando el teclado numérico
        float horizontal = 0f;
        float vertical = 0f;

        if (Input.GetKey(KeyCode.Keypad4)) horizontal = -1; // Izquierda
        if (Input.GetKey(KeyCode.Keypad6)) horizontal = 1;  // Derecha
        if (Input.GetKey(KeyCode.Keypad8)) vertical = 1;    // Arriba (Adelante)

        direction = new Vector2(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        // Aplica el movimiento basado en la dirección
        rb.AddForce((direction.x * Vector3.right + direction.y * Vector3.forward) * moveForce);
    }

    private void UpdateGravity()
    {
        Physics.gravity = planetGravity * gravityVector * gravityMult;
    }
}
