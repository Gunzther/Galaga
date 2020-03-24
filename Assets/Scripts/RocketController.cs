using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketController : MonoBehaviour
{
    public bool active;
    public Text livesText;
    public float speed, bullet_speed;
    public GameObject pbullet;
    public GameplayManager gm;

    private float timer;
    private int lives;
    private bool startCountdown, cooldown, destroyCoolDown;

    public void ResetValue()
    {
        lives = gm.playerLives;
        livesText.text = "LIVES: " + lives;
        transform.position = new Vector3(0, -4.5f, 0);
        timer = 0;
        startCountdown = false;
        cooldown = false;
        destroyCoolDown = false;
        active = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetValue();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if(transform.position.x > -6.5) transform.position = new Vector3(transform.position.x - speed, -4.5f, 0);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (transform.position.x < 6.5) transform.position = new Vector3(transform.position.x + speed, -4.5f, 0);
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

        if (lives > 0) lives -= 1;
        livesText.text = "LIVES: " + lives;

        if (lives <= 0)
        {
            gm.Gameover();
            active = false;
            return;
        }

        transform.position = new Vector2(0, -6f);
        timer = 0;
        destroyCoolDown = true;
    }
}
