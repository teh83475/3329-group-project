using UnityEngine;

public class walkingPeople : MonoBehaviour
{
    private float movementSpeed = 2.0f;

    // Update is called once per frame
    void Update()
    {
        //print("LOCATION:" + gameObject.transform.position);
        if (gameObject.transform.position[2] <= -153)
        {
            Destroy(gameObject);
        }
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * movementSpeed, Space.World);
        
    }
}
