using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExamManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TMP_Text infoText;

    private void Start()
    {
        if (CurrentPatient.Data == null)
            return;

        characterImage.sprite = CurrentPatient.Data.characterSprite;
        backgroundImage.sprite = CurrentPatient.Data.backgroundSprite;
        infoText.text = CurrentPatient.Data.caso;
    }
}