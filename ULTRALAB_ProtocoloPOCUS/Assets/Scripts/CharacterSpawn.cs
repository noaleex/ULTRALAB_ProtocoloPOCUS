using UnityEngine;
using Unity.Cinemachine; 

public class CharacterSpawn : MonoBehaviour
{
    [Header("Prefabs dos Personagens")]
    [SerializeField] private GameObject prefabMasculino;
    [SerializeField] private GameObject prefabFeminino;

    [Header("Ponto de Nascimento")]
    [SerializeField] private Transform spawnPoint;

    [Header("Configuração da Câmera")]
    [SerializeField] private CinemachineCamera cinemachineCamera; 

    void Start()
    {
        
        string characterEscolhido = PlayerPrefs.GetString("Character", "");

        print("Spawnado o Personagem: " + characterEscolhido);

        GameObject prefabParaInstanciar = null;

        if (characterEscolhido == "Masculino")
        {
            prefabParaInstanciar = prefabMasculino;
        }
        else if (characterEscolhido == "Feminino")
        {
            prefabParaInstanciar = prefabFeminino;
        }

        if (prefabParaInstanciar != null && spawnPoint != null)
        {
            GameObject novoPersonagem = Instantiate(prefabParaInstanciar, spawnPoint.position, spawnPoint.rotation);
            
            if (cinemachineCamera != null)
            {
                cinemachineCamera.Follow = novoPersonagem.transform;
            }
        }
    }
}