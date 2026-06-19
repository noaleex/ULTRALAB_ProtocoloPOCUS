using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableProbe : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;

        UltrasoundManager.Instance.CheckProbePosition(
            rectTransform.position
        );
    }
}