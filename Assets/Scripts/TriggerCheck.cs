using UnityEngine;
using TMPro;

public class TriggerCheck : MonoBehaviour
{
    public TextMeshProUGUI youWinText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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

            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null) {
                playerMovement.setIsGameEnd(true);
            }
            if (youWinText != null)
            {
                youWinText.gameObject.SetActive(true);
            }
        }
    }

}
