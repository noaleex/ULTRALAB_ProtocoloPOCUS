using UnityEngine;

public class SkipDay : MonoBehaviour, IInteractable
{
    [SerializeField] private Timer timer;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (timer != null)
        {
            timer.SkipToNextDay();
        }
    }
}