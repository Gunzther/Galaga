using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    public bool active;
    public int lives = 3;
    public Text livesText;
    public float speed, bullet_speed;
    public GameObject pbullet;

    private float timer;
    private bool startCountdown, cooldown, destroyCoolDown;

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = "LIVES: " + lives;

        if (active)
        {
            transform.position = new Vector3(0, -4.5f, 0);
            timer = 0;
            startCountdown = false;
            cooldown = false;
            destroyCoolDown = false;
            transform.tag = "rocket";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = new Vector3(transform.position.x - speed, -4.5f, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = new Vector3(transform.position.x + speed, -4.5f, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space) && !cooldown)
            {
                GameObject bul = GameObject.Instantiate(pbullet);
                bul.transform.position = new Vector3(transform.position.x, -4.3f, 0);
                bul.GetComponent<Rigidbody2D>().velocity = transform.up * bullet_speed;
                startCountdown = true;
            }

            if (startCountdown) //Shooting cool down
            {
                timer += Time.deltaTime;
                if (timer > Random.Range(0.3f, 0.5f))
                {
                    cooldown = true;
                }
                if (timer > Random.Range(0.7f, 0.75f))
                {
                    cooldown = false;
                    startCountdown = false;
                    timer = 0;
                }
            }
        }

        if (destroyCoolDown) //Cool down when rocket be destroyed
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                destroyCoolDown = false;
                timer = 0;
                transform.position = new Vector2(0, -4.5f);
                this.GetComponent<SpriteRenderer>().enabled = true;
                this.GetComponent<Collider2D>().enabled = true;
                active = true;
            }
        }
    }
    public void Destroyed()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        active = false;

        lives -= 1;
        livesText.text = "LIVES: " + lives;

        transform.position = new Vector2(0, -6f);
        timer = 0;
        destroyCoolDown = true;
    }
}
