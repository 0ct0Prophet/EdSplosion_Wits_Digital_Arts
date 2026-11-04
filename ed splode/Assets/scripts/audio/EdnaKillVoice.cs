using UnityEngine;
using System.Collections.Generic;

public class EdnaKillVoice : MonoBehaviour
{
    [Header("Voice Lines")]
    public AudioSource voiceSource;
    public AudioClip[] voiceLines;

    [Header("Individual Line Volumes")]
    public float[] lineVolumes = { 1f, 1f, 1f, 1f, 1f };

    private const int KillsPerVoiceLine = 3;

    private int killCount = 0;
    private int lastPlayedIndex = -1;

    // Remember enemies that have already been counted
    private HashSet<int> countedEnemies = new HashSet<int>();

    public void RegisterKill(GameObject enemy)
    {
        if (enemy == null)
            return;

        int enemyID = enemy.GetInstanceID();

        // Never count the same enemy twice
        if (!countedEnemies.Add(enemyID))
            return;

        killCount++;

        Debug.Log("EDNA KILLS: " + killCount +
                  " | Enemy: " + enemy.name);

        // Only speak every third kill
        if (killCount % KillsPerVoiceLine != 0)
            return;

        PlayRandomVoiceLine();
    }

    private void PlayRandomVoiceLine()
    {
        if (voiceSource == null || voiceLines == null ||
            voiceLines.Length == 0)
            return;

        // Don't interrupt an existing line
        if (voiceSource.isPlaying)
            return;

        List<int> availableLines = new List<int>();

        for (int i = 0; i < voiceLines.Length; i++)
        {
            if (voiceLines[i] != null && i != lastPlayedIndex)
                availableLines.Add(i);
        }

        // Only allow a repeat if there's one valid clip
        if (availableLines.Count == 0)
        {
            for (int i = 0; i < voiceLines.Length; i++)
            {
                if (voiceLines[i] != null)
                    availableLines.Add(i);
            }
        }

        if (availableLines.Count == 0)
            return;

        int selectedIndex =
            availableLines[Random.Range(0, availableLines.Count)];

        float volume = 1f;

        if (lineVolumes != null &&
            selectedIndex < lineVolumes.Length)
        {
            volume = Mathf.Max(0f, lineVolumes[selectedIndex]);
        }

        voiceSource.PlayOneShot(voiceLines[selectedIndex], volume);

        lastPlayedIndex = selectedIndex;
    }
}