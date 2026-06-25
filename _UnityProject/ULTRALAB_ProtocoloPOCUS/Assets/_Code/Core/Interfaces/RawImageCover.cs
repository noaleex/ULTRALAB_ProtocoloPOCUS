using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class RawImageCover : MonoBehaviour
{
    private RawImage rawImage;
    private RectTransform rectTransform;

    private Vector2 lastSize;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
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

        float containerWidth = rectTransform.rect.width;
        float containerHeight = rectTransform.rect.height;

        if (containerHeight == 0f || rawImage.texture.height == 0) return;

        Vector2 currentSize = new Vector2(containerWidth, containerHeight);
        if(currentSize == lastSize) return;

        lastSize = currentSize;

        float containerRatio = containerWidth / containerHeight;
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