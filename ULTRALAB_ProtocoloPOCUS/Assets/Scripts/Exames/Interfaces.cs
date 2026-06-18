using UnityEngine;
using UnityEngine.SceneManagement;


public class Interfaces : MonoBehaviour
{
    [SerializeField] private GameObject pocus;
    [SerializeField] private GameObject leito;
    [SerializeField] private GameObject tool;

    [SerializeField] private string lab;


    public void PocusClick()
    {
        pocus.SetActive(true);
        AudioManager.Instance?.PlayInteraction();
    }


    public void LeitoClick()
    {
        leito.SetActive(true);
        AudioManager.Instance?.PlayInteraction();
    }


    public void ToolClick()
    {
        tool.SetActive(true);
        AudioManager.Instance?.PlayInteraction();
    }


    public void BackInterface()
    {
        pocus.SetActive(false);
        leito.SetActive(false);
        tool.SetActive(false);

        AudioManager.Instance?.PlayBack();
    }


    public void BackExam()
    {
        PauseController.SetPause(false);

        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(lab);
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;


        if(PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.RefreshReferences();

            PlayerReferences.Instance.EnablePlayer();


            if(PlayerReferences.Instance.InteractIcon != null)
            {
                PlayerReferences.Instance.InteractIcon.SetActive(true);
            }
        }
    }
}