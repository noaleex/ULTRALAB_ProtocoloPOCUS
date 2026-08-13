using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Configuração")]
    [SerializeField] private int startHour = 8;
    [SerializeField] private int endHour = 16;

    public int CurrentDay { get; private set; } = 1;

    private int currentHour;
    private int currentMinute;

    private float secondCounter;
    private bool timerRunning;

    private void Start()
    {
        currentHour = startHour;
        currentMinute = 0;

        UpdateClockText();

        DayTransition.Instance.BeginDay(
            CurrentDay,
            StartDay
        );
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        secondCounter += Time.deltaTime;

        if (secondCounter >= 1f)
        {
            secondCounter = 0f;

            AddMinute();
        }
    }

    private void AddMinute()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        UpdateClockText();

        if (currentHour >= endHour)
        {
            timerRunning = false;

            EndDay();
        }
    }

    private void EndDay()
    {
        DayTransition.Instance.CheckPatientsAtEndOfDay(
            NextDay
        );
    }

    private void NextDay()
    {
        CurrentDay++;

        currentHour = startHour;
        currentMinute = 0;

        UpdateClockText();

        DayTransition.Instance.BeginDay(
            CurrentDay,
            StartDay
        );
    }

    private void StartDay()
    {
        secondCounter = 0;
        timerRunning = true;
    }

    public void SkipToNextDay()
    {
        if (!timerRunning)
            return;

        timerRunning = false;

        EndDay();
    }

    private void UpdateClockText()
    {
        timerText.text =
            $"{currentHour:00}:{currentMinute:00}";
    }
}