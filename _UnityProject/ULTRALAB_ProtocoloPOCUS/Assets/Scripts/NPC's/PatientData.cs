using UnityEngine;

[CreateAssetMenu(fileName = "Patient", menuName = "Patients/Patient Data")]
public class PatientData : ScriptableObject
{
    [Header("Geral")]
    public string patientName;
    public int age;
    public string sex;
    public bool tutorial;

    [Header("Visual")]
    public Sprite characterSprite;
    public Sprite backgroundSprite;

    [Header("Caso")]
    [TextArea]
    public string resumeCaso;

    [TextArea]
    public string physicalExam;

    [Header("Via Aérea")]
    public string permeabilidade;
    public string presenca;
    public bool intervenção;

    [Header("Respiração")]
    public string frequencia;
    public string saturation;
    public bool acessoria;
    public string padraoRespiratorio;
    public string asculta;
    public string expansibilidade;
    public string oxigenoterapiaTipo;
    public string oxigenoterapiaFluxo;

    [Header("Circulação")]
    public string frequenciaCardiaca;
    public string pressaoArterial;
    public string perfusaoTempo;
    public string perfusaoExtremidades;
    public string pulsos;
    public string ritmoCardiaco;
    public string edema;
    public string temperatura;

    [Header("Avaliação neurológica")]
    public string nivelConsciencia;

    [Header("Exposição")]
    public string avalicaoPele;
    public string presencaPele;
    public string dorEscala;
}

public static class CurrentPatient
{
    public static PatientData Data;
}