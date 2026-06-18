using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using FMODUnity;
using FMOD.Studio;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCdialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;
    public EventReference interactSoundEvent;

    private EventInstance dialogueVoiceInstance;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool IsDialogueActive => isDialogueActive;
    public System.Action OnDialogueEnded;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        PlayInteractSound();

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void PlayInteractSound()
    {
        if (!interactSoundEvent.IsNull)
        {
            RuntimeManager.PlayOneShot(interactSoundEvent);
        }
    }

    public void StartDialogueExternally()
    {
        if (dialogueData == null || isDialogueActive)
            return;


        if (!gameObject.activeInHierarchy)
        {
            gameObject.SetActive(true);
        }


        if (PlayerReferences.Instance?.InteractionDetector != null)
        {
            PlayerReferences.Instance.InteractionDetector.ForceInteractable(this);
        }


        PlayInteractSound();

        StartDialogue();
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);

        PauseController.SetPause(true);

        StartCoroutine(TypeLine());

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.InteractIcon.SetActive(false);

            if (PlayerReferences.Instance.playerMovement != null)
            {
                PlayerReferences.Instance.DisablePlayer();
            }
        }
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            StopVoice();

            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);

            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        StartVoice();

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(dialogueData.typingSpeed);
        }

        StopVoice();

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex &&
            dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSecondsRealtime(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void StartVoice()
    {
        if (dialogueData.voiceEvent.IsNull)
            return;

        StopVoice();

        dialogueVoiceInstance = RuntimeManager.CreateInstance(dialogueData.voiceEvent);
        dialogueVoiceInstance.start();
    }

    void StopVoice()
    {
        if (dialogueVoiceInstance.isValid())
        {
            dialogueVoiceInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            dialogueVoiceInstance.release();
            dialogueVoiceInstance = default;
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        StopVoice();

        isDialogueActive = false;

        dialogueText.SetText("");
        dialoguePanel.SetActive(false);

        PauseController.SetPause(false);


        if (PlayerReferences.Instance?.InteractionDetector != null)
        {
            PlayerReferences.Instance.InteractionDetector.ClearForcedInteractable(this);
        }


        OnDialogueEnded?.Invoke();
        OnDialogueEnded = null;


        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.InteractIcon.SetActive(true);

            if (PlayerReferences.Instance.playerMovement != null)
            {
                PlayerReferences.Instance.EnablePlayer();
            }
        }
    }
}