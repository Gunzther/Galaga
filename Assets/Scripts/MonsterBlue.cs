using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBlue : MonoBehaviour, MonsterStrategy
{
    private Text score;
    private float monsterSpeed;
    private bool attack, setEndTarget, goToRocket;
    private GameObject target, leftChecker, rightChecker;
    private Vector2 targetPos, startPos;

    public void Scoring()
    {
        score.text = (int.Parse(score.text) + 100).ToString();
    }

    public void ResetPos()
    {
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = startPos;
        attack = false;
        setEndTarget = false;
    }

    public void ActiveMove()
    {
        transform.GetComponent<Rigidbody2D>().velocity = -transform.up * monsterSpeed;
    }

    public void AttackPlayer()
    {
        target = GameObject.FindGameObjectWithTag("rocket");
        attack = true;
        goToRocket = true;
    }

    public void BackToStartPoint()
    {
        transform.position = new Vector2(startPos.x - 2, 10);
        transform.GetComponent<Rigidbody2D>().velocity = -transform.up * 0;
        targetPos = startPos;
        setEndTarget = false;
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
        attack = false;
        setEndTarget = false;
    }

    // Update monster movement
    void Update()
    {
        if (attack)
        {
            if (transform.position.y < -5.5f)
            {
                if (!setEndTarget)
                {
                    if(transform.position.x > 0) targetPos = rightChecker.transform.position;
                    else targetPos = leftChecker.transform.position;
                    setEndTarget = true;
                }
                goToRocket = false;
            }

            if(!goToRocket) transform.position = Vector2.MoveTowards(transform.position, targetPos, monsterSpeed * 2f * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos.y) < 0.1 && Mathf.Abs(transform.position.x - startPos.x) < 0.1)
            {
                Start();
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, monsterSpeed * Time.deltaTime);
            }
        }
    }
}
