using Assets.Scripts.VrToolkit;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour, IRaycastReceiver
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image background;
    [SerializeField] float fadeSpeed;
    private Coroutine fadeCoroutine;
    public void OnPointerEnter()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeUp());
    }

    public void OnPointerExit()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeDown());
    }

    private IEnumerator FadeUp()
    {
        float time = 0;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, time);
            background.color = new Color(background.color.r, background.color.g, background.color.b, time);
            if (time >= 1)
                break;
        }
    }
    private IEnumerator FadeDown()
    {
        float time = 1;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            time -= Time.deltaTime * fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, time);
            background.color = new Color(background.color.r, background.color.g, background.color.b, time);
            if (time <= 0)
                break;
        }
    }
}
