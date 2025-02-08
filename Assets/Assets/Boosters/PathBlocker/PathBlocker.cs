using UnityEngine;

public class PathBlocker : MonoBehaviour
{
    [SerializeField] private Transform blockerTransform;
    private Vector3 initialPosition;
    [SerializeField] private Vector3 finalPosition;
    private bool moving = false;
    [SerializeField] private float animationTime = 0.5f;
    private float animationCurrentTime = 0;

    private void Awake()
    {
        initialPosition = blockerTransform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            moving = true;
        }
    }


    private void Update()
    {
        if (moving) 
        {
            animationCurrentTime = Mathf.Min(animationCurrentTime + Time.deltaTime, animationTime);
            blockerTransform.localPosition = Vector3.Lerp(initialPosition, finalPosition, animationCurrentTime / animationTime);
            if (animationCurrentTime >= animationTime)
            {
                moving = false;
            }
        }
    }


}
