using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class Arrows : MonoBehaviour
{
    [Header("Páginas")]
    public GameObject[] pages;
    public int currentPageIndex = 0;
    public GameObject conductPanel;

    [Header("Referências")]
    public MedicalData medicalDataUI;

    [Header("FMOD - Sons")]
    [SerializeField] private EventReference somClick;

    public void UpPage()
    {
        if (pages.Length == 0)
            return;

        pages[currentPageIndex].SetActive(false);

        currentPageIndex =
            (currentPageIndex + 1) % pages.Length;

        pages[currentPageIndex].SetActive(true);
    }

    public void DownPage()
    {
        if (pages.Length == 0)
            return;

        pages[currentPageIndex].SetActive(false);

        currentPageIndex =
            (currentPageIndex - 1 + pages.Length) % pages.Length;

        pages[currentPageIndex].SetActive(true);
    }

    public void ConfirmConduct()
    {
        TocarSomClick();

        if (CurrentPatient.Data == null ||
            CurrentPatient.Object == null)
        {
            Debug.LogError("Nenhum paciente selecionado!");
            return;
        }

        if (CurrentPatient.Object.ConductCompleted)
        {
            Debug.LogWarning(
                $"A conduta do paciente " +
                $"{CurrentPatient.Data.patientName} já foi realizada."
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

        PatientData npcData = CurrentPatient.Data;

        CheckPatientData(npcData);

        CurrentPatient.Object.CompleteConduct();
    }

    public void CloseConduct()
    {
        TocarSomClick();

        conductPanel.SetActive(false);

        PauseController.SetPause(false);
        PlayerReferences.Instance.EnablePlayer();

        currentPageIndex = 0;
    }

    private void TocarSomClick()
    {
        if (!somClick.IsNull)
        {
            RuntimeManager.PlayOneShot(somClick);
        }
    }

    private bool CheckPatientData(PatientData npc)
    {
        int acertos = 0;
        int erros = 0;

        // =========================
        // VIA AÉREA
        // =========================

        Contabilizar(
            medicalDataUI.permeabilidadeDropdown
                .options[medicalDataUI.permeabilidadeDropdown.value].text,
            npc.permeabilidade,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.presencaDropdown
                .options[medicalDataUI.presencaDropdown.value].text,
            npc.presenca,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.intervencaoDropdown
                .options[medicalDataUI.intervencaoDropdown.value].text,
            npc.intervencao,
            ref acertos,
            ref erros
        );

        // =========================
        // RESPIRAÇÃO
        // =========================

        Contabilizar(
            medicalDataUI.frequenciaInput.text,
            npc.frequencia,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.saturationInput.text,
            npc.saturation,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.acessoriaDropdown
                .options[medicalDataUI.acessoriaDropdown.value].text,
            npc.acessoria,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.padraoRespiratorioDropdown
                .options[medicalDataUI.padraoRespiratorioDropdown.value].text,
            npc.padraoRespiratorio,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.ascultaDropdown
                .options[medicalDataUI.ascultaDropdown.value].text,
            npc.asculta,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.expansibilidadeDropdown
                .options[medicalDataUI.expansibilidadeDropdown.value].text,
            npc.expansibilidade,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.oxigenoterapiaTipoDropdown
                .options[medicalDataUI.oxigenoterapiaTipoDropdown.value].text,
            npc.oxigenoterapiaTipo,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.oxigenoterapiaFluxoInput.text,
            npc.oxigenoterapiaFluxo,
            ref acertos,
            ref erros
        );

        // =========================
        // CIRCULAÇÃO
        // =========================

        Contabilizar(
            medicalDataUI.frequenciaCardiacaInput.text,
            npc.frequenciaCardiaca,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.pressaoArterialInput.text,
            npc.pressaoArterial,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.pressaoArterial2Input.text,
            npc.pressaoArterial2,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.perfusaoTempoInput.text,
            npc.perfusaoTempo,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.perfusaoExtremidadesDropdown
                .options[medicalDataUI.perfusaoExtremidadesDropdown.value].text,
            npc.perfusaoExtremidades,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.pulsosDropdown
                .options[medicalDataUI.pulsosDropdown.value].text,
            npc.pulsos,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.ritmoCardiacoDropdown
                .options[medicalDataUI.ritmoCardiacoDropdown.value].text,
            npc.ritmoCardiaco,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.edemaDropdown
                .options[medicalDataUI.edemaDropdown.value].text,
            npc.edema,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.temperaturaInput.text,
            npc.temperatura,
            ref acertos,
            ref erros
        );

        // =========================
        // AVALIAÇÃO NEUROLÓGICA
        // =========================

        Contabilizar(
            medicalDataUI.nivelConscienciaDropdown
                .options[medicalDataUI.nivelConscienciaDropdown.value].text,
            npc.nivelConsciencia,
            ref acertos,
            ref erros
        );

        // =========================
        // EXPOSIÇÃO
        // =========================

        Contabilizar(
            medicalDataUI.avalicaoPeleDropdown
                .options[medicalDataUI.avalicaoPeleDropdown.value].text,
            npc.avalicaoPele,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.presencaPeleDropdown
                .options[medicalDataUI.presencaPeleDropdown.value].text,
            npc.presencaPele,
            ref acertos,
            ref erros
        );

        Contabilizar(
            medicalDataUI.dorEscalaInput.text,
            npc.dorEscala,
            ref acertos,
            ref erros
        );

        // =========================
        // WELFARE
        // =========================

        int novoWelfare =
            npc.welfareScore + acertos - erros;

        npc.welfareScore =
            Mathf.Clamp(novoWelfare, 0, 74);

        Debug.Log(
            $"Paciente: {npc.patientName}\n" +
            $"Acertos: {acertos}\n" +
            $"Erros: {erros}\n" +
            $"Novo Welfare: {npc.welfareScore}"
        );

        return erros == 0;
    }

    private void Contabilizar(
        string valorUI,
        string valorCorreto,
        ref int acertos,
        ref int erros)
    {
        if (valorUI == valorCorreto)
            acertos++;
        else
            erros++;
    }
}