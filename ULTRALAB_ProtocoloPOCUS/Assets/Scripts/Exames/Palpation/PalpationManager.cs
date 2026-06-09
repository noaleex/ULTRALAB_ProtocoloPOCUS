using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PalpationManager : MonoBehaviour
{
    public static PalpationManager Instance;
    public TMP_Text infoText;

    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    // Lista para salvar as regiões do corpo que já foram examinadas
    private List<BodyAreaP.BodyRegion> regioesDescobertas = new List<BodyAreaP.BodyRegion>();

    private void Awake()
    {
        Instance = this;

        if (raycaster == null) raycaster = FindFirstObjectByType<GraphicRaycaster>();
        if (eventSystem == null) eventSystem = FindFirstObjectByType<EventSystem>();
        
        // Texto sem nada
        if (infoText != null) infoText.text = ""; 
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }

        Vector2 mousePos = Mouse.current.position.ReadValue();
        CheckPalpation(mousePos);
    }

    private void CheckPalpation(Vector2 screenPosition)
    {
        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        if (results.Count == 0)
        {
            return;
        }

        foreach (RaycastResult result in results)
        {
            BodyAreaP area = result.gameObject.GetComponentInParent<BodyAreaP>();

            if (area == null)
            {
                area = result.gameObject.GetComponentInChildren<BodyAreaP>();
            }

            if (area != null)
            {
                if (!regioesDescobertas.Contains(area.region))
                {
                    regioesDescobertas.Add(area.region);
                    // O "\n" serve para pular linha
                    infoText.text += area.info + "\n" + "\n";

                }
                
                return; 
            }
        }
    }
}