using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

    [SerializeField]
    GameObject godOfDeath;
    bool meetTheDeath;

    [SerializeField]
    GameObject player;
    float age;

    float randNum;
    float countDown;
    float nextRandPeriod;
    
    public bool spawnFloor;
    // Use this for initialization
    void Start () {
        spawnFloor = true;
        countDown = 2f;
        nextRandPeriod = 2f;
        meetTheDeath = false;
	}
	
	// Update is called once per frame
	void Update () {
        age = player.GetComponent<PlayerMovement>().age;
        randNum = Random.Range(0, 10);                  // 10% chance will occur no floor
        nextRandPeriod -= Time.deltaTime;
        
        if (randNum == 1 && nextRandPeriod < 0 && age < 55)
        {
            spawnFloor = false;
            countDown = Random.Range(1, 4);             // no floor will remain for 1 ~ 3 seconds only
            nextRandPeriod = Random.Range(10, 30);      // no floor may occur every 10 ~ 30 seconds
        }

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        else
        {
            spawnFloor = true;
        }

        if (age > 60)
        {
            randNum = Random.Range(0, 10);                  // 10% chance will meet god of death
            if (randNum == 1)
            {
                GameObject demon = Instantiate(godOfDeath, transform.position, transform.rotation) as GameObject;
            }
        }
    }
}
