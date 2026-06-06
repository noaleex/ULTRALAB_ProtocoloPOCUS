using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class LabManager : MonoBehaviour
{
    [SerializeField] private string uti;
    public EventReference GameMusic;
    private EventInstance musicInstance;

    void Start()
    {
        if (!GameMusic.IsNull)
        {
            musicInstance = RuntimeManager.CreateInstance(GameMusic);
            musicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            musicInstance.start();
        }
    }

    public void BackLAB()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(uti);
    }

    private void OnDestroy()
    {
        if (musicInstance.isValid())
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            musicInstance.release();
        }
    }
}
