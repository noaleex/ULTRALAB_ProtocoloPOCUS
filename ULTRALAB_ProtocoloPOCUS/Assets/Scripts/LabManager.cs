using UnityEngine;

public class LabManager : MonoBehaviour
{
    [SerializeField] private string uti;

    public void BackLAB()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(uti);
    }
}
