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

   

    void Start()
    {
        currentLives = maxLives;
        originalCameraOffset = Camera.main.transform.localPosition;
        respawnPosition = transform.position;
        respawnRotation = transform.rotation;
        UpdateLivesDisplay();
    }
    
    

    public void LoseLife()
    {
        if (currentLives <= 0) return;
        currentLives--;
        UpdateLivesDisplay();
        

        if (currentLives <= 0)
        {
            currentLives = 0;
            UpdateLivesDisplay();
            

            if (youLoseText != null)
            {
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
            // Reset player position 
            ResetPlayer();
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
        yield return new WaitForSeconds(1f);
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