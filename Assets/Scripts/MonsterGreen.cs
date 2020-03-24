using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterGreen : MonoBehaviour, MonsterStrategy
{
    private Text score;
    private float monsterSpeed, timer;
    private bool active, setEndTarget, goToRocket;
    private GameObject target, leftChecker, rightChecker;
    private Vector2 targetPos, startPos;

    public void Scoring()
    {
        score.text = (int.Parse(score.text) + 300).ToString();
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
        transform.position = startPos;
    }

    public void ActiveMove()
    {
        target = GameObject.FindGameObjectWithTag("rocket");
        transform.rotation = Quaternion.AngleAxis(180, new Vector3(0, 0, 1));
        transform.GetComponent<Rigidbody2D>().velocity = transform.up * (monsterSpeed/2);
        active = true;
        goToRocket = true;
    }

    public void AttackPlayer()
    {
        goToRocket = false;
        transform.GetComponent<Rigidbody2D>().velocity = transform.up * 0;
        Shoot();
    }

    public void BackToStartPoint()
    {
        transform.position = new Vector2(startPos.x - 2, 10);
        transform.rotation = Quaternion.AngleAxis(0, new Vector3(0, 0, 1));
        transform.GetComponent<Rigidbody2D>().velocity = transform.up * 0;
        targetPos = startPos;
    }

    private void Shoot()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Awake()
    {
        GameplayManager setting = transform.parent.GetComponent<GameplayManager>();
        score = setting.score;
        monsterSpeed = setting.blueSpeed;
        leftChecker = setting.leftChecker;
        rightChecker = setting.rightChecker;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        active = false;
        setEndTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (!goToRocket)
            {
                if (!setEndTarget)
                {
                    timer += Time.deltaTime;
                    if (timer > 1)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        if (transform.position.x > 0) targetPos = rightChecker.transform.position;
                        else targetPos = leftChecker.transform.position;
                        setEndTarget = true;
                    }
                }

                else transform.position = Vector2.MoveTowards(transform.position, targetPos, monsterSpeed * 2 * Time.deltaTime);

                if (Mathf.Abs(transform.position.y - startPos.y) < 0.1 && Mathf.Abs(transform.position.x - startPos.x) < 0.1)
                {
                    active = false;
                    setEndTarget = false;
                }
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position + new Vector3(0, 1.9f, 0), monsterSpeed * Time.deltaTime);
            }
        }
    }
}
