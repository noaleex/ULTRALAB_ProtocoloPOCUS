using UnityEngine;
using FMODUnity;

public class ProbeButton : MonoBehaviour
{
    public TransdutorSelected.ProbeType probeType;

    [Header("FMOD")]
    public EventReference clickSound;

    public void SelectProbe()
    {
       
        if (!clickSound.IsNull)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayOneShot(clickSound);
            }
            else
            {
                RuntimeManager.PlayOneShot(clickSound);
            }
        }

        UltrasoundManager.Instance.SelectProbe(probeType);
    }
}