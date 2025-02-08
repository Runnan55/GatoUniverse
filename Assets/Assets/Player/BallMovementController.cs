using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    private Rigidbody rb;
    private float extraGravityForce = 30f;
    private bool enabledExtraGravity = false;

    private float movementForce = 20f;
    private Vector2 inputDirection = Vector2.zero;
    private float currentMaxForwardVelocity;
    private float maxForwardVelocity = 30;
    private float maxHorizontalVelocity = 60;

    private float maxBoostedForwardVelocity = 150;
    private float boostDuration = 2;
    private float currentBoostDuration = 0;
    private bool boosting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentMaxForwardVelocity = maxForwardVelocity;
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

    private void Update()
    {
        if (!boosting && currentBoostDuration > 0) 
        {
            currentBoostDuration = Mathf.Max(currentBoostDuration - Time.deltaTime, 0);
            currentMaxForwardVelocity = Mathf.Lerp(maxBoostedForwardVelocity, maxForwardVelocity, 1f - currentBoostDuration / boostDuration);
        }
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

        if (rb.linearVelocity.z > currentMaxForwardVelocity)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, currentMaxForwardVelocity);
        }
        if (Mathf.Abs(rb.linearVelocity.x) > maxHorizontalVelocity)
        {
            rb.linearVelocity = new Vector3(Mathf.Sign(rb.linearVelocity.x), rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

    public void Boost()
    {
        boosting = true;
        currentBoostDuration = boostDuration;
        currentMaxForwardVelocity = maxBoostedForwardVelocity;
        print("Start boosting");
    }
    public void StopBoost()
    {
        boosting = false;
        print("Stop boosting");
    }
}
