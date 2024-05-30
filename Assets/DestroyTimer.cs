using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    [SerializeField] float time;

    float currentTime;
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > time)
        {
            Destroy(gameObject);
        }
    }
}
