using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private PauseController pauseController;


    private void Start()
    {
        Button button = GetComponent<Button>();

        if (pauseController != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(pauseController.OnMenu);
        }
        else
        {
            Debug.LogError("PauseController não encontrado no ButtonMenu");
        }
    }
}