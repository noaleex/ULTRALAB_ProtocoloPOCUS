using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using FMODUnity;

public class PalpationManager : MonoBehaviour
{
    public static PalpationManager Instance;
    public TMP_Text infoText;

    // Agora você DEVE arrastar o GraphicRaycaster e o EventSystem aqui pelo Inspector da Unity
    [SerializeField] private GraphicRaycaster raycaster;
    [SerializeField] private EventSystem eventSystem;

    private List<BodyAreaP.BodyRegion> regioesDescobertas = new List<BodyAreaP.BodyRegion>();

    private void Awake()
    {
        Instance = this;
        
        if (infoText != null) infoText.text = ""; 
    }

    public void Click(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }

        // Usando Pointer.current para garantir compatibilidade com Toque (Celular) e Clique (PC)
        if (Pointer.current != null)
        {
            Vector2 pointerPos = Pointer.current.position.ReadValue();
            CheckPalpation(pointerPos);
        }
    }

    private void CheckPalpation(Vector2 screenPosition)
    {
        // Se algum dos componentes obrigatórios estiver faltando, interrompe para evitar erros
        if (eventSystem == null || raycaster == null) return;

        PointerEventData pointerData = new PointerEventData(eventSystem);
        pointerData.position = screenPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        if (results.Count == 0) return;

        foreach (RaycastResult result in results)
        {
            BodyAreaP area = result.gameObject.GetComponentInParent<BodyAreaP>();

            if (area == null)
            {
                area = result.gameObject.GetComponentInChildren<BodyAreaP>();
            }

            if (area != null)
            {
                if (!area.clickSound.IsNull)
                {
                    RuntimeManager.PlayOneShot(area.clickSound);
                }
                if (!regioesDescobertas.Contains(area.region))
                {
                    regioesDescobertas.Add(area.region);
                    infoText.text += area.info + "\n\n";
                }
                
                return; 
            }
        }
    }
}