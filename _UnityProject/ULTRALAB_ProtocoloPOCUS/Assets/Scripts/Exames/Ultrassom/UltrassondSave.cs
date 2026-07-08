using UnityEngine;

public class UltrassondSave : MonoBehaviour
{
    [SerializeField] private UltrasoundManager ultrasoundManager;

    public void OnSaveImage()
    {
        if (ultrasoundManager == null)
        {
            Debug.LogError("UltrasoundManager não foi atribuído.");
            return;
        }

        if (ultrasoundManager.resultImage == null)
        {
            Debug.LogError("ResultImage não foi atribuída.");
            return;
        }

        if (ultrasoundManager.resultImage.sprite == null)
        {
            Debug.Log("Nenhuma imagem para salvar.");
            return;
        }

        // Não salva a imagem padrão
        if (ultrasoundManager.resultImage.sprite == ultrasoundManager.defaultImage)
        {
            Debug.Log("Nenhum exame válido para salvar.");
            return;
        }

        ExamsSaveData.Save(
            ultrasoundManager.resultImage.sprite,
            ultrasoundManager.CurrentRegion
        );

        ExamsSaveData.IsDefaultUltrasoundImage =
            ultrasoundManager.resultImage.sprite == ultrasoundManager.defaultImage;

        Debug.Log($"Imagem salva: {ExamsSaveData.SavedExam}");
    }
}
