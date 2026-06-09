using UnityEngine;

public class ProbeButton : MonoBehaviour
{
    public TransdutorSelected.ProbeType probeType;

    public void SelectProbe()
    {
        UltrasoundManager.Instance.SelectProbe(probeType);
    }
}