using UnityEngine;
using UnityEngine.UI;

public class Interfaces : MonoBehaviour
{
    [SerializeField] private GameObject pocus;
    [SerializeField] private GameObject leito;
    [SerializeField] private GameObject tool;
    [SerializeField] private string lab;

    public void PocusClick()
    {
        pocus.SetActive(true);
    }

    public void LeitoClick()
    {
        leito.SetActive(true);
    }

    public void ToolClick()
    {
        tool.SetActive(true);
    }

    public void BackInterface()
    {
        pocus.SetActive(false);
        leito.SetActive(false);
        tool.SetActive(false);
    }

    public void BackExam()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(lab);
        PlayerReferences.Instance.playerMovement.enabled = true;
        PauseController.SetPause(false);
    }
}