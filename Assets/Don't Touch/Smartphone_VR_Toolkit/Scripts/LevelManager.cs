using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject errorPanel;
    [SerializeField] TextMeshProUGUI errorText;
    public static LevelManager Instance;

    [SerializeField] GameObject loaderCanvas;
    [SerializeField] Image progressBar;
    List<string> scenes;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        scenes = new List<string>();
    }

    public void CheckAndLoad(string sceneName)
    {
        if (!scenes.Contains(sceneName))
            LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        scenes = sceneName.Split(',').Select(x => x.Trim()).ToList();

        loaderCanvas.SetActive(true);

        List<AsyncOperation> operations = new()
        {
            SceneManager.LoadSceneAsync(scenes[0])
        };
        if (scenes.Count > 1)
        {
            for (int i = 1; i < scenes.Count; i++)
            {
                operations.Add(SceneManager.LoadSceneAsync(scenes[i], LoadSceneMode.Additive));
            }
        }

        while (operations.Any(x => !x.isDone))
        {
            progressBar.fillAmount = Mathf.Clamp01(operations.Last().progress / 0.9f);
            yield return new WaitForEndOfFrame();
        }

        scenes.Clear();
        loaderCanvas.SetActive(false);
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogCallback;
    }

    //Called when there is an exception
    public void LogCallback(string condition, string stackTrace, LogType type)
    {
        /*
        if (type == LogType.Error || type == LogType.Exception)
        {
            errorPanel.SetActive(true);
            errorText.text = condition + " - " + stackTrace;
        }
        */
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogCallback;
    }
}
