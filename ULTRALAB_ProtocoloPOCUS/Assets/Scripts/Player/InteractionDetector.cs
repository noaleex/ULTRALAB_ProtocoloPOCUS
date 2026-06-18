using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null;
    private AndroidControl android;

    [SerializeField] private GameObject interactionIcon;


    private void Awake()
    {
        android = AndroidControl.Instance;
    }


    private void Start()
    {
        interactionIcon.SetActive(false);
    }


    private void OnEnable()
    {
        android = AndroidControl.Instance;
    }


    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            interactableInRange?.Interact();
        }
    }

    public void MobileInteract()
    {
        interactableInRange?.Interact();
    }

    public void ForceInteractable(IInteractable interactable)
    {
        interactableInRange = interactable;
    }

    public void ClearForcedInteractable(IInteractable interactable)
    {
        if (interactableInRange == interactable)
        {
            interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) &&
            interactable.CanInteract())
        {
            interactableInRange = interactable;

            interactionIcon.SetActive(true);


            if(android == null)
                android = AndroidControl.Instance;


            android?.SetInteract(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) &&
            interactable == interactableInRange)
        {
            interactableInRange = null;
        }


        if(android == null)
            android = AndroidControl.Instance;


        android?.SetInteract(false);

        interactionIcon.SetActive(false);
    }
}