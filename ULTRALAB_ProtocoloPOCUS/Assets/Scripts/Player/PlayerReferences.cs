using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences Instance;

    [SerializeField] private GameObject interactIcon;
    [SerializeField] private InteractionDetector interactionDetector;

    public CharacterController2D playerMovement;

    public GameObject InteractIcon => interactIcon;
    public InteractionDetector InteractionDetector => interactionDetector;

    private void Awake()
    {
        Instance = this;
    }
}