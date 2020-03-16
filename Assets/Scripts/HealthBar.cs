using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour 
{
    public float MinValue = 0f;
    public float MaxValue = 1f;

    public float MinResult = 0f;
    public float MaxResult = 1f;

    public float Step = 0.01f;
    public float Target = 1f;

    private float TargetValue => Target.Map(MinValue, MaxValue, MinResult, MaxResult);
    private Image image;

    public void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void Update()
    {
        if (TargetValue < MinResult || TargetValue > MaxResult)
            return;

        if (image.fillAmount < TargetValue)
            if (image.fillAmount + Step >= TargetValue)
                image.fillAmount = TargetValue;
            else
                image.fillAmount += Step;

        if (image.fillAmount > TargetValue)
            if (image.fillAmount - Step <= TargetValue)
                image.fillAmount = TargetValue;
            else
                image.fillAmount -= Step;
    }

    public void ApplyHealth(Player p)
    {
        MinValue = 0f;
        MaxValue = p.MaxHealth;

        Target = p.Health;
    }

    public void ApplyInk(Player p)
    {
        MinValue = 0f;
        MaxValue = p.MaxInk;

        Target = p.Ink;
    }
}