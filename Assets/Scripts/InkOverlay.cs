using UnityEngine;

public class InkOverlay : MonoBehaviour
{
    [Header("Ink Bar Preferences")]
    public float MinResult = 0f;
    public float MaxResult = 1f;
    public float Step = 0.0005f;
    
    [Header("Ink Bars")]
    public HealthBar[] InkBars;


    public HealthBar HealthBar => InkBars[Selected];
    public int Selected { get; private set; } = 0;

    public void Start()
    {
        foreach (HealthBar bar in InkBars)
        {
            bar.MinResult = MinResult;
            bar.MaxResult = MaxResult;
            bar.Step = Step;
        }

        Select(0);
        Globals.InkOverlay = this;
    }

    public void Select(int value)
    {
        value %= 4;

        for (int i = 0; i < InkBars.Length; i++)
            InkBars[i].gameObject.SetActive(i == value);

        Selected = value;
    }
}