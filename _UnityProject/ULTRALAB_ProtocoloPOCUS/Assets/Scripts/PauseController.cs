using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public static bool IsGamePaused { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public static void SetPause(bool pause)
    {
        IsGamePaused = pause;

        Time.timeScale = pause ? 0f : 1f;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.started)
            return;


        if (IsGamePaused)
            ResumeGame();

        else
            PauseGame();
    }

    private void PauseGame()
    {
        SetPause(true);

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    private void ResumeGame()
    {
        SetPause(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void OnMenu()
    {
        if (IsGamePaused)
            ResumeGame();

        else
            PauseGame();
    }
}