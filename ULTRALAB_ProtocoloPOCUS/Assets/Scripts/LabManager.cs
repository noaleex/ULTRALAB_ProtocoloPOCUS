using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LabManager : MonoBehaviour
{
    [Header("Música ambiente FMOD")]
    public EventReference ambientMusicEvent;
    public EventReference ambientMusicEvent2;

    private EventInstance ambientMusicInstance;
    private EventInstance ambientMusicInstance2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!ambientMusicEvent.IsNull)
        {
            ambientMusicInstance = RuntimeManager.CreateInstance(ambientMusicEvent);
            ambientMusicInstance.start();
        }

        if (!ambientMusicEvent2.IsNull)
        {
            ambientMusicInstance2 = RuntimeManager.CreateInstance(ambientMusicEvent2);
            ambientMusicInstance2.start();
        }
    }

    void OnDestroy()
    {
        if (ambientMusicInstance.isValid())
        {
            ambientMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientMusicInstance.release();
        }

        if (ambientMusicInstance2.isValid())
        {
            ambientMusicInstance2.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            ambientMusicInstance2.release();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
