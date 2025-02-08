using UnityEngine;

public class Player1 : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            gravityMult = 8;
            UpdateGravity();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && gravityMult == 8)
        {
            gravityMult = 1;
            UpdateGravity();
        }

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gravityMult = 0.5f;
            UpdateGravity();
        }
        else if (Input.GetKeyUp(KeyCode.Space) && gravityMult == 0.5f)
        {
            gravityMult = 1;
            UpdateGravity();
        }*/


    }

    private void FixedUpdate()
    {
        //float anglediff = Vector2.Dot(rb.linearVelocity, direction);

        rb.AddForce((direction.x * Vector3.right + direction.y * Vector3.forward) * moveForce);

    }


    private void UpdateGravity()
    {
        Physics.gravity = planetGravity * gravityVector * gravityMult;
    }
}
