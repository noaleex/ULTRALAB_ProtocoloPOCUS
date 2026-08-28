using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UltrassondSave : MonoBehaviour
{
    [SerializeField] private UltrasoundManager ultrasoundManager;
    [SerializeField] private TextMeshPro confirmText;

    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float textTime = 1.5f;
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

    private IEnumerator Fade(
        float target)
    {
       //confirmText 
       float start =
            confirmText.color.a;

        float t = 0f;

        while (t < fadeTime)
        {
            t +=
                Time.unscaledDeltaTime;

            Color color =
                confirmText.color;

            color.a =
                Mathf.Lerp(
                    start,
                    target,
                    t / fadeTime
                );

            confirmText.color =
                color;

            yield return null;
        }

        Color finalColor =
            confirmText.color;

        finalColor.a =
            target;

        confirmText.color =
            finalColor;
    }
}