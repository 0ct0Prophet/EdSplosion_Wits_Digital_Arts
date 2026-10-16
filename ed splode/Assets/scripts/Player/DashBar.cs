using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    [SerializeField] Sprite[] dashSprites;
    private Sprite newSprite;

    public void SetDashSprite(int amount)
    {
        newSprite = dashSprites[amount];
        gameObject.GetComponent<Image>().sprite = newSprite;
    }

}