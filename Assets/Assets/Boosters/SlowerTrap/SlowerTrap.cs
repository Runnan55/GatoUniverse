using NUnit.Framework;
using UnityEngine;

public class SlowerTrap : MonoBehaviour
{
    static BallMovementController[] players = new BallMovementController[2];

    private void Start()
    {
        if (players[0] == null)
        {
            BallMovementController[] balls = FindObjectsByType<BallMovementController>(FindObjectsSortMode.None);

            players[0] = balls[0];
            players[1] = balls[1];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == players[0].gameObject)
        {
            players[1].ActiveSlow();
        }
        else if (other.gameObject == players[1].gameObject)
        {
            players[0].ActiveSlow();
        }
    }
}
