using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    private string targetTag = "Enemy";
    internal static int enemiesOnLvl;
    internal static bool levelClear;

    private GameObject[] enemies;

    private void Start()
    {
        EnemyCount();
    }

    public void EnemyCount()
    {
        enemies = GameObject.FindGameObjectsWithTag(targetTag);

        enemiesOnLvl = enemies.Length;

        if (enemiesOnLvl == 0)
        {
            levelClear = true;
        }
    }

    private void Update()
    {
        EnemyCount();
    }
}
