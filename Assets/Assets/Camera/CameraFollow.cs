using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0, 0, -10);

    private void Update()
    {
        transform.position = playerTransform.position + cameraOffset;
    }
}
