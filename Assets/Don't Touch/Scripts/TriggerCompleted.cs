using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TriggerCompleted : MonoBehaviour
{
    [SerializeField] int boolCount;
    int currentCount = 0;
    CameraController controller;
    private void Awake()
    {
        controller = FindObjectOfType<CameraController>();
    }
    public void Trigger()
    {
        currentCount++;
        if (currentCount >= boolCount)
        {
            controller.End();
        }
    }
}
