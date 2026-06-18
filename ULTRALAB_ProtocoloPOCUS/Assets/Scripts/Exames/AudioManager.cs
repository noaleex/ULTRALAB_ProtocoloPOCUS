using UnityEngine;
using FMODUnity;
using FMOD.Studio;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

     [SerializeField] private EventReference backgroundMusic;
    [SerializeField] private EventReference interactionSound;
    [SerializeField] private EventReference backButtonSound;

    private EventInstance bgInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (!backgroundMusic.IsNull)
        {
            bgInstance = RuntimeManager.CreateInstance(backgroundMusic);
            bgInstance.start();
        }
    }

    public void PlayInteraction()
    {
        if (!interactionSound.IsNull)
            RuntimeManager.PlayOneShot(interactionSound);
    }

    public void PlayBack()
    {
        if (!backButtonSound.IsNull)
            RuntimeManager.PlayOneShot(backButtonSound);
    }

    public void PlayOneShot(EventReference eventRef)
{
    if (eventRef.IsNull) return;

    RuntimeManager.PlayOneShot(eventRef);
}

    public void StopBackground()
    {
        if (bgInstance.isValid())
        {
            bgInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bgInstance.release();
        }
    }

    void OnDestroy()
    {
        StopBackground();
    }
}
