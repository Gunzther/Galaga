using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggerCheck : MonoBehaviour
{
    public bool isCheckPoint;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(8, 10);
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(9, 12);
        Physics2D.IgnoreLayerCollision(10, 11);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCheckPoint)
        {
            collision.GetComponent<MonsterStrategy>().BackToStartPoint();
        }
        else
        {
            collision.GetComponent<MonsterStrategy>().AttackPlayer();
        }
    }
}
