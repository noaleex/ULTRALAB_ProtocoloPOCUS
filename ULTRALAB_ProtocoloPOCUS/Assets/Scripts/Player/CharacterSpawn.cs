using UnityEngine;
using Unity.Cinemachine;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabMasculino;
    [SerializeField] private GameObject prefabFeminino;

    [SerializeField] private Transform spawnPoint;

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

        if (characterEscolhido == "Feminino")
        {
            prefabParaInstanciar = prefabFeminino;
        }

        if (prefabParaInstanciar != null && spawnPoint != null)
        {
            GameObject novoPersonagem = Instantiate(
                prefabParaInstanciar,
                spawnPoint.position,
                spawnPoint.rotation
            );

            if (cinemachineCamera != null)
            {
                cinemachineCamera.Follow = novoPersonagem.transform;
            }
        }
    }
}