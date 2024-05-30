using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody body;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    public void Initialize(Vector3 direction)
    {
        body.AddForce(direction * 10, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
