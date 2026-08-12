using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FMODUnity;
using UnityEngine.EventSystems;

public class MedicalData : MonoBehaviour
{
    [Header("Referência de Dados")]
    public PatientData patientData;

    [Header("Via Aérea")]
    public TMP_Dropdown permeabilidadeDropdown;
    public TMP_Dropdown presencaDropdown;
    public TMP_Dropdown intervencaoDropdown;

    [Header("Respiração")]
    public TMP_InputField frequenciaInput;
    public TMP_InputField saturationInput;
    public TMP_Dropdown acessoriaDropdown;
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

    [Header("FMOD - Sons")]
    [SerializeField] private EventReference somSelecao;
    [SerializeField] private EventReference somDigitacao;

    private string ultimoValorDor = "";

    private void Start()
    {
        if (CurrentPatient.Data != null)
        {
            patientData = CurrentPatient.Data;
        }

    
        SetupNumericInput(frequenciaInput);
        SetupNumericInput(saturationInput);
        SetupNumericInput(oxigenoterapiaFluxoInput);
        SetupNumericInput(frequenciaCardiacaInput);
        SetupNumericInput(pressaoArterialInput);
        SetupNumericInput(pressaoArterial2Input);
        SetupNumericInput(perfusaoTempoInput);
        SetupNumericInput2(temperaturaInput);
        SetupNumericInput2(dorEscalaInput);

        
        AdicionarSomDropdowns();

        // Som ao digitar na escala de dor (1-10)
        dorEscalaInput.onValueChanged.AddListener(_ => TocarSomDigitacao());
    }

    private void AdicionarSomDropdowns()
    {
        permeabilidadeDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        presencaDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        intervencaoDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        acessoriaDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        padraoRespiratorioDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        ascultaDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        expansibilidadeDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        oxigenoterapiaTipoDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        perfusaoExtremidadesDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        pulsosDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        ritmoCardiacoDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        edemaDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        nivelConscienciaDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        avalicaoPeleDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());
        presencaPeleDropdown.onValueChanged.AddListener(_ => TocarSomSelecao());

       
        AdicionarEventTriggerDropdown(permeabilidadeDropdown);
        AdicionarEventTriggerDropdown(presencaDropdown);
        AdicionarEventTriggerDropdown(intervencaoDropdown);
        AdicionarEventTriggerDropdown(acessoriaDropdown);
        AdicionarEventTriggerDropdown(padraoRespiratorioDropdown);
        AdicionarEventTriggerDropdown(ascultaDropdown);
        AdicionarEventTriggerDropdown(expansibilidadeDropdown);
        AdicionarEventTriggerDropdown(oxigenoterapiaTipoDropdown);
        AdicionarEventTriggerDropdown(perfusaoExtremidadesDropdown);
        AdicionarEventTriggerDropdown(pulsosDropdown);
        AdicionarEventTriggerDropdown(ritmoCardiacoDropdown);
        AdicionarEventTriggerDropdown(edemaDropdown);
        AdicionarEventTriggerDropdown(nivelConscienciaDropdown);
        AdicionarEventTriggerDropdown(avalicaoPeleDropdown);
        AdicionarEventTriggerDropdown(presencaPeleDropdown);
    }

    private void AdicionarEventTriggerDropdown(TMP_Dropdown dropdown)
    {
        EventTrigger trigger = dropdown.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = dropdown.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => TocarSomSelecao());
        trigger.triggers.Add(entry);
    }

    private void TocarSomSelecao()
    {
        if (!somSelecao.IsNull)
        {
            RuntimeManager.PlayOneShot(somSelecao);
        }
    }

    private void TocarSomDigitacao()
    {
        string valorAtual = dorEscalaInput.text;

        // Só toca som se o jogador digitou (adicionou caracteres), não se apagou
        if (valorAtual.Length > ultimoValorDor.Length)
        {
            if (!somDigitacao.IsNull)
            {
                RuntimeManager.PlayOneShot(somDigitacao);
            }
        }

        ultimoValorDor = valorAtual;
    }

    /// <summary>
    /// Configura o InputField para aceitar apenas inteiros de 0 a 999
    /// </summary>
    private void SetupNumericInput(TMP_InputField inputField)
    {
        if (inputField == null) return;

        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.characterLimit = 3; // Impede digitar mais de 3 dígitos

        inputField.onEndEdit.AddListener((string value) =>
        {
            if (int.TryParse(value, out int numericValue))
            {
                int clampedValue = Mathf.Clamp(numericValue, 0, 999);
                inputField.text = clampedValue.ToString();
            }
            else
            {
                inputField.text = "0";
            }
        });
    }

    private void SetupNumericInput2(TMP_InputField inputField)
    {
        if (inputField == null) return;

        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.characterLimit = 2; // Impede digitar mais de 2 dígitos

        inputField.onEndEdit.AddListener((string value) =>
        {
            if (int.TryParse(value, out int numericValue))
            {
                int clampedValue = Mathf.Clamp(numericValue, 0, 99);
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
        patientData.intervencao = intervencaoDropdown.options[intervencaoDropdown.value].text;

        // Respiração
        patientData.frequencia = frequenciaInput.text;
        patientData.saturation = saturationInput.text;
        patientData.acessoria = acessoriaDropdown.options[acessoriaDropdown.value].text;
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