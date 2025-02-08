using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float jumpForce = 40f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 vel = other.GetComponent<Rigidbody>().linearVelocity;
            other.GetComponent<Rigidbody>().linearVelocity = new Vector3(vel.x, jumpForce, vel.z);
        }
    }
}
