using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public Text score;
    public float blueSpeed, redSpeed, greenSpeed;
    public int redBulletAmounts;
    public float redBulletSpeed;
    public GameObject mbullet, leftChecker, rightChecker;

    private float timer;
    private List<int> randNum, childIndex;
    private bool activeMonster, restart;

    public void removeChildIndex(int index)
    {
        childIndex.Remove(index);
        print(childIndex.Count());
        if(childIndex.Count() == 0)
        {
            restart = true;

            foreach (Transform child in transform)
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
                print("set sprite");
                child.gameObject.SetActive(true);
                print("set active");
            }
            Start();
        }
    }

    void Start()
    {
        timer = 0;
        randNum = new List<int>();
        activeMonster = false;
        childIndex = Enumerable.Range(0, 40).ToList();
        restart = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3 && !activeMonster)
        {
            randNum = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                int num = childIndex[Random.Range(0, childIndex.Count() - 1)];
                if (!restart && !randNum.Contains(num) && transform.GetChild(num).gameObject.activeSelf)
                {
                    randNum.Add(num);
                    transform.GetChild(num).GetComponent<MonsterStrategy>().ActiveMove();
                }
            }
            activeMonster = true;
        }
        if (timer > 5)
        {
            timer = 0;
            activeMonster = false;
        }
    }
}
