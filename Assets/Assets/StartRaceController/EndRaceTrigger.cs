using UnityEngine;

public class EndRaceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartRaceController.instance.OnRaceFinished();
        }
    }
}
