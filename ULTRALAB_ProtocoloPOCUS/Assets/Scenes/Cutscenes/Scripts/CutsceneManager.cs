using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager Instance {get; private set;}

    [Header("UI Management")]
    [SerializeField] private GameObject[] uiGroupsToHide;
    private Dictionary<GameObject, bool> originalUIState = new Dictionary<GameObject, bool>();

    [Header("Switch Cutscene")]
    [SerializeField] private GameObject cutsceneFeminina;
    [SerializeField] private GameObject cutsceneMaculina;
    private GameObject cutsceneAtiva;

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

    private void Start()
    {
        if (cutsceneFeminina != null) cutsceneFeminina.SetActive(false);
        if (cutsceneMaculina != null) cutsceneMaculina.SetActive(false);

        string genero = PlayerPrefs.GetString("Character", "");

        if (genero == "Masculino")
        {
            cutsceneAtiva = cutsceneMaculina;
        }
        else
        {
            cutsceneAtiva = cutsceneFeminina;
        }

        if (cutsceneAtiva != null)
        {
            cutsceneAtiva.SetActive(true);
        }
        else
        {
            FinalizarEInstanciarPlayer();
        }
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

        FinalizarEInstanciarPlayer();
    }

    private void FinalizarEInstanciarPlayer ()
    {
        CharacterSpawn spawner = FindFirstObjectByType<CharacterSpawn>();
        if (spawner != null)
        {
            spawner.SpawnPlayer();
        }
        
        if (cutsceneAtiva != null)
        {
            Destroy(cutsceneAtiva);
        }
    }
}
