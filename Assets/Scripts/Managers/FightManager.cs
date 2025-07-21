using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    // Function to have the player attack the enemy
    public void Attack()
    {
        EnemyController.main.TakeDamage(PlayerController.main.GetAttack());
        EnemyController.main.UseQueuedAction();
    }

    // Function to have the player defend against the enemy's attack
    public void Defend()
    {
        PlayerController.main.Defend();
        EnemyController.main.UseQueuedAction();
    }
}
