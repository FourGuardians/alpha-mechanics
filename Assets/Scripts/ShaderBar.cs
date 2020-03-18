using UnityEngine;
using UnityEngine.UI;

public class ShaderBar : MonoBehaviour 
{
    public float MinValue = 0f;
    public float MaxValue = 1f;

    public float MinResult = 0f;
    public float MaxResult = 1f;

    public float Step = 0.01f;
    public float Target = 1f;

    private float TargetValue => Target.Map(MinValue, MaxValue, MinResult, MaxResult);
    private Material material;

    public void Start()
    {
        material = gameObject.GetComponent<Image>().material;
    }

    public void Update()
    {
        if (TargetValue < MinResult || TargetValue > MaxResult)
            return;

        float amount = material.GetFloat("_Amount");

        if (amount < TargetValue)
            if (amount + Step >= TargetValue)
                material.SetFloat("_Amount", TargetValue);
            else
                material.SetFloat("_Amount", amount + Step);

        if (amount > TargetValue)
            if (amount - Step <= TargetValue)
                material.SetFloat("_Amount", TargetValue);
            else
                material.SetFloat("_Amount", amount - Step);
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