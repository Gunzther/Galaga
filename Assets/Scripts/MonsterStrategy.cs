using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface MonsterStrategy
{
    void Scoring();

    void ActiveMove();

    void AttackPlayer();

    void BackToStartPoint();
}
