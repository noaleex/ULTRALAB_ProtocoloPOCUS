using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MedicalData : MonoBehaviour
{
    [Header("Referência de Dados")]
    public PatientData patientData; // Ou use CurrentPatient.Data

    [Header("Via Aérea")]
    public TMP_Dropdown permeabilidadeDropdown;
    public TMP_Dropdown presencaDropdown;
    public Toggle intervencaoToggle;

    [Header("Respiração")]
    public TMP_InputField frequenciaInput;
    public TMP_InputField saturationInput;
    public Toggle acessoriaToggle;
    public TMP_Dropdown padraoRespiratorioDropdown;
    public TMP_Dropdown ascultaDropdown;
    public TMP_Dropdown expansibilidadeDropdown;
    public TMP_Dropdown oxigenoterapiaTipoDropdown;
    public TMP_InputField oxigenoterapiaFluxoInput;

    [Header("Circulação")]
    public TMP_InputField frequenciaCardiacaInput;
    public TMP_InputField pressaoArterialInput;
    public TMP_InputField pressaoArterial2Input;
    public TMP_InputField perfusaoTempoInput;
    public TMP_Dropdown perfusaoExtremidadesDropdown;
    public TMP_Dropdown pulsosDropdown;
    public TMP_Dropdown ritmoCardiacoDropdown;
    public TMP_Dropdown edemaDropdown;
    public TMP_InputField temperaturaInput;

    [Header("Avaliação Neurológica")]
    public TMP_Dropdown nivelConscienciaDropdown;

    [Header("Exposição")]
    public TMP_Dropdown avalicaoPeleDropdown;
    public TMP_Dropdown presencaPeleDropdown;
    public TMP_InputField dorEscalaInput;

    private void Start()
    {
        // Se estiver usando o paciente global estático:
        if (CurrentPatient.Data != null)
        {
            patientData = CurrentPatient.Data;
        }

        // Configura a restrição numérica de 0 a 999 em todos os InputFields
        SetupNumericInput(frequenciaInput);
        SetupNumericInput(saturationInput);
        SetupNumericInput(oxigenoterapiaFluxoInput);
        SetupNumericInput(frequenciaCardiacaInput);
        SetupNumericInput(pressaoArterialInput);
        SetupNumericInput(pressaoArterial2Input);
        SetupNumericInput(perfusaoTempoInput);
        SetupNumericInput(temperaturaInput);
        SetupNumericInput(dorEscalaInput);
    }

    /// <summary>
    /// Configura o InputField para aceitar apenas inteiros de 0 a 999.
    /// </summary>
    private void SetupNumericInput(TMP_InputField inputField)
    {
        if (inputField == null) return;

        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.characterLimit = 3; // Impede digitar mais de 3 dígitos

        // Adiciona validação ao finalizar a edição
        inputField.onEndEdit.AddListener((string value) =>
        {
            if (int.TryParse(value, out int numericValue))
            {
                // Garante que fique entre 0 e 999
                int clampedValue = Mathf.Clamp(numericValue, 0, 999);
                inputField.text = clampedValue.ToString();
            }
            else
            {
                inputField.text = "0";
            }
        });
    }

    /// <summary>
    /// Verifica se todos os campos estão devidamente preenchidos.
    /// </summary>
    public bool ValidateAllFields()
    {
        // Exemplo de verificação de campos de texto vazios
        if (string.IsNullOrEmpty(frequenciaInput.text) ||
            string.IsNullOrEmpty(saturationInput.text) ||
            string.IsNullOrEmpty(frequenciaCardiacaInput.text) ||
            string.IsNullOrEmpty(pressaoArterialInput.text) ||
            string.IsNullOrEmpty(temperaturaInput.text) ||
            string.IsNullOrEmpty(dorEscalaInput.text))
        {
            Debug.LogWarning("Existem campos numéricos obrigatórios não preenchidos!");
            return false;
        }

        Debug.Log("Todas as informações foram validadas com sucesso!");
        return true;
    }

    /// <summary>
    /// Salva as informações da UI para o ScriptableObject.
    /// </summary>
    public void SaveDataToPatient()
    {
        if (!ValidateAllFields()) return;

        if (patientData == null)
        {
            Debug.LogError("Nenhum PatientData atribuído!");
            return;
        }

        // Via Aérea
        patientData.permeabilidade = permeabilidadeDropdown.options[permeabilidadeDropdown.value].text;
        patientData.presenca = presencaDropdown.options[presencaDropdown.value].text;
        patientData.intervenção = intervencaoToggle.isOn;

        // Respiração
        patientData.frequencia = frequenciaInput.text;
        patientData.saturation = saturationInput.text;
        patientData.acessoria = acessoriaToggle.isOn;
        patientData.padraoRespiratorio = padraoRespiratorioDropdown.options[padraoRespiratorioDropdown.value].text;
        patientData.asculta = ascultaDropdown.options[ascultaDropdown.value].text;
        patientData.expansibilidade = expansibilidadeDropdown.options[expansibilidadeDropdown.value].text;
        patientData.oxigenoterapiaTipo = oxigenoterapiaTipoDropdown.options[oxigenoterapiaTipoDropdown.value].text;
        patientData.oxigenoterapiaFluxo = oxigenoterapiaFluxoInput.text;

        // Circulação
        patientData.frequenciaCardiaca = frequenciaCardiacaInput.text;
        patientData.pressaoArterial = pressaoArterialInput.text;
        patientData.pressaoArterial2 = pressaoArterial2Input.text;
        patientData.perfusaoTempo = perfusaoTempoInput.text;
        patientData.perfusaoExtremidades = perfusaoExtremidadesDropdown.options[perfusaoExtremidadesDropdown.value].text;
        patientData.pulsos = pulsosDropdown.options[pulsosDropdown.value].text;
        patientData.ritmoCardiaco = ritmoCardiacoDropdown.options[ritmoCardiacoDropdown.value].text;
        patientData.edema = edemaDropdown.options[edemaDropdown.value].text;
        patientData.temperatura = temperaturaInput.text;

        // Avaliação Neurológica
        patientData.nivelConsciencia = nivelConscienciaDropdown.options[nivelConscienciaDropdown.value].text;

        // Exposição
        patientData.avalicaoPele = avalicaoPeleDropdown.options[avalicaoPeleDropdown.value].text;
        patientData.presencaPele = presencaPeleDropdown.options[presencaPeleDropdown.value].text;
        patientData.dorEscala = dorEscalaInput.text;

        Debug.Log("Dados do paciente salvos com sucesso!");
    }
}