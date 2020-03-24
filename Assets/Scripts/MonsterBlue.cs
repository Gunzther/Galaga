using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBlue : MonoBehaviour, MonsterStrategy
{
    private Text score;
    private float monsterSpeed;
    private bool attack, setEndTarget, goToRocket;
    private GameObject target;
    private Vector2 targetPos, startPos;

    public void Scoring()
    {
        score.text = (int.Parse(score.text) + 100).ToString();
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

    private void SetMonsterValue()
    {
        GameplayManager setting = transform.parent.GetComponent<GameplayManager>();
        score = setting.score;
        monsterSpeed = setting.blueSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetMonsterValue();
        startPos = transform.position;
        attack = false;
        setEndTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            //print("target: " + target.name + " Pos: " + target.transform.position.ToString());

            if (transform.position.y < -5.5f)
            {
                if (!setEndTarget)
                {
                    print("set end target");
                    if(transform.position.x > 0) targetPos = GameObject.Find("CheckPointRight").transform.position;
                    else targetPos = GameObject.Find("CheckPointLeft").transform.position;
                    setEndTarget = true;
                }
                goToRocket = false;
            }

            if(!goToRocket) transform.position = Vector2.MoveTowards(transform.position, targetPos, monsterSpeed * 2 * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - startPos.y) < 0.1 && Mathf.Abs(transform.position.x - startPos.x) < 0.1)
            {
                attack = false;
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, monsterSpeed * Time.deltaTime);
            }
        }
    }
}
