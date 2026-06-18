using UnityEngine;
using UnityEngine.UI;

public class ImageAnexx : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private UltrasoundManager ultrasoundManager;

    [SerializeField] private Image anexxImage;

    public void OnSaveImage()
    {
        if (ultrasoundManager == null)
            return;

        ExamsSaveData.SavedImage =
            ultrasoundManager.resultImage.sprite;

        ExamsSaveData.HasNewImage = true;
    }

    public void OnAnexx()
    {
        if (!ExamsSaveData.HasNewImage)
            return;

        anexxImage.sprite =
        ExamsSaveData.SavedImage;
        anexxImage.color = Color.white;
        AudioManager.Instance?.PlayBack();
        
        ExamsSaveData.HasNewImage = false;
    }
}