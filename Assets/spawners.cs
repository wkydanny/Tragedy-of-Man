using UnityEngine;
using System.Collections;

public class spawners : MonoBehaviour {

    public GameObject player;
    public GameObject godOfDeath;
    public GameObject pill;
    public GameObject money;
    public GameObject[] cars;
    public GameObject[] flights;
    public bool goodToInstantiate;
    float ENEMYSPAWNTIME;
    float MONEYSPAWNTIME;
    float timeToSpawn;
    float timeToSpawn2;
    int randNum;
    int age;
    int sickAge;
    int tempNum;
    int sickChance;

	// Use this for initialization
	void Start () {
        goodToInstantiate = false;
        sickAge = 0;
        sickChance = 100;
        ENEMYSPAWNTIME = 4f;
        MONEYSPAWNTIME = 1f;
        timeToSpawn = ENEMYSPAWNTIME;
        timeToSpawn2 = MONEYSPAWNTIME;
        //Debug.Log(cars.Length);
	}
	
	// Update is called once per frame
	void Update () {
        age = (int)player.GetComponent<PlayerMovement>().age;
        tempNum = Random.Range(1, 100);
        if (age == tempNum)
        {
            tempNum = Random.Range(1, sickChance);
            if (tempNum == 1 && (sickAge+10) < age)
            {
                goodToInstantiate = true;
            }
        }
        if (age > 20 && age <60)
        {
            sickChance = 10;
        }
        else if (age > 60 && age < 100)
        {
            sickChance = 5;
        }
        else if (age > 100)
        {
            sickChance = 2;
        }
        if (goodToInstantiate == true)
        {
            GameObject GOD = Instantiate(godOfDeath, transform.position, transform.rotation) as GameObject;
            goodToInstantiate = false;
            sickAge = age;
            StartCoroutine(WaitFor(3f));
        }

        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <=0)
        {
            randNum = Random.Range(0, 3);
            if (randNum == 0)
            {
                randNum = (int)Random.Range(0, cars.Length);
                GameObject ENEMY = Instantiate(cars[randNum], transform.position, transform.rotation) as GameObject;
                timeToSpawn = ENEMYSPAWNTIME;
            }
            else if (randNum == 1)
            {
                randNum = (int)Random.Range(0, flights.Length);
                Vector3 NewPos = this.transform.position;
                NewPos.y += 3/2;
                GameObject ENEMY = Instantiate(flights[randNum], NewPos, transform.rotation) as GameObject;
                timeToSpawn = ENEMYSPAWNTIME;
            }
            else
            {
                timeToSpawn = ENEMYSPAWNTIME;
            }
        }

        timeToSpawn2 -= Time.deltaTime;
        if (timeToSpawn2 <= 0)
        {
            randNum = Random.Range(0, 8);
            if (randNum == 0)
            {
                GameObject MONEY = Instantiate(money, transform.position, transform.rotation) as GameObject;
            }
            else if (randNum == 1)
            {
                Vector3 NewPos = this.transform.position;
                NewPos.y += 1 / 2;
                GameObject MONEY = Instantiate(money, NewPos, transform.rotation) as GameObject;
            }
            else if (randNum == 2)
            {
                Vector3 NewPos = this.transform.position;
                NewPos.y += 1;
                GameObject MONEY = Instantiate(money, NewPos, transform.rotation) as GameObject;
            }
            else if (randNum == 3)
            {
                Vector3 NewPos = this.transform.position;
                NewPos.y += 3 / 2;
                GameObject MONEY = Instantiate(money, NewPos, transform.rotation) as GameObject;
            }
            timeToSpawn2 = MONEYSPAWNTIME;

        }

    }

    IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Vector3 NewPos = this.transform.position;
        NewPos.y += 1;
        GameObject PILL = Instantiate(pill, NewPos, transform.rotation) as GameObject;
    }
}
