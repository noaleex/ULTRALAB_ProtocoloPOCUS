using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(2, 5)]
    public string sentence;
}

[System.Serializable]
public class CutsceneSequence
{
    public string cutsceneID;
    public DialogueLine[] lines;
}

public class TextCutsceneManager : MonoBehaviour
{
    public static TextCutsceneManager Instance {get; private set;}

    [Header("Configurações de UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Danco de Diálogos")]
    [SerializeField] private CutsceneSequence[] allCutscenes;

    private CutsceneSequence currentSequence;
    private int currentLineIndex = 0;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (dialoguePanel != null) dialoguePanel.SetActive(false);
    }

    public void StartCutsceneDialogue (string id)
    {
        currentSequence = System.Array.Find(allCutscenes, c => c.cutsceneID == id);

        if (currentSequence == null)
        {
            Debug.LogWarning($"[TextCutsceneManager] Cutscene com ID '{id}' não encontrada!");
            return;
        }

        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        Debug.Log($"[TextCutsceneManager] Carregou os diálogos da cutscene: {id}");
    }

    public void PlayNextLine ()
    {
        if (currentSequence == null) return;

        if (currentLineIndex < currentSequence.lines.Length)
        {
            DialogueLine nextLine = currentSequence.lines[currentLineIndex];

            if (nameText != null) 
            {
                nameText.text = nextLine.characterName;
                nameText.gameObject.SetActive(!string.IsNullOrEmpty(nextLine.characterName));
            }

            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            
            typingCoroutine = StartCoroutine(TypewriterEffect(nextLine.sentence));
            currentLineIndex++;
        } else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        dialoguePanel.SetActive(false);
        currentSequence = null;
    }

    private IEnumerator TypewriterEffect(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
