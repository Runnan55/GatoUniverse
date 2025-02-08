using UnityEngine;

[RequireComponent(typeof(BallMovementController))]
public class Player1 : MonoBehaviour
{
    private BallMovementController controller;

    private void Awake()
    {
        controller = GetComponent<BallMovementController>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            controller.ActivateExtraGravity();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            controller.DeactivateExtraGravity();
        }

        controller.SetInputDirection(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
    }

}
