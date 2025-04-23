using UnityEngine;

public class GuardianSpin : MonoBehaviour
{
    public float spinSpeed = 30f; // Degrees per second
    public float cycleDuration;
    public float startTime = 0;
    public float initAngle = 0;

    private void Awake()
    {
        transform.rotation = Quaternion.Euler(0, initAngle, 0);
    }

    void Update()
    {

        if (Time.timeSinceLevelLoad > startTime)
        {
            float cycleValue = Mathf.Repeat(Time.timeSinceLevelLoad - startTime, cycleDuration) < cycleDuration / 2 ? -1f : 1f;
            transform.Rotate(0, spinSpeed * Time.deltaTime * cycleValue, 0);
        }
        
    }
}