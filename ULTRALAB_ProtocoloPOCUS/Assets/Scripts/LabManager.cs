using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LabManager : MonoBehaviour
{
    [Header("Música ambiente FMOD")]
    public EventReference ambientMusicEvent;

    private EventInstance ambientMusicInstance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ambientMusicEvent.IsNull)
            return;

        ambientMusicInstance = RuntimeManager.CreateInstance(ambientMusicEvent);
        ambientMusicInstance.start();
    }

    void OnDestroy()
    {
        if (ambientMusicInstance.isValid())
        {
            ambientMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientMusicInstance.release();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
