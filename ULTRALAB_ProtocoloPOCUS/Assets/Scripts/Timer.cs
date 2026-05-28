using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI timerText;

    [Header("Configuração")]
    public int startHour = 8;
    public int endHour = 16;

    private int currentHour;
    private int currentMinute;

    private float secondCounter;

    void Start()
    {
        currentHour = startHour;
        currentMinute = 0;

        UpdateClockText();
    }

    void Update()
    {
        secondCounter += Time.deltaTime;

        // 1 segundo real = 1 minuto no jogo, da pra mudar depois
        if (secondCounter >= 1f)
        {
            secondCounter = 0f;

            AddMinute();
        }
    }

    void AddMinute()
    {
        currentMinute++;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour++;
        }

        UpdateClockText();

        // Final do plantão
        if (currentHour >= endHour && currentMinute >= 0)
        {
            Debug.Log("Plantão encerrado!");
            
            //Vamos colocar aqui o progresso que ele teve nos exames,etc
            enabled = false;
        }
    }

    void UpdateClockText()
    {
        timerText.text = $"{currentHour:00}:{currentMinute:00}";
    }
}