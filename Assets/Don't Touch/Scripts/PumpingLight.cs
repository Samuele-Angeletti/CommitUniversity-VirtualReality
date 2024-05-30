using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpingLight : MonoBehaviour
{
    Light lightComp;
    [SerializeField] float speed;
    [SerializeField] float minRange;
    float maxRange;
    float modifier = -1;
    bool goingUp;
    private void Awake()
    {
        lightComp = GetComponent<Light>();
        maxRange = lightComp.range;
        goingUp =  true;
    }

    private void Update()
    {
        if (goingUp)
        {
            if (lightComp.range >= maxRange)
            {
                modifier = -1;
                goingUp = false;
            }
        }
        else if (!goingUp)
        {
            if (lightComp.range <= minRange)
            {
                modifier = 1;
                goingUp = true;
            }
        }

        lightComp.range += modifier * speed * Time.deltaTime;
    }
}
