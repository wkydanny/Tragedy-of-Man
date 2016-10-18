using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

    bool goSpawn;
    bool spawnFloor;

    [SerializeField]
    GameObject MovingFloor;

    [SerializeField]
    GameObject platformController;

    // Use this for initialization
    void Start() {
        goSpawn = false;
        spawnFloor = true;
    }

    // Update is called once per frame
    void Update() {
        spawnFloor = platformController.GetComponent<PlatformController>().spawnFloor;
        //Debug.Log("read spawn = " + spawnFloor);
        if (goSpawn == true)
        {
            GameObject floor = Instantiate(MovingFloor, transform.position, transform.rotation) as GameObject;
            floor.name = "floor";
            floor.GetComponent<SpriteRenderer>().enabled = true;
            floor.transform.Find("floor collision").gameObject.SetActive(true);
            goSpawn = false;
            if (spawnFloor == false)
            {
                floor.GetComponent<SpriteRenderer>().enabled = false;
                floor.transform.Find("floor collision").gameObject.SetActive(false);

            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "barrier")
        {
            goSpawn = true;
        }
    }
}
