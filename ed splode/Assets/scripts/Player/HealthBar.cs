using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    [SerializeField] Sprite[] healthSprites;
    private Sprite newSprite;

    public void SetSprite(int amount)
    {
        newSprite = healthSprites[amount];
        gameObject.GetComponent<Image>().sprite = newSprite;
    }

    public void SetSlider(float amount)
    {
        healthSlider.value = amount;
    }
    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount;
        SetSlider(amount);
    }
}