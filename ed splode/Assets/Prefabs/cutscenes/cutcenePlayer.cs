using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class cutcenePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float lengthOfVideo;
    public string SceneToLoad;
public void Start()
    {
        videoPlayer.Play();
        StartCoroutine(PlayVideo());
    }

    public IEnumerator PlayVideo()
    {
        yield return new WaitForSeconds(lengthOfVideo);
        Debug.Log("loading next scene");
        SceneManager.LoadScene(SceneToLoad);
    }

}
