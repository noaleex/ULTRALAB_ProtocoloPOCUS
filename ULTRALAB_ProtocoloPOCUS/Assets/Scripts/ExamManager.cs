using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [SerializeField] private string lab;
    [SerializeField] private string uti;
    
    public void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(lab);
    }
}
