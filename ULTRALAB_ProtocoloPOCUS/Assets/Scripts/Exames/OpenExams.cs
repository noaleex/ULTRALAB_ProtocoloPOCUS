using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenExams : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject panelExam;
    [SerializeField] private string exams;

    [Header("Conduta")]
    [SerializeField] private NPC npcConduta;

    [Header("Cena Permitida")]
    [SerializeField] private string allowedScene;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        panelExam.SetActive(true);

        PauseController.SetPause(true);

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.InteractIcon.SetActive(false);

            if (PlayerReferences.Instance.playerMovement != null)
            {
                PlayerReferences.Instance.playerMovement.enabled = false;
            }
        }
    }

    public void ClosePanel()
    {
        panelExam.SetActive(false);

        PauseController.SetPause(false);

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.InteractIcon.SetActive(true);

            if (PlayerReferences.Instance.playerMovement != null)
            {
                PlayerReferences.Instance.playerMovement.enabled = true;
            }
        }
    }

    public void OpenExam()
    {
        SceneManager.LoadScene(exams);
        
    }

    public void OpenConduta()
    {
        if (SceneManager.GetActiveScene().name != allowedScene)
            return;

        panelExam.SetActive(false);
        if (npcConduta != null)
        {
            npcConduta.StartDialogueExternally();
        }
        
    }
}