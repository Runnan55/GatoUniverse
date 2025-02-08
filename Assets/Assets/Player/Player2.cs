using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


[RequireComponent(typeof(BallMovementController))]
public class Player2 : MonoBehaviour
{
    private BallMovementController controller;

    private void Awake()
    {
        controller = GetComponent<BallMovementController>();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            controller.ActivateExtraGravity();
        }
        else if (Input.GetKeyUp(KeyCode.RightControl))
        {
            controller.DeactivateExtraGravity();
        }

        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.Keypad4)) direction.x = -1; // Izquierda
        else if (Input.GetKey(KeyCode.Keypad6)) direction.x = 1;  // Derecha
        if (Input.GetKey(KeyCode.Keypad8)) direction.y = 1;    // Arriba (Adelante)

        controller.SetInputDirection(direction);
    }

}
