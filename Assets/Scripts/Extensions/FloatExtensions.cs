public static class FloatExtensions
{
    public static float Map(this float value, float a1, float a2, float b1, float b2) =>
        b1 + (value - a1) * (b2 - b1) / (a2 - a1);
}