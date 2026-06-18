using UnityEngine;
using UnityEditor; 
using FMODUnity;
using FMOD.Studio;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private string tutorial;
    
    [SerializeField] private GameObject menuInicial;
    [SerializeField] private GameObject SelectionCharacter;
    [SerializeField] private GameObject menuOptionsPC;
    [SerializeField] private GameObject menuOptionsAndroid;
    [SerializeField] private GameObject menuSairConfimacao;

    public EventReference MusicaMenu;    
    public EventReference ClickMenu;
    public EventReference SelecionarPersonagemClick;
    private EventInstance menuMusicInstance;

    private string character;

    private void Start()
    {
        if (!MusicaMenu.IsNull)
        {
            menuMusicInstance = RuntimeManager.CreateInstance(MusicaMenu);
            menuMusicInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            menuMusicInstance.start();
        }
    }

    private void OnDestroy()
    {
        StopMenuMusic();
        if (menuMusicInstance.isValid())
        {
            menuMusicInstance.release();
        }
    }

    private void StopMenuMusic()
    {
        if (menuMusicInstance.isValid())
        {
            menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    private void PlayClickSound()
    {
        if (!ClickMenu.IsNull)
        {
            RuntimeManager.PlayOneShot(ClickMenu, transform.position);
        }
    }

    public void Play()
    {
        PlayClickSound();
        menuInicial.SetActive(false);
        SelectionCharacter.SetActive(true);
    }

    public void Back()
    {
        PlayClickSound();
        menuInicial.SetActive(true);
        SelectionCharacter.SetActive(false);
        character = null;

        print("Personagem Limpo");
        PlayerPrefs.DeleteKey("Character");
        PlayerPrefs.Save();
    }

    public void ButtonMale()
    {
        if (!SelecionarPersonagemClick.IsNull)
        {
            RuntimeManager.PlayOneShot(SelecionarPersonagemClick, transform.position);
        }
        character = "Masculino";
        print("Personagem Masculino selecionado");
    }

    public void ButtonFemale()
    {
        if (!SelecionarPersonagemClick.IsNull)
        {
            RuntimeManager.PlayOneShot(SelecionarPersonagemClick, transform.position);
        }
        character = "Feminino";
        print("Personagem Feminino selecionado");
    }

    public void StartGame()
    {
        PlayClickSound();
        if (string.IsNullOrEmpty(character))
        {
            print("Selecione um personagem antes de iniciar!");
            return;
        }

        PlayerPrefs.SetString("Character", character);
        PlayerPrefs.Save();
        StopMenuMusic();
        UnityEngine.SceneManagement.SceneManager.LoadScene(tutorial);
    }

    public void Options()
    {
        menuInicial.SetActive(false);

        if (Application.platform == RuntimePlatform.Android)
        {
            menuOptionsAndroid.SetActive(true);
        }
        else
        {
            menuOptionsPC.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        menuOptionsPC.SetActive(false);
        menuOptionsAndroid.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void QuitConfirm()
    {
        menuInicial.SetActive(false);
        menuSairConfimacao.SetActive(true);
    }

    public void QuitCancel()
    {
        menuSairConfimacao.SetActive(false);
        menuInicial.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void AndroidConfim()
    { 
            menuSairConfimacao.SetActive(true);
            menuInicial.SetActive(false);
    }

}
