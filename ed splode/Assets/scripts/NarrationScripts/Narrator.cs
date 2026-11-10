using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public string name;
    public List<NarrationLines> lines;
    NarrationLines Narration;



    //Triggers the dialogue with the character
    public void Narrate(int num)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Narration = lines[num];
        NarrationManager.Instance.StartNarration(Narration);

    }

}
