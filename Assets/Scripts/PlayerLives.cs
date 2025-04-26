using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    public TextMeshProUGUI livesText;
    public Vector3 respawnPosition;
    public Quaternion respawnRotation;
    private Vector3 originalCameraOffset;
    public TextMeshProUGUI youLoseText;
    public Button restartButton;
    public float invincibleDuration = 1.5f;

    private float invincibleTime = 0;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioSource bgmSource;
    public AudioClip loseLifeSound;
    public AudioClip gameOverSound;



    void Start()
    {
        currentLives = maxLives;
        originalCameraOffset = Camera.main.transform.localPosition;
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
        UpdateLivesDisplay();
    }

    private void Update()
    {
        if (invincibleTime > 0)
        {
            
            invincibleTime -= Time.deltaTime;
            if (invincibleTime < 0) invincibleTime= 0;
            Camera.main.fieldOfView = Mathf.Lerp(60, 45, invincibleTime/ invincibleDuration);
        }
    }



    public void LoseLife()
    {
        if (invincibleTime > 0) return;
        // Play lose life sound
        if (loseLifeSound != null)
        {
            audioSource.PlayOneShot(loseLifeSound);
        }

        if (currentLives <= 0) return;
        currentLives--;
        UpdateLivesDisplay();
        

        if (currentLives <= 0)
        {
            currentLives = 0;
            UpdateLivesDisplay();
            

            if (youLoseText != null)
            { // Play game over sound
                invincibleTime = 9999;
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
            GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().FreezeTime();
        }
        else
        {
            // give invincibility
            invincibleTime = invincibleDuration;
        }
    }

    void ResetPlayer()
    {

        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;

        // Reset entire player hierarchy position
        transform.position = respawnPosition;
        transform.rotation = respawnRotation;

        if (Camera.main.transform.parent == transform)
        {
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.localRotation = Quaternion.identity;
        }

        // Re-enable and reset state
        cc.enabled = true;
        GetComponent<PlayerMovement>().setIsGameEnd(false);
        GetComponent<PlayerMovement>().ResetVelocity();

        // Optional: Add brief invincibility
        StartCoroutine(TemporaryInvincibility());
    }
    IEnumerator TemporaryInvincibility()
    {
        // Add visual feedback like blinking
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(invincibleDuration);
        GetComponent<Collider>().enabled = true;
    }

   
    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {   
            livesText.text = "Lives: " + currentLives;
            
        }
    }
}