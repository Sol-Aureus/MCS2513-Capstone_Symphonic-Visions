using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    private int turnCounter = 0; // Counter to keep track of turns in the fight

    // Function to have the player attack the enemy
    public void Attack()
    {
        turnCounter++; // Increment the turn counter
        EnemyController.main.TakeDamage(PlayerController.main.GetAttack());
        EnemyController.main.UseQueuedAction();
    }

    // Function to have the player defend against the enemy's attack
    public void Defend()
    {
        turnCounter++; // Increment the turn counter
        PlayerController.main.Defend();
        EnemyController.main.UseQueuedAction();
    }
}
