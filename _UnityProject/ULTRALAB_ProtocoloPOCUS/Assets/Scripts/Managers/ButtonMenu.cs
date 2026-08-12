using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMenu : MonoBehaviour
{
    [SerializeField] private PauseController pauseController;
    [SerializeField] private EventReference clickSound;

    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("Button não encontrado no ButtonMenu", this);
            return;
        }

        if (pauseController != null)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OnButtonClicked);
        }
        else
        {
            Debug.LogError("PauseController não encontrado no ButtonMenu", this);
        }
    }

    private void OnButtonClicked()
    {
        PlayClickSound();
        pauseController.OnMenu();
    }

    private void PlayClickSound()
    {
        if (clickSound.IsNull)
            return;

        RuntimeManager.PlayOneShot(clickSound, transform.position);
    }
}