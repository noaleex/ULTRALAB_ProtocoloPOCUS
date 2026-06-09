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

    private GameObject interactIcon;
    private EventInstance dialogueVoiceInstance;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
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
        }

        if (PlayerReferences.Instance.playerMovement != null)
        {
            PlayerReferences.Instance.playerMovement.enabled = false;
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
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        StopVoice();

        isTyping = false;

        if (
            dialogueData.autoProgressLines.Length > dialogueIndex &&
            dialogueData.autoProgressLines[dialogueIndex]
        )
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    void StartVoice()
    {
        if (dialogueData.voiceEvent.IsNull)
            return;

        dialogueVoiceInstance = RuntimeManager.CreateInstance(dialogueData.voiceEvent);
        dialogueVoiceInstance.start();
    }

    void StopVoice()
    {
        if (dialogueVoiceInstance.isValid())
        {
            dialogueVoiceInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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

        if (PlayerReferences.Instance != null)
        {
            PlayerReferences.Instance.InteractIcon.SetActive(true);
        }

        if (PlayerReferences.Instance.playerMovement != null)
        {
            PlayerReferences.Instance.playerMovement.enabled = true;
        }
    }
}
