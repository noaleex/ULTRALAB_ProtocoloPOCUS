using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Image targetImage;

    [Header("Brilho")]
    [SerializeField] private Slider ganhoSlider;

    [Header("Zoom")]
    [SerializeField] private Slider realZoomSlider; 
    [SerializeField] private float minZoom = 1f;   
    [SerializeField] private float maxZoom = 3f;  

    private RectTransform imageRect;
    private ScrollRect scrollRect;

    private void Start()
    {
        imageRect = targetImage.rectTransform;
        
        // Pega o Scroll Rect
        scrollRect = targetImage.GetComponentInParent<ScrollRect>();

        imageRect.pivot = new Vector2(0.5f, 0.5f);

        ganhoSlider.onValueChanged.AddListener(AlterarBrilho);
        realZoomSlider.onValueChanged.AddListener(AlterarZoom);
        
        AlterarZoom(realZoomSlider.value);
        AlterarBrilho(ganhoSlider.value);
    }

    private void AlterarBrilho(float valor)
    {
        targetImage.color = new Color(valor, valor, valor, 1f);
    }

    private void AlterarZoom(float valor)
    {
        float fatorZoom = Mathf.Lerp(minZoom, maxZoom, valor);
        imageRect.localScale = new Vector3(fatorZoom, fatorZoom, 1f);

        if (scrollRect != null)
        {
            scrollRect.enabled = (fatorZoom > minZoom);
            
            if (fatorZoom <= minZoom)
            {
                imageRect.anchoredPosition = Vector2.zero;
            }
        }
    }
}