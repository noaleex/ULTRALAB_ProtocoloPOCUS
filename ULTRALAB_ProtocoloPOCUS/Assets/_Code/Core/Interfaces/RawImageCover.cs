using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageCover : MonoBehaviour
{
    private RawImage rawImage;
    private RectTransform rectTransform;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        AdjustImage();
    }

    void OnEnable()
    {
        AdjustImage();
    }

    void OnRectTransformDimensionsChange()
    {
        AdjustImage();
    }

    private void AdjustImage()
    {
        if (rawImage == null || rawImage.texture == null || rectTransform == null) return;

        float containerRatio = rectTransform.rect.width / rectTransform.rect.height;

        float imageRatio = (float)rawImage.texture.width / rawImage.texture.height;

        float uvScaleX = 1f;
        float uvScaleY = 1f;
        float uvOffsetX = 0f;
        float uvOffsetY = 0f;

        if (imageRatio > containerRatio)
        {
            uvScaleX = containerRatio / imageRatio;
            uvOffsetX = (1f - uvScaleX) / 2f;
        }
        else
        {
            uvScaleY = imageRatio / containerRatio;
            uvOffsetY = (1f - uvScaleY) / 2f;
        }

        rawImage.uvRect = new Rect(uvOffsetX, uvOffsetY, uvScaleX, uvScaleY);
    }
}