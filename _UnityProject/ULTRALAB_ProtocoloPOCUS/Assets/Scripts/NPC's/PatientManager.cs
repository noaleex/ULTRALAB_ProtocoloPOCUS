using System.Collections.Generic;
using UnityEngine;

public class PatientManager : MonoBehaviour
{
    public static PatientManager Instance;

    private readonly List<OpenExams> patients = new List<OpenExams>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterPatient(OpenExams patient)
    {
        if (patient == null)
            return;

        if (!patients.Contains(patient))
        {
            patients.Add(patient);
        }
    }

    public void UnregisterPatient(OpenExams patient)
    {
        if (patient == null)
            return;

        patients.Remove(patient);
    }

    public IReadOnlyList<OpenExams> GetPatients()
    {
        return patients;
    }

    /// <summary>
    /// Verifica se todos os pacientes tiveram sua conduta realizada.
    /// </summary>
    public bool AllPatientsCompletedConduct()
    {
        foreach (OpenExams patient in patients)
        {
            if (patient == null)
                continue;

            if (!patient.ConductCompleted)
            {
                return false;
            }
        }

        return true;
    }
}