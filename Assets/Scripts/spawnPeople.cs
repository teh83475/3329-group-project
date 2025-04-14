using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPeople : MonoBehaviour
{
    private List<GameObject> people = new List<GameObject>();
    public GameObject[] peopleModels;
    private float movementSpeed = 2.0f;

    public float spawnTime = 0.5f;
    void Start()
    {
        InvokeRepeating("addEnemy", spawnTime, spawnTime);
    }

    void addEnemy()
    {
        Renderer rd = GetComponent<Renderer>();
        float s = rd.bounds.size.x / 2;

        float x1 = transform.position.x - s;
        float x2 = transform.position.x + s;

        Vector3 spawnPoint = new Vector3(Random.Range(x1, x2), 0.2f, transform.position.z);
        int randomIndex = Random.Range(0, peopleModels.Length);
        GameObject selectedModel = peopleModels[randomIndex];
        GameObject person = Instantiate(selectedModel, spawnPoint, Quaternion.identity);

        Quaternion toRotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
        person.transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 5f * Time.deltaTime);
        people.Add(person);
    }

    void Update()
    {
        for (int i = 0; i < people.Count; i++)
        {
            people[i].transform.Translate(Vector3.back *Time.deltaTime* movementSpeed, Space.World) ;
        }
  
    }
}


