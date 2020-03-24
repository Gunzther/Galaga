using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface MonsterStrategy
{
    void Scoring();

    void ResetPos();

    void ActiveMove();

    void AttackPlayer();

    void BackToStartPoint();
}
