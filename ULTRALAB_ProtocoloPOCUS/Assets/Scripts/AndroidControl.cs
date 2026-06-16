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
        Instance = this;
    }


    private void Start()
    {
        interactionDetector = PlayerReferences.Instance.InteractionDetector;

        SetInteract(false);

        /*if (Application.platform == RuntimePlatform.Android) 
        { 
            painelAndroid.SetActive(true); 
        } 
        else 
        { 
            painelAndroid.SetActive(false); 
        }*/
    }


    public void ClickInteract()
    {
        interactionDetector?.MobileInteract();
    }


    public void SetInteract(bool value)
    {
        if(buttonInteract == null)
            return;

        if(value)
        {
            buttonInteract.color = new Color(1f,1f,1f,1f);
        }
        else
        {
            buttonInteract.color = new Color(1f,1f,1f,0.5f);
        }
    }
}