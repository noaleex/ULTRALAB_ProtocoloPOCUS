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

        if (ultrasoundManager.resultImage == null ||
            ultrasoundManager.resultImage.sprite == null)
        {
            Debug.Log("Nenhuma imagem para salvar.");
            return;
        }

        bool isDefault =
            ultrasoundManager.resultImage.sprite == ultrasoundManager.defaultImage;

        ExamsSaveData.Save(
            ultrasoundManager.resultImage.sprite,
            isDefault
                ? BodyArea.BodyRegion.Empty
                : ultrasoundManager.CurrentRegion
        );

        ExamsSaveData.IsDefaultUltrasoundImage = isDefault;

        Debug.Log($"Imagem salva: {ExamsSaveData.SavedExam}");
    }
}