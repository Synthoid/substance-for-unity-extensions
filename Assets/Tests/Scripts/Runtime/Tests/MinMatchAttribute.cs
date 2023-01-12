using UnityEngine;

public class MinMatchAttribute : PropertyAttribute
{
    public readonly string target = "";
    public readonly float min = 0f;
    public readonly float max = 0f;

    public MinMatchAttribute(string target, float min, float max)
    {
        this.target = target;
        this.min = min;
        this.max = max;
    }
}