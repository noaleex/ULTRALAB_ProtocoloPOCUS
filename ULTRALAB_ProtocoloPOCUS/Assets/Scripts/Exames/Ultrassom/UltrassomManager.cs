using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class UltrasoundManager : MonoBehaviour
{
    public static UltrasoundManager Instance;

    [Header("Imagem da Sonda")]

    public Image probeImage;

    [Header("Tela do Ultrassom")]
    public Image resultImage;

    [Header("FMOD")]
    public EventReference gelSound;

    private EventInstance gelInstance;

    [Header("Imagem Padrão")]
    public Sprite defaultImage;

    [Header("Sprites dos Transdutores")]
    public Sprite setorialProbeSprite;
    public Sprite linearProbeSprite;
    public Sprite convexProbeSprite;

    private RectTransform probeRect;

    [Header("Áreas")]
    public BodyArea heart;
    public BodyArea lung1;
    public BodyArea lung2;
    public BodyArea bladder;

    [HideInInspector]
    public TransdutorSelected.ProbeType currentProbe =
        TransdutorSelected.ProbeType.None;

    private void Awake()


    {
        Instance = this;

        probeRect = probeImage.GetComponent<RectTransform>();
    }

    private void Start()
    {
        resultImage.sprite = defaultImage;
    }



    public void SelectProbe(TransdutorSelected.ProbeType probe)
{
    currentProbe = probe;

    switch (probe)
    {
        case TransdutorSelected.ProbeType.Setorial:
            probeImage.sprite = setorialProbeSprite;
            break;

        case TransdutorSelected.ProbeType.Linear:
            probeImage.sprite = linearProbeSprite;
            break;

        case TransdutorSelected.ProbeType.Convex:
            probeImage.sprite = convexProbeSprite;
            break;

             HandleGelSound(true);
    }

    CheckProbePosition(probeRect.position);
}

   private void HandleGelSound(bool probeOverBody)
{
    if (gelSound.IsNull || currentProbe == TransdutorSelected.ProbeType.None)
    {
        StopAndReleaseGel();
        return;
    }

    if (probeOverBody)
    {
        if (!gelInstance.isValid())
        {
            gelInstance = RuntimeManager.CreateInstance(gelSound);
            gelInstance.start();
        }
    }
    else
    {
        StopAndReleaseGel();
    }
}

    private void StopAndReleaseGel()
{
    if (gelInstance.isValid())
    {
        gelInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        gelInstance.release();
    }

    gelInstance = default;
}

    public void CheckProbePosition(Vector2 probePosition)
    {
        // CORAÇÃO -> LINEAR
        if (heart.ContainsPoint(probePosition))
        {
            if (currentProbe == TransdutorSelected.ProbeType.Linear)
                resultImage.sprite = heart.correctImage;
            else
                resultImage.sprite = defaultImage;

            return;
        }

        // PULMÃO -> SETORIAL
        if (lung1.ContainsPoint(probePosition) ||
            lung2.ContainsPoint(probePosition))
        {
            if (currentProbe == TransdutorSelected.ProbeType.Setorial)
                resultImage.sprite = lung1.correctImage;
            else
                resultImage.sprite = defaultImage;

            return;
        }

        // BEXIGA -> CONVEXO
        if (bladder.ContainsPoint(probePosition))
        {
            if (currentProbe == TransdutorSelected.ProbeType.Convex)
                resultImage.sprite = bladder.correctImage;
            else
                resultImage.sprite = defaultImage;

            return;
        }

        resultImage.sprite = defaultImage;
    }
}