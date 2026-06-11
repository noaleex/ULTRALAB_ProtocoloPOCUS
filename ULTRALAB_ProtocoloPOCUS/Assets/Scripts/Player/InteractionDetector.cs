using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;

    [SerializeField] private GameObject interactionIcon;

    private void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            interactableInRange?.Interact();
        }
    }

    public void ForceInteractable(IInteractable interactable)
    {
        interactableInRange = interactable;
    }

    public void ClearForcedInteractable(IInteractable interactable)
    {
        if (interactableInRange == interactable)
        {
            interactableInRange = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) &&
            interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) &&
            interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}