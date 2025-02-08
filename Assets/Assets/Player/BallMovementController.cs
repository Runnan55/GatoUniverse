using UnityEngine;

public class BallMovementController : MonoBehaviour
{
    private bool canControl = false;

    private Rigidbody rb;
    public float extraGravityForce = 30f;
    private bool enabledExtraGravity = false;

    private float passiveForce = 10.0f;
    private float passiveVelocityMult = 0.8f;
    private float currentPassiveMult = 1.0f;
    private float movementForce = 20f;
    private Vector2 inputDirection = Vector2.zero;
    private float maxForwardVelocity = 30;
    private float maxHorizontalVelocity = 60;

    private float boostVelocityMultiplier = 3;
    private float boostDuration = 2;
    private float currentBoostDuration = 0;
    private bool boosting = false;

    private float slowVelocityMultiplier = 0.5f;
    private float slowDuration = 2;
    private float currentSlowDuration = 0;

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

    private void Update()
    {
        if (!boosting && currentBoostDuration > 0) 
        {
            currentBoostDuration = Mathf.Max(currentBoostDuration - Time.deltaTime, 0);
        }
        if (currentSlowDuration > 0)
        {
            currentSlowDuration = Mathf.Max(currentSlowDuration - Time.deltaTime, 0);
        }
    }

    private void FixedUpdate()
    {
        if (enabledExtraGravity && canControl) 
        {
            rb.AddForce(Vector3.up * -1 * extraGravityForce);
        }
        if (inputDirection != Vector2.zero && canControl) 
        {
            if (inputDirection.y != 0)
                rb.AddForce(new Vector3(inputDirection.x, 0, inputDirection.y) * movementForce);
            else if (inputDirection.y == 0)
                rb.AddForce(new Vector3(inputDirection.x * movementForce, 0, passiveForce));
        }

        if (rb.linearVelocity.z > CurrentMaxForwardVelocity())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, CurrentMaxForwardVelocity());
        }
        if (Mathf.Abs(rb.linearVelocity.x) > maxHorizontalVelocity)
        {
            rb.linearVelocity = new Vector3(Mathf.Sign(rb.linearVelocity.x), rb.linearVelocity.y, rb.linearVelocity.z);
        }
    }

    private float CurrentMaxForwardVelocity()
    {
        if (inputDirection.y > 0)
            currentPassiveMult = 1;
        else
            currentPassiveMult = passiveVelocityMult;

        return maxForwardVelocity * currentPassiveMult *
            Mathf.Lerp(1, boostVelocityMultiplier, currentBoostDuration / boostDuration) * 
            Mathf.Lerp(1, slowVelocityMultiplier, currentSlowDuration / slowDuration);
    }

    public void Boost()
    {
        boosting = true;
        currentBoostDuration = boostDuration;
    }
    public void StopBoost()
    {
        boosting = false;
    }

    public void ActiveSlow()
    {
        currentSlowDuration = slowDuration;
    }

    public void ActiveMovement()
    {
        rb.isKinematic = false;
    }

    public void EnableControl()
    {
        canControl = true;
    }

    public void DisableControl()
    {
        canControl = false;
        DeactivateExtraGravity();
    }
}
