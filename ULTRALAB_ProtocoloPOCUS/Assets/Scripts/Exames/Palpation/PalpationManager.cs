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

        if(infoText != null)
            infoText.text = "";
    }


    public void AddInfo(ButtonBodyArea.BodyRegion region, string info)
    {
        if (regioesDescobertas.Contains(region))
            return;


        regioesDescobertas.Add(region);

        infoText.text += info + "\n\n";
    }
}