using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance {get; private set;}

    [Header("UI Management")]
    [SerializeField] private GameObject[] uiGroupsToHide;

    private Dictionary<GameObject, bool> originalUIState = new Dictionary<GameObject, bool>();

    [Header("Estrutura de Cena")]
    [SerializeField] private Transform cutscenesReoot;

    [SerializeField] private Transform actorsRoot;
    [SerializeField] private Transform vcamRoot;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }

    public void OnCutsceneStart ()
    {
        Debug.Log("[CutsceneManager] Iniciando Cutscene. Ocultando interfaces...");
        originalUIState.Clear();

        foreach (GameObject uiGroup in uiGroupsToHide)
        {
            if (uiGroup != null)
            {
                originalUIState[uiGroup] = uiGroup.activeSelf;
                uiGroup.SetActive(false);
            }
        }
    }

    public void OnCutsceneEnd ()
    {
        Debug.Log("[CutsceneManager] Cutscene concluída. Reativando Interfaces...");

        foreach (GameObject uiGroup in uiGroupsToHide)
        {
            if (uiGroup != null && originalUIState.ContainsKey(uiGroup))
            {
                uiGroup.SetActive(originalUIState[uiGroup]); 
            }
        }
    }
}
