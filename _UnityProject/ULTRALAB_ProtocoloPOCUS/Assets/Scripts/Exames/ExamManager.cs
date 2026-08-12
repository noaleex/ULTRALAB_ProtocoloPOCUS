using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image characterFullImage;
    [SerializeField] private TMP_Text infoText;

    [Header("Tutorial")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private GameObject table;

    private void Start()
    {
        if (CurrentPatient.Data == null)
        {
            return;
        }

        characterImage.sprite = CurrentPatient.Data.characterSprite;
        characterFullImage.sprite = CurrentPatient.Data.characterFullSprite;
        //infoText.text = CurrentPatient.Data.caso;

        if (CurrentPatient.Data.tutorial)
        {
            table.SetActive(false);
            backgroundImage.sprite = CurrentPatient.Data.backgroundSprite;
        }
    }
}