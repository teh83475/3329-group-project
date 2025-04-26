using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TriggerCheck : MonoBehaviour
{
    public AudioSource winAudioSource;
    public AudioSource bgmSource;
    public AudioClip winSound;
    public TextMeshProUGUI youWinText;
    public TextMeshProUGUI finalTimeText;
    public Button restartButton;

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
            //Debug.Log("Trigger Hit");

            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            Timer timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
            timer.FreezeTime();
            restartButton.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (playerMovement != null) {
                playerMovement.setIsGameEnd(true);
            }
            if (youWinText != null)
            {// Play win sound
                if (winSound != null)
                {
                    winAudioSource.PlayOneShot(winSound);
                }
                bgmSource.Stop();
                youWinText.gameObject.SetActive(true);
            }
            if (finalTimeText != null)
            {
                finalTimeText.text = "Time Used: " + Time.timeSinceLevelLoad.ToString();
                finalTimeText.gameObject.SetActive(true);
            }
        }
    }

}
