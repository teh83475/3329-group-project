using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Audio;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private bool timeFreezed = false;

    public TextMeshProUGUI youLoseText;
    public Button restartButton;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioSource bgmSource;
    public AudioClip loseLifeSound;
    public AudioClip gameOverSound;

    // Update is called once per frame
    void Update()
    {
        if (!timeFreezed)
        {
            

            if (60 -  Time.timeSinceLevelLoad <= 0)
            {
                FreezeTime();
                timerText.text = "Time Left: 0";
                if (youLoseText != null)
                { // Play game over sound
                    if (gameOverSound != null)
                    {
                        audioSource.PlayOneShot(gameOverSound);
                    }
                    bgmSource.Stop();


                    youLoseText.gameObject.SetActive(true);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    restartButton.gameObject.SetActive(true);

                }

                GetComponent<PlayerMovement>().setIsGameEnd(true);
                
                return;
            }

            timerText.text = "Time Left: " + (60 - Time.timeSinceLevelLoad).ToString();
        }
        

    }

    public void FreezeTime()
    {
        timeFreezed=true;
    }
}
