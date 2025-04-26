using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuardianDetection : MonoBehaviour
{
    public float visionAngle = 130f;
    public float visionRange = 5f;
    public string playerTag = "Player";

    public TextMeshProUGUI youLoseText;
    public Button restartButton;

    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player == null) return;

        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        Debug.Log("hv player!");
        if (angleToPlayer < visionAngle / 2f)
        {
            Debug.Log("wi thin angle!");
            if (directionToPlayer.magnitude < visionRange)
            {
                Debug.Log("within rangle!");
                // Player detected! 
                Debug.Log("Game Over!");
                PlayerLost();
            }
        }
    }

    public void PlayerLost()
    {
        Debug.Log("Player detected! Game Over.");
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();
        PlayerLives playerLives = playerMovement.GetComponent<PlayerLives>();
        Timer timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
       
        if (playerLives != null)
        {
            playerLives.LoseLife();
        }
        else // Fallback to original behavior if no lives system
        {
            timer.FreezeTime();
            if (playerMovement != null)
            {
                playerMovement.setIsGameEnd(true);
                FindObjectsByType<Timer>(FindObjectsSortMode.None)[0].FreezeTime();
            }
            if (youLoseText != null)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                youLoseText.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);
            }
        }
    }

    // Optional: Visualize the vision cone in Scene view
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float halfAngle = visionAngle / 2f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfAngle, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfAngle, Vector3.up);
        Vector3 leftRayDirection = leftRayRotation * transform.forward;
        Vector3 rightRayDirection = rightRayRotation * transform.forward;
        Gizmos.DrawRay(transform.position, leftRayDirection * visionRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * visionRange);
    }
}