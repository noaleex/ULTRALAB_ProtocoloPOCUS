using UnityEngine;
using Unity.Cinemachine;

public class CharacterSpawn : MonoBehaviour
{
    [SerializeField] private GameObject prefabMasculino;
    [SerializeField] private GameObject prefabFeminino;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private CinemachineCamera cinemachineCamera;

    private void Start()
    {
        if (GameManager.Instance.HasPlayer())
        {
            if (cinemachineCamera != null)
            {
                cinemachineCamera.Follow =
                    GameManager.Instance.Player.transform;
            }

            return;
        }

        string characterEscolhido =
            PlayerPrefs.GetString("Character", "");

        GameObject prefabParaInstanciar = null;

        if (characterEscolhido == "Masculino")
        {
            prefabParaInstanciar = prefabMasculino;
        }

        if (characterEscolhido == "Feminino")
        {
            prefabParaInstanciar = prefabFeminino;
        }

        if (prefabParaInstanciar == null)
            return;

        GameObject novoPersonagem = Instantiate(
            prefabParaInstanciar,
            spawnPoint.position,
            spawnPoint.rotation
        );

        DontDestroyOnLoad(novoPersonagem);

        GameManager.Instance.RegisterPlayer(novoPersonagem);

        if (cinemachineCamera != null)
        {
            cinemachineCamera.Follow =
                novoPersonagem.transform;
        }
    }
}