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
    public GameObject mbullet;

    private float timer;
    private List<int> randNum, childIndex;
    private bool activeMonster;

    public void removeChildIndex(int index)
    {
        childIndex.Remove(index);
        print(childIndex.Count());
    }

    void Start()
    {
        timer = 0;
        randNum = new List<int>();
        activeMonster = false;
        childIndex = Enumerable.Range(1, 40).ToList();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3 && !activeMonster)
        {
            int loops = Random.Range(2, 4);
            for (int i = 0; i < loops;)
            {
                int num = childIndex[Random.Range(0, childIndex.Count() - 1)];
                if (!randNum.Contains(num) && transform.GetChild(num).gameObject.activeSelf)
                {
                    randNum.Add(num);
                    transform.GetChild(num).GetComponent<MonsterStrategy>().ActiveMove();
                    i++;
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
