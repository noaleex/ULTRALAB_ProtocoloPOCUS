using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences Instance;
    [SerializeField] private GameObject interactIcon;

    public GameObject InteractIcon => interactIcon;

    private void Awake()
    {
        Instance = this;
    }
}