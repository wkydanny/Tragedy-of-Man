using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public GameObject ageText;
    public GameObject sickText;
    public GameObject goldText;
    bool sickStatus;
    float jumpPower;
    public float movePower;
    public float age;
    public bool landOnFloor;
    bool alive;
    float money;
    float liferemaining;
    float lifenow;
    public GameObject baby;         // age of 0-1
    public GameObject kid;          // age of 2-10
    public GameObject teen;         // age of 11-17
    public GameObject adult;        // age of 18-59
    public GameObject oldman;       // age of 60+
    public GameObject grave;        // die

    // Use this for initialization
    void Start () {
        alive = true;
        age = 0;
        jumpPower = 150f;
        movePower = 1f;
        landOnFloor = false;
        liferemaining = 100f;
        lifenow = 0f;
        sickStatus = false;
        money = 200;
	}

    // Update is called once per frame
    void Update()
    {
        // assure player doesn't rotate as a rigidbody
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        if (alive == true)
        {
            age += Time.deltaTime * 0.5f;  // * 0.1f
            goldText.GetComponent<Text>().text = Mathf.Round(money).ToString();
            money -= Time.deltaTime * 3f;

            //  UI Text to display the age
            if (age == 0)
            {
                ageText.GetComponent<Text>().text = "Welcome to this tragedy world...";
            }
            else
            {
                ageText.GetComponent<Text>().text = "Your age is " + Mathf.Round(age).ToString();
            }

            // set active of play's apperance according to the age
            if (age > 1 && age <= 10)   // for age of 2~10
            {
                baby.SetActive(false);
                kid.SetActive(true);
                jumpPower = 200f;
            }
            else if (age > 10 && age <= 17)   // for age of 11~17
            {
                kid.SetActive(false);
                teen.SetActive(true);
                jumpPower = 250f;
            }
            else if (age > 17 && age <= 59)   // for age of 18~59
            {
                teen.SetActive(false);
                adult.SetActive(true);
                jumpPower = 300f;
            }
            else if (age > 59)   // for age of 60+
            {
                adult.SetActive(false);
                oldman.SetActive(true);
                jumpPower = 150f;
            }

        }
        else
        {
            ageText.GetComponent<Text>().text = "You die at the age of " + Mathf.Round(age).ToString() + ". R.I.P.~~~~~~~";
            transform.position = Vector3.Lerp(transform.position, new Vector2(0f, -0.08f), movePower / 100f);
            liferemaining -= Time.deltaTime;
            if ( Vector3.Magnitude(transform.position)< 0.1 && liferemaining < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            if (money <= 0)
            {
                goldText.GetComponent<Text>().text = "Loser";
            }
        }
        if (sickStatus == true)
        {
            if(liferemaining > 0)
            {
                liferemaining -= Time.deltaTime;
                sickText.GetComponent<Text>().text = "Sorry, you still have " + Mathf.Round(liferemaining).ToString() + " years to live.";
            }
        }
        else
        {
            sickText.GetComponent<Text>().text = "";
        }
        if (money <= 0)
        {
            sickText.GetComponent<Text>().text = "You committed suicide because you declared bankrupt.";
        }
        if (liferemaining <= 0 || money <= 0)
        {
            alive = false;
            baby.SetActive(false);
            kid.SetActive(false);
            teen.SetActive(false);
            adult.SetActive(false);
            oldman.SetActive(false);
            grave.SetActive(true);
            GetComponent<Rigidbody2D>().isKinematic = true;
            liferemaining = 2f;
        }

    }

    void FixedUpdate()
    {
        if (alive == true)
        {
            if (Input.GetKeyDown("space") && landOnFloor == true)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.up * jumpPower);
            }
            Vector3 moveDir = Vector3.zero;
            moveDir.x = Input.GetAxis("Horizontal"); // get result of AD keys in X
            transform.position += moveDir * movePower * Time.deltaTime;
            if (!Input.anyKey)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector2(0f, 0f), movePower / 100f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (alive == true)
        {
            if (other.tag == "floor")
            {
                landOnFloor = true;
            }
            if (other.tag == "fatal" && alive == true)
            {
                alive = false;
                baby.SetActive(false);
                kid.SetActive(false);
                teen.SetActive(false);
                adult.SetActive(false);
                oldman.SetActive(false);
                grave.SetActive(true);
                GetComponent<Rigidbody2D>().isKinematic = true;
                liferemaining = 2f;
            }
            if (other.tag == "sick")
            {
                liferemaining = Random.Range(3f, 10f);
                lifenow = age;
                sickStatus = true;
                other.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (other.tag == "cure")
            {
                liferemaining = 100f;
                sickStatus = false;
                Destroy(other.gameObject);
            }
            if (other.tag == "gold")
            {
                money += 10;
                Destroy(other.gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "floor")
        {
            landOnFloor = false;
        }
    }
}
