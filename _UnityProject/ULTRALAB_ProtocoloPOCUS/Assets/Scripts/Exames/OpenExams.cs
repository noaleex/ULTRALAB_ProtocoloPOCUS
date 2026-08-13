using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class OpenExams : MonoBehaviour, IInteractable
{
    [Header("Exames")]
    [SerializeField] private GameObject panelExam;
    [SerializeField] private GameObject panelConduct;
    [SerializeField] private string exams;
    [SerializeField] private PatientData patientData;

    [Header("Conduta")]
    [SerializeField] private NPC npcConduta;

    [Header("Cena Permitida")]
    [SerializeField] private string allowedScene;

    [Header("Áudio")]
    public EventReference ClickSound;

    private bool conductCompleted = false;

    public bool ConductCompleted => conductCompleted;

    public PatientData PatientDataReference => patientData;

    private void Start()
    {
        if (PatientManager.Instance != null)
        {
            PatientManager.Instance.RegisterPatient(this);
        }
        else
        {
            Debug.LogError(
                $"PatientManager não encontrado para o paciente {name}."
            );
        }
    }

    private void OnDestroy()
    {
        if (PatientManager.Instance != null)
        {
            PatientManager.Instance.UnregisterPatient(this);
        }
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        PlayClickSound();

        panelExam.SetActive(true);
        PauseController.SetPause(true);

        CurrentPatient.Data = patientData;
        CurrentPatient.Object = this;

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.DisablePlayer();

            if (PlayerReferences.Instance.InteractIcon != null)
            {
                PlayerReferences.Instance.InteractIcon.SetActive(false);
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
            PlayerReferences.Instance.EnablePlayer();

            if (PlayerReferences.Instance.InteractIcon != null)
            {
                PlayerReferences.Instance.InteractIcon.SetActive(true);
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

        if (SceneManager.GetActiveScene().name == allowedScene)
        {
            panelExam.SetActive(false);
            panelConduct.SetActive(true);

            if (npcConduta != null)
            {
                npcConduta.OnDialogueEnded = ReturnToExamPanel;
                npcConduta.StartDialogueExternally();
            }
        }
    }

    public void CompleteConduct()
    {
        conductCompleted = true;

        Debug.Log(
            $"Conduta do paciente {patientData.patientName} foi concluída."
        );
    }

    public void ReturnToExamPanel()
    {
        if (PlayerReferences.Instance?.InteractionDetector != null)
        {
            PlayerReferences.Instance.InteractionDetector
                .ForceInteractable(this);
        }
    }

    private void PlayClickSound()
    {
        if (!ClickSound.IsNull)
        {
            RuntimeManager.PlayOneShot(
                ClickSound,
                transform.position
            );
        }
    }
}