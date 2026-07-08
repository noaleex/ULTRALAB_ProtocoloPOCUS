using UnityEngine;
using UnityEngine.UI;

public class ImageAnexx : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image anexxImage;
    [SerializeField] private Image defaultImage;

    public void OnAnexx()
    {
        UpdateUI();

        AudioManager.Instance?.PlayBack();
    }

    public void UpdateUI()
    {
        if (ExamsSaveData.HasImage)
        {
            anexxImage.sprite = ExamsSaveData.SavedImage;
            anexxImage.color = Color.white;
        }
        else
        {
            anexxImage.sprite = defaultImage.sprite;
            anexxImage.color = new Color(1, 1, 1, 0);
        }
    }

    public void OnConfirm()
    {
        if (!ExamsSaveData.HasImage)
        {
            Debug.Log("Anexe uma imagem para confirmar.");
            return;
        }

        if (ExamsSaveData.IsDefaultUltrasoundImage)
        {
            Debug.Log("Imagem vazia do ultrassom.");
            return;
        }

        switch (ExamsSaveData.SavedExam)
        {
            case BodyArea.BodyRegion.Heart:
                Debug.Log("Imagem do Coração confirmada.");
                break;

            case BodyArea.BodyRegion.Lung1:
                Debug.Log("Imagem do Pulmão confirmada.");
                break;

            case BodyArea.BodyRegion.Lung2:
                Debug.Log("Imagem do Pulmão 2 confirmada.");
                break;

            case BodyArea.BodyRegion.Bladder:
                Debug.Log("Imagem da Bexiga confirmada.");
                break;

            default:
                Debug.Log("Anexe uma imagem para confirmar.");
                break;
        }
    }

    public void OnDeleteImage()
    {
        ExamsSaveData.Clear();

        UpdateUI();

        Debug.Log("Imagem apagada.");
    }

    public void OnUndoDelete()
    {
        ExamsSaveData.UndoDelete();

        UpdateUI();

        Debug.Log("Imagem restaurada.");
    }
}