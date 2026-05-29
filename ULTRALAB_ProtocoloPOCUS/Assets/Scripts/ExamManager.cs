using UnityEngine;

public class ExamManager : MonoBehaviour
{
    [SerializeField] private string lab;
    [SerializeField] private string uti;
    
    public void BackLAB()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(lab);
    }

    public void BackUTI()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(uti);
    }
}
