using UnityEngine;
using TMPro;

public class Arrows : MonoBehaviour
{
    [Header("Páginas")]
    public GameObject[] pages;
    public int currentPageIndex = 0;
    public GameObject conductPanel; // Painel de conduta

    [Header("Referências")]
    public MedicalData medicalDataUI; // Arraste o objeto que contém o script MedicalData no Inspector

    public void UpPage()
    {
        if (pages.Length == 0) return;

        pages[currentPageIndex].SetActive(false);
        currentPageIndex = (currentPageIndex + 1) % pages.Length; // Navegação limpa em loop
        pages[currentPageIndex].SetActive(true);
    }

    public void DownPage()
    {
        if (pages.Length == 0) return;

        pages[currentPageIndex].SetActive(false);
        currentPageIndex = (currentPageIndex - 1 + pages.Length) % pages.Length; // Navegação limpa em loop
        pages[currentPageIndex].SetActive(true);
    }

    /// <summary>
    /// Compara os dados inseridos na UI com os dados reais do ScriptableObject do NPC atual.
    /// </summary>
    public void ConfirmConduct()
    {
        if (CurrentPatient.Data == null)
        {
            Debug.LogError("Nenhum paciente selecionado em CurrentPatient.Data!");
            return;
        }

        if (!medicalDataUI.ValidateAllFields())
        {
            Debug.LogWarning("Por favor, preencha todos os campos antes de confirmar!");
            return;
        }

        PatientData npcData = CurrentPatient.Data;

        bool isCorrect = CheckPatientData(npcData);
    }

    public void CloseConduct()
    {
        conductPanel.SetActive(false);
        PauseController.SetPause(false);
        currentPageIndex = 0;
    }

    /// <summary>
    /// Realiza a comparação campo a campo entre a UI e o ScriptableObject do NPC.
    /// </summary>
    private bool CheckPatientData(PatientData npc)
    {
        // Via Aérea
        if (medicalDataUI.permeabilidadeDropdown.options[medicalDataUI.permeabilidadeDropdown.value].text != npc.permeabilidade) return false;
        if (medicalDataUI.presencaDropdown.options[medicalDataUI.presencaDropdown.value].text != npc.presenca) return false;
        if (medicalDataUI.intervencaoToggle.isOn != npc.intervenção) return false;

        // Respiração
        if (medicalDataUI.frequenciaInput.text != npc.frequencia) return false;
        if (medicalDataUI.saturationInput.text != npc.saturation) return false;
        if (medicalDataUI.acessoriaToggle.isOn != npc.acessoria) return false;
        if (medicalDataUI.padraoRespiratorioDropdown.options[medicalDataUI.padraoRespiratorioDropdown.value].text != npc.padraoRespiratorio) return false;
        if (medicalDataUI.ascultaDropdown.options[medicalDataUI.ascultaDropdown.value].text != npc.asculta) return false;
        if (medicalDataUI.expansibilidadeDropdown.options[medicalDataUI.expansibilidadeDropdown.value].text != npc.expansibilidade) return false;
        if (medicalDataUI.oxigenoterapiaTipoDropdown.options[medicalDataUI.oxigenoterapiaTipoDropdown.value].text != npc.oxigenoterapiaTipo) return false;
        if (medicalDataUI.oxigenoterapiaFluxoInput.text != npc.oxigenoterapiaFluxo) return false;

        // Circulação
        if (medicalDataUI.frequenciaCardiacaInput.text != npc.frequenciaCardiaca) return false;
        if (medicalDataUI.pressaoArterialInput.text != npc.pressaoArterial) return false;
        if (medicalDataUI.pressaoArterial2Input.text != npc.pressaoArterial2) return false;
        if (medicalDataUI.perfusaoTempoInput.text != npc.perfusaoTempo) return false;
        if (medicalDataUI.perfusaoExtremidadesDropdown.options[medicalDataUI.perfusaoExtremidadesDropdown.value].text != npc.perfusaoExtremidades) return false;
        if (medicalDataUI.pulsosDropdown.options[medicalDataUI.pulsosDropdown.value].text != npc.pulsos) return false;
        if (medicalDataUI.ritmoCardiacoDropdown.options[medicalDataUI.ritmoCardiacoDropdown.value].text != npc.ritmoCardiaco) return false;
        if (medicalDataUI.edemaDropdown.options[medicalDataUI.edemaDropdown.value].text != npc.edema) return false;
        if (medicalDataUI.temperaturaInput.text != npc.temperatura) return false;

        // Avaliação Neurológica
        if (medicalDataUI.nivelConscienciaDropdown.options[medicalDataUI.nivelConscienciaDropdown.value].text != npc.nivelConsciencia) return false;

        // Exposição
        if (medicalDataUI.avalicaoPeleDropdown.options[medicalDataUI.avalicaoPeleDropdown.value].text != npc.avalicaoPele) return false;
        if (medicalDataUI.presencaPeleDropdown.options[medicalDataUI.presencaPeleDropdown.value].text != npc.presencaPele) return false;
        if (medicalDataUI.dorEscalaInput.text != npc.dorEscala) return false;

        return true; // Todos os campos bateram com o ScriptableObject!
    }
}