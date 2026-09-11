using System.Collections;
using UnityEngine;

public class Flashcolour : MonoBehaviour
{
    public Renderer rend;
    public Color flashColour = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColor;

    private void Start()
    {
        originalColor = rend.material.color;
    }

    private IEnumerator DoFlash()
    {
        rend.material.color = flashColour;
        yield return new WaitForSeconds(flashDuration);
        rend.material.color = originalColor;
    }

    public void Flash()
    {
        StopAllCoroutines();
        StartCoroutine(DoFlash());
    }

}
