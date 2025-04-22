using UnityEngine;

public class UIElement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject text = gameObject;
        text.SetActive(false);
    }
}
