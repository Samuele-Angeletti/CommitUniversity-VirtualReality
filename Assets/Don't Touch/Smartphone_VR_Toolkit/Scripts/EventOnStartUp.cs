using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventOnStartUp : MonoBehaviour
{
    [SerializeField] bool setPlayerPosition = true;
    [SerializeField] Vector3 playerPos = Vector3.zero;
    private void Start()
    {
        if (setPlayerPosition)
        {
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.transform.position = playerPos;
            }
        }
    }
}
