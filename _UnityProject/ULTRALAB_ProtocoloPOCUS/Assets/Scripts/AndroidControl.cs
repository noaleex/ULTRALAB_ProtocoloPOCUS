using UnityEngine;
using UnityEngine.UI;

public class AndroidControl : MonoBehaviour
{
    public static AndroidControl Instance;

    [SerializeField] private GameObject painelAndroid;
    [SerializeField] private Image buttonInteract;

    private InteractionDetector interactionDetector;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        SetInteract(false);

        if (painelAndroid != null)
        {
            painelAndroid.SetActive(Application.platform == RuntimePlatform.Android);
        }

        FindPlayerReferences();
    }

    private void FindPlayerReferences()
    {
        if (PlayerReferences.Instance != null)
        {
            interactionDetector = PlayerReferences.Instance.InteractionDetector;
        }
    }

    public void SetPlayerInteraction(InteractionDetector detector)
    {
        interactionDetector = detector;
    }

    public void ClickInteract()
    {
        if (interactionDetector != null)
        {
            interactionDetector.MobileInteract();
        }
    }

    public void SetInteract(bool value)
    {
        if (buttonInteract == null)
            return;


        if (value)
        {
            buttonInteract.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            buttonInteract.color = new Color(1f, 1f, 1f, 0.5f);
        }
    }
}