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
    [SerializeField] private MedicalData medicalDataUI;

    [Header("Cena Permitida")]
    [SerializeField] private bool tutorialScene = false;

    [Header("Áudio")]
    public EventReference ClickSound;

    private bool conductCompleted = false;

    // Estado individual deste paciente
    private ConductState conductState =
        new ConductState();

    public bool ConductCompleted =>
        conductCompleted;

    public PatientData PatientDataReference =>
        patientData;

    public ConductState ConductState =>
        conductState;

    private void Start()
    {
        if (PatientManager.Instance != null)
        {
            PatientManager.Instance.RegisterPatient(this);

            patientData.welfareScore = 50;
        }
        else
        {
            Debug.LogError(
                "PatientManager não encontrado!"
            );
        }

        // Começa o dia com prontuário vazio
        conductState.Clear();
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

        // Define paciente atual
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
        
        CurrentPatient.Data = null;
        CurrentPatient.Object = null;

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

        // Define o paciente atual
        CurrentPatient.Data = patientData;
        CurrentPatient.Object = this;

        if (medicalDataUI != null)
        {
            medicalDataUI.LoadState(
                conductState
            );
        }
        else
        {
            Debug.LogError(
                $"MedicalData não foi atribuído no paciente " +
                $"{patientData.patientName}!"
            );
        }

        if (tutorialScene)
        {
            panelExam.SetActive(false);

            npcConduta.OnDialogueEnded =
                ReturnToExamPanel;

            npcConduta.StartDialogueExternally();
        }
        else
        {
            panelExam.SetActive(false);

            panelConduct.SetActive(true);

            PauseController.SetPause(false);
        }
    }

    public void SaveConductState()
    {
        if (medicalDataUI == null)
            return;

        conductState =
            medicalDataUI.GetCurrentState();

        Debug.Log(
            $"Conduta salva do paciente: " +
            $"{patientData.patientName}"
        );
    }

    public void CompleteConduct()
    {
        // Salva primeiro
        SaveConductState();

        conductCompleted = true;

        Debug.Log(
            $"Conduta do paciente " +
            $"{patientData.patientName} foi concluída."
        );
    }

    public void ResetConductForNewDay()
    {
        conductState.Clear();

        conductCompleted = false;

        Debug.Log(
            $"Conduta do paciente " +
            $"{patientData.patientName} foi resetada para o novo dia."
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