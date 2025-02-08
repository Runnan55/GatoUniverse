using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private float extraGravityForce = 30f;
    private bool enabledExtraGravity = false;

    private float movementForce = 20f;
    private Vector2 inputDirection = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateExtraGravity()
    {
        enabledExtraGravity = true;
    }
    public void DeactivateExtraGravity() 
    {
        enabledExtraGravity = false; 
    }

    public void SetInputDirection(Vector2 inputDirection)
    {
        this.inputDirection = inputDirection;
    }


    private void FixedUpdate()
    {
        if (enabledExtraGravity) 
        {
            rb.AddForce(Vector3.up * -1 * extraGravityForce);
        }
        if (inputDirection != Vector2.zero) 
        {
            rb.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * movementForce);
        }
    }


}
