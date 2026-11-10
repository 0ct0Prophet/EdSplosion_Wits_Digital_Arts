using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{

    public static NarrationManager Instance { get; private set; }

    public GameObject NarrationDialogParent; //this is what contians all the dialog UI
    public TextMeshProUGUI textComponent;//where the text goes
    public GameObject NarrationButtonPrefab; // Prefab for continue button
    public Transform NarrationButtonContainer; //container which holds buttons
    public NarrationLines lines;// contains narration lines
    private int index;//keeps track of which line you are on

    private void Awake()
    {
 
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        NarrationDialogParent.SetActive(false);
    }
    public void StartNarration() //begins the narration by showing the first line and creating a button to continue
    {
        

        // start from first line
        index = 0;

        // show the narration UI and clear previous buttons
        NarrationDialogParent.SetActive(true);

        foreach (Transform child in NarrationButtonContainer)
        {
            Destroy(child.gameObject);
        }


        ShowLine();

        GameObject buttonObj = Instantiate(NarrationButtonPrefab, NarrationButtonContainer);
        buttonObj.GetComponent<Button>().onClick.AddListener(() => NextLine());
    }

    // Overload: begin narration with a specific NarrationLines asset
    public void StartNarration(NarrationLines Narration)
    {
        // set the manager's lines to the provided asset and start
        lines = Narration;
        StartNarration();
    }

    public void ShowLine() //shows the line of dialog that corresponds to the index number
    {
        if (lines == null || lines.LineAmmount() == 0)
        {
            textComponent.text = string.Empty;
            return;
        }

        if (index < 0 || index >= lines.LineAmmount())
        {
            textComponent.text = string.Empty;

            return;
        }
        StopAllCoroutines(); // Stop any ongoing typing coroutine
        StartCoroutine(TypeOut()); // Start the coroutine to type out the text
    }

    public void NextLine()// goes to the next line of dialog
    {
        
            if (index < lines.LineAmmount() - 1)
            {
                index++;
                ShowLine();
            }
            else
            {
                StartCoroutine(HideNarration());
            }
        }
    
    public IEnumerator HideNarration()
    {
        foreach (Transform child in NarrationButtonContainer)
        {
            Destroy(obj: child.gameObject); // gets rid of "next" button
        }
        if (textComponent != null) textComponent.text = string.Empty;
        index = 0;

        yield return new WaitForSeconds(0.4f); // Wait for the animation to finish
        NarrationDialogParent.SetActive(false);
        
    }



    //types out the text letter by letter
    IEnumerator TypeOut()
    {
        textComponent.text = "";
        foreach (char letter in lines.SetLine(index).ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(0.03f);

        }
    }

    public bool IsNarrationActive() // checks if the narration is currently active
    {
        return NarrationDialogParent.activeSelf;
    }
}
