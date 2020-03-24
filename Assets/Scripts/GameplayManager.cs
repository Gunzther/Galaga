using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public Text score, gameoverText;
    public float blueSpeed, redSpeed, greenSpeed;
    public int redBulletAmounts;
    public float redBulletSpeed;
    public GameObject mbullet, leftChecker, rightChecker;
    public RocketController rc;
    public int playerLives;

    private float timer;
    private List<int> randNum, childIndex;
    private bool activeMonster, restart, gameover, showText;

    public void RemoveChildIndex(int index)
    {
        childIndex.Remove(index);
        if(childIndex.Count() == 0)
        {
            RestartGame();
            if (!gameover) Start();
        }
    }

    public void RestartGame()
    {
        restart = true;

        foreach (Transform child in transform)
        {
            child.GetComponent<MonsterStrategy>().ResetPos();
            child.GetComponent<SpriteRenderer>().enabled = true;
            print("set sprite");
            child.gameObject.SetActive(true);
            print("set active");
        }
    }

    public void Gameover()
    {
        gameover = true;
    }

    void Start()
    {
        timer = 0;
        randNum = new List<int>();
        activeMonster = false;
        childIndex = Enumerable.Range(0, 40).ToList();
        restart = false;
        gameover = false;
    }

    void Update()
    {
        if (!gameover)
        {
            timer += Time.deltaTime;
            if (timer > 2 && !activeMonster)
            {
                randNum = new List<int>();

                int length = 3;
                if (childIndex.Count() < 3) length = childIndex.Count();

                for (int i = 0; i < length; i++)
                {
                    int num = childIndex[Random.Range(0, childIndex.Count() - 1)];
                    if (!randNum.Contains(num) && transform.GetChild(num).gameObject.activeSelf)
                    {
                        randNum.Add(num);
                        transform.GetChild(num).GetComponent<MonsterStrategy>().ActiveMove();
                    }
                }
                if (!restart) activeMonster = true;
            }
            if (timer > 4)
            {
                timer = 0;
                activeMonster = false;
            }
        }
        if (gameover)
        {
            if (!showText)
            {
                gameoverText.gameObject.SetActive(true);
                RestartGame();
                showText = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            { 
                SceneManager.LoadScene("Gameplay");
            }
        }
    }
}
