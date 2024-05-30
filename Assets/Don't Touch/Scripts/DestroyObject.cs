using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] bool active;
    private void Awake()
    {
        if (active)
            Destroy(gameObject);
    }
}
