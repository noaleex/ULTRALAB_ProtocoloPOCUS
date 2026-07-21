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
    public string caso;

    [TextArea]
    public string physicalExam;
}

public static class CurrentPatient
{
    public static PatientData Data;
}