using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class DialogHandler : MonoBehaviour
{

    public Narrator narrator;
    public string sceneToLoad;
    public GameObject NPC;
    public GameObject transition;
    public void Start()
    {
        StartCoroutine(StartTalking());

    }

    public IEnumerator StartTalking()
    {
        yield return new WaitForSeconds(2);
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        transition.GetComponent<Animator>().Play("transition2");
        yield return new WaitForSeconds(.4f);
        SceneManager.LoadScene(sceneToLoad);

    }

}