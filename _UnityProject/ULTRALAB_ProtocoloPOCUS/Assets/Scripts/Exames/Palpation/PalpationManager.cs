using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PalpationManager : MonoBehaviour
{
    public static PalpationManager Instance;

    public TMP_Text infoText;

    private List<ButtonBodyArea.BodyRegion> regioesDescobertas = new();

    private void Awake()
    {
        Instance = this;

        if (infoText != null)
            infoText.text = "";

        regioesDescobertas.Clear();
    }

    public void AddInfo(ButtonBodyArea.BodyRegion region)
    {
        if (CurrentPatient.Data == null)
        {
            Debug.LogWarning("Nenhum paciente selecionado.");
            return;
        }

        if (regioesDescobertas.Contains(region))
            return;

        regioesDescobertas.Add(region);

        string info = GetInfoFromRegion(region);

        if (!string.IsNullOrEmpty(info))
        {
            infoText.text += info + "\n\n";
        }
    }

    private string GetInfoFromRegion(ButtonBodyArea.BodyRegion region)
    {
        foreach (PhysicalExamInfo examInfo in CurrentPatient.Data.physicalExam)
        {
            if (examInfo.region == region)
            {
                return examInfo.info;
            }
        }

        Debug.LogWarning("Nenhuma informação encontrada para a região: " + region);

        return "";
    }
}