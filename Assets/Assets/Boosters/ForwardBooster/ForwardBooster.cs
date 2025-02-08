using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class ForwardBooster : MonoBehaviour
{
    [SerializeField] float boostFoce = 20f;

    private List<GameObject> balls = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            balls.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            balls.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (balls.Count > 0) 
        {
            foreach (var ball in balls)
            {
                ball.GetComponent<Rigidbody>().AddForce(transform.forward * boostFoce);
            }
        }
    }
}
