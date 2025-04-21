using UnityEngine;

public class GuardianSpin : MonoBehaviour
{
    public float spinSpeed = 30f; // Degrees per second
    public float cycleDuration;

    void Update()
    {
        float cycleValue = Mathf.Repeat(Time.time, cycleDuration) < cycleDuration/2 ? -1f : 1f;
        transform.Rotate(0, spinSpeed * Time.deltaTime * cycleValue, 0);
    }
}