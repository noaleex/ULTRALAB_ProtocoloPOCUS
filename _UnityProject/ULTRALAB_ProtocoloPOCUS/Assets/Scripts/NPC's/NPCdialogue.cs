using UnityEngine;
using FMODUnity;

[CreateAssetMenu(fileName = "NPCdialogue", menuName = "NPC Dialogue")]
public class NPCdialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;

    [Header("FMOD")]
    public EventReference voiceEvent;

    public float voicePitch = 1f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}
