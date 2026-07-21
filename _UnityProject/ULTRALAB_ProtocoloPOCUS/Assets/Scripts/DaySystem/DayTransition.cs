using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayTransition : MonoBehaviour
{
    public static DayTransition Instance;

    [Header("UI")]
    [SerializeField] private Image blackPanel;
    [SerializeField] private TextMeshProUGUI dayText;

    [Header("Config")]
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float textTime = 1.5f;

    private void Awake()
    {
        Instance = this;
    }

    public void BeginDay(int day, Action onFinished)
    {
        StartCoroutine(DayRoutine(day, onFinished));
    }

    IEnumerator DayRoutine(int day, Action onFinished)
    {
        PauseController.SetPause(true);

        yield return Fade(1);

        dayText.text = $"Dia {day}. . .";
        dayText.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(textTime);

        yield return Fade(0);

        dayText.gameObject.SetActive(false);

        PauseController.SetPause(false);

        onFinished?.Invoke();
    }

    IEnumerator Fade(float target)
    {
        float start = blackPanel.color.a;

        float t = 0;

        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;

            Color c = blackPanel.color;
            c.a = Mathf.Lerp(start, target, t / fadeTime);

            blackPanel.color = c;

            yield return null;
        }

        Color final = blackPanel.color;
        final.a = target;
        blackPanel.color = final;
    }
}