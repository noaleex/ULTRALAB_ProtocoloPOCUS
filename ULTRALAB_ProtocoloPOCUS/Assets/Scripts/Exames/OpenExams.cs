using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class OpenExams : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject panelExam;
    [SerializeField] private string exams;

    [Header("Conduta")]
    [SerializeField] private NPC npcConduta;

    [Header("Cena Permitida")]
    [SerializeField] private string allowedScene;
    public EventReference ClickSound;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        PlayClickSound();
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
        PlayClickSound();
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
        PlayClickSound();
        SceneManager.LoadScene(exams);
        
    }

    public void OpenConduta()
    {
        PlayClickSound();
        if (SceneManager.GetActiveScene().name != allowedScene)
            return;

        panelExam.SetActive(false);
        if (npcConduta != null)
        {
            npcConduta.StartDialogueExternally();
        }
        
    }

    private void PlayClickSound()
    {
        if (!ClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(ClickSound, transform.position);
        }
    }
}