using UnityEngine;

public class SliderTest : MonoBehaviour
{
    [MinMatch("minValue", 0f, 2f)]
    public float value = 1f;
    [Range(0f, 2f)]
    public float minValue = 0f;
}