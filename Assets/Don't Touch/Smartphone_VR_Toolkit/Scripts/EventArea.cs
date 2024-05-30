using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventArea : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerExit;
    [SerializeField] UnityEvent extraEvent;
    [SerializeField] bool delay = false;
    [SerializeField] float delayTime;
    Player player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (delay)
            {
                StartCoroutine(DelayAction(onTriggerEnter));
            }
            else
                onTriggerEnter?.Invoke();
        }
    }

    private IEnumerator DelayAction(UnityEvent value)
    {
        yield return new WaitForSeconds(delayTime);
        value.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (delay)
            {
                StartCoroutine(DelayAction(onTriggerExit));
            }
            else
                onTriggerExit?.Invoke();
        }
    }

    public void TestPlayerConditionOnEnter(int quantity)
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        if (player.CollectionMatch(quantity))
        {
            if (delay)
            {
                StartCoroutine(DelayAction(extraEvent));
            }
            else
                extraEvent?.Invoke();
        }
    }
}
