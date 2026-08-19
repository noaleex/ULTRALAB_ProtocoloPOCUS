using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class Arrows : MonoBehaviour
{
    [Header("Páginas")]
    [SerializeField] private GameObject[] pages;
    [SerializeField] private int currentPageIndex = 0;
    [SerializeField] private GameObject conductPanel;

    [Header("Referências")]
    [SerializeField] private MedicalData medicalDataUI;
    [SerializeField] private PatientConductEvaluator conductEvaluator;

    [Header("FMOD - Sons")]
    [SerializeField] private EventReference somClick;

    public void UpPage()
    {
        if (pages == null ||
            pages.Length == 0)
            return;

        pages[currentPageIndex]
            .SetActive(false);

        currentPageIndex =
            (currentPageIndex + 1)
            % pages.Length;

        pages[currentPageIndex]
            .SetActive(true);
    }

    public void DownPage()
    {
        if (pages == null ||
            pages.Length == 0)
            return;

        pages[currentPageIndex]
            .SetActive(false);

        currentPageIndex =
            (currentPageIndex - 1 + pages.Length)
            % pages.Length;

        pages[currentPageIndex]
            .SetActive(true);
    }

    public void ConfirmConduct()
    {
        TocarSomClick();

        if (CurrentPatient.Data == null ||
            CurrentPatient.Object == null)
        {
            Debug.LogError(
                "Nenhum paciente selecionado!"
            );

            return;
        }

        if (CurrentPatient.Object.ConductCompleted)
        {
            Debug.LogWarning(
                $"A conduta do paciente " +
                $"{CurrentPatient.Data.patientName} " +
                $"já foi realizada."
            );

            return;
        }

        if (medicalDataUI == null)
        {
            Debug.LogError(
                "MedicalData não foi atribuído no Arrows!"
            );

            return;
        }

        if (!medicalDataUI.ValidateAllFields())
        {
            Debug.LogWarning(
                "Por favor, preencha todos os campos antes de confirmar!"
            );

            return;
        }

        // AVALIAR
        conductEvaluator.EvaluatePatient(
            CurrentPatient.Data,
            medicalDataUI
        );

        // SALVAR ESTADO DO PACIENTE
        CurrentPatient.Object
            .CompleteConduct();

        Debug.Log(
            $"Conduta de {CurrentPatient.Data.patientName} concluída."
        );
    }

    public void CloseConduct()
    {
        TocarSomClick();

        if (CurrentPatient.Object != null)
        {
            CurrentPatient.Object.SaveConductState();
        }

        if (medicalDataUI != null)
        {
            medicalDataUI.ResetForm();
        }

        conductPanel.SetActive(false);

        PauseController.SetPause(false);

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.EnablePlayer();
        }

        currentPageIndex = 0;

        CurrentPatient.Data = null;
        CurrentPatient.Object = null;
    }

    private void TocarSomClick()
    {
        if (!somClick.IsNull)
        {
            RuntimeManager.PlayOneShot(
                somClick
            );
        }
    }
}