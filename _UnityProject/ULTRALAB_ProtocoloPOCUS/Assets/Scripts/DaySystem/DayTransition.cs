using System;
using System.Collections;
using System.Collections.Generic;
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

    private IEnumerator DayRoutine(
        int day,
        Action onFinished)
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

    public void CheckPatientsAtEndOfDay(
        Action onFinished)
    {
        StartCoroutine(
            CheckPatientsRoutine(onFinished)
        );
    }

    private IEnumerator CheckPatientsRoutine(
        Action onFinished)
    {
        PauseController.SetPause(true);

        if (PatientManager.Instance == null)
        {
            Debug.LogError(
                "PatientManager não encontrado!"
            );

            yield break;
        }

        if (!PatientManager.Instance.AllPatientsCompletedConduct())
        {
            dayText.text =
                "Realize a conduta de todos os pacientes para terminar o dia";

            dayText.gameObject.SetActive(true);

            yield return new WaitForSecondsRealtime(
                textTime
            );

            dayText.gameObject.SetActive(false);

            PauseController.SetPause(false);

            yield break;
        }

        yield return Fade(1);

        IReadOnlyList<OpenExams> patients =
            PatientManager.Instance.GetPatients();

        bool gameOver = false;

        List<OpenExams> patientsToDestroy =
            new List<OpenExams>();

        foreach (OpenExams patientObject in patients)
        {
            if (patientObject == null)
                continue;

            PatientData patient =
                patientObject.PatientDataReference;

            if (patient == null)
                continue;

            // PACIENTE GANHOU ALTA
            if (patient.welfareScore >= 74)
            {
                dayText.text =
                    $"O paciente {patient.patientName} ganhou alta";

                dayText.gameObject.SetActive(true);

                yield return new WaitForSecondsRealtime(
                    textTime
                );

                patientsToDestroy.Add(
                    patientObject
                );
            }

            // PACIENTE PIOROU
            else if (patient.welfareScore <= 0)
            {
                dayText.text =
                    $"O paciente {patient.patientName} piorou, game over";

                dayText.gameObject.SetActive(true);

                yield return new WaitForSecondsRealtime(
                    textTime
                );

                gameOver = true;

                break;
            }

            // PACIENTE CONTINUA
            else
            {
                dayText.text =
                    $"O paciente {patient.patientName} continua em tratamento";

                dayText.gameObject.SetActive(true);

                yield return new WaitForSecondsRealtime(
                    textTime
                );
            }
        }

        // DESTRUIR PACIENTES QUE TIVERAM ALTA
        foreach (OpenExams patient in patientsToDestroy)
        {
            if (patient != null)
            {
                Destroy(patient.gameObject);
            }
        }

        // GAME OVER
        if (gameOver)
        {
            GameOver();
            yield break;
        }

        // PRÓXIMO DIA
        dayText.gameObject.SetActive(false);

        yield return Fade(0);

        PauseController.SetPause(false);

        onFinished?.Invoke();
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
        // SceneManager.LoadScene("GameOver");
    }

    private IEnumerator Fade(float target)
    {
        float start = blackPanel.color.a;

        float t = 0;

        while (t < fadeTime)
        {
            t += Time.unscaledDeltaTime;

            Color c = blackPanel.color;

            c.a = Mathf.Lerp(
                start,
                target,
                t / fadeTime
            );

            blackPanel.color = c;

            yield return null;
        }

        Color final = blackPanel.color;

        final.a = target;

        blackPanel.color = final;
    }
}