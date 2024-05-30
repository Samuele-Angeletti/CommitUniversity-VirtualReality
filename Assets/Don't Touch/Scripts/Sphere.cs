using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    [SerializeField] List<GameObject> bolts;
    [SerializeField, Range(0.1f, 1)] float minTimer;
    [SerializeField, Range(0.1f, 5)] float maxTimer;
    float currentTime = 0;
    float timer;
    private void Start()
    {
        GetTimer();
    }
    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timer)
        {
            GetTimer();
            currentTime = 0;
            var notActivebolts = bolts.Where(x => !x.activeSelf).ToList();
            if (!notActivebolts.Any())
                return;

            var boltToActive = notActivebolts[Random.Range(0, notActivebolts.Count)];
            if (boltToActive != null)
            {
                boltToActive.SetActive(true);
            }
        }
    }

    public void GetTimer()
    {
        timer = Random.Range(minTimer, maxTimer);
    }
}
