using System.Collections;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;

public class DialogManager : MonoBehaviour
{
   
    public Narrator narrator;
    
    public void Start()
    {
        StartCoroutine(StartTalking());
     
    }

    public IEnumerator StartTalking()
    {
       
        narrator.Narrate(0);
        yield return new WaitUntil(() => !NarrationManager.Instance.IsNarrationActive());
        
        

    }
        
        
    



}