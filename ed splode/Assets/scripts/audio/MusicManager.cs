using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource scratchSource;
    public AudioClip recordScratch;
    public AudioClip elevatorDing;
    [SerializeField] private bool playElevatorDing = true;

    public float fadeDuration = 3f;

    private Coroutine fadeCoroutine;
    private bool playerDied = false;

    private void Start()
    {
        if (musicSource != null && musicSource.clip != null)
        {
            float songLength = musicSource.clip.length;
            float latestStartTime = Mathf.Max(0f, songLength - 30f);
            musicSource.time = Random.Range(0f, latestStartTime);
            musicSource.Play();
        }
    }

    public void FadeOut()
    {
        if (playerDied || fadeCoroutine != null)
            return;

        fadeCoroutine = StartCoroutine(FadeMusic());
    }

    private IEnumerator FadeMusic()
    {
        float startVolume = musicSource.volume;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
        fadeCoroutine = null;
    }

    public void PlayerDied()
    {
        if (playerDied)
            return;

        playerDied = true;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        musicSource.Stop();

        scratchSource.PlayOneShot(recordScratch);
    }

    public void ElevatorReady()
    {
        if (playerDied)
            return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        musicSource.Stop();

        if (playElevatorDing)
        {
            scratchSource.PlayOneShot(elevatorDing);
        }
    }
}