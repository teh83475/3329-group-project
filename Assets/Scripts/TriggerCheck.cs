using UnityEngine;
using TMPro;
public class TriggerCheck : MonoBehaviour
{
    private bool isWin = false;
    public TextMeshProUGUI youWinText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (youWinText != null)
        {
            youWinText.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger Hit");
            if (youWinText != null)
            {
                youWinText.gameObject.SetActive(true);
            }
        }
    }

}
