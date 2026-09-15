using UnityEngine;


[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject
{
    [Header("NPC Dialogue Settings")]
    public string npcName;
    public Sprite npcPotrait;
    public string[] dialogueLines;
    public bool[] autoProgressLine;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip[] voiceClips;
    public float voicePitch = 1.0f;


    



}


