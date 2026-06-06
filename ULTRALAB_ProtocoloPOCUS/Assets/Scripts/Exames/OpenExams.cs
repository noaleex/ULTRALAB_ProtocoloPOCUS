using UnityEngine;

public class OpenExams : MonoBehaviour,IInteractable
{

    [SerializeField] private string exams;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(exams);
    }
}
