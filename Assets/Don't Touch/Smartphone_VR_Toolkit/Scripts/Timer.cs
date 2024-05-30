using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] TextMeshProUGUI textTime;

    [SerializeField] UnityEvent onTimerEnd;
    [SerializeField] bool endPlayer = false;
    [SerializeField] bool changeScene = false;
    [SerializeField] string sceneName;
    [SerializeField] bool parse;
    float currentTime;
    Player player;
    bool active = true;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        currentTime = time;
    }
    private void Update()
    {
        if (!active)
            return;

        if (currentTime <= 0)
            return;

        currentTime -= Time.deltaTime;
        if (textTime != null)
        {
            string text = parse ? TimeSpan.FromSeconds(currentTime).ToString("mm':'ss") : $"{(int)currentTime}";
            textTime.text = text;
        }

        if (currentTime <= 0)
        {
            onTimerEnd.Invoke();

            if (changeScene)
            {
                LevelManager.Instance.CheckAndLoad(sceneName);
                return;
            }

            if (endPlayer)
            {
                player.End();
            }
        }
    }

    public void ResetTimer()
    {
        currentTime = time;
    }

    public void StopTimer()
    {
        active = false;
    }
}
