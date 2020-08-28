using UnityEngine;
using System.Collections;

public class BugSpawner : MonoBehaviour
{
    public EnemyBug[] enemyBugs;

    public struct SpawnedBug
    {
        public int attack;
        public int defense;
        public string name;
        public int health;
        public int level;
        public Sprite image;
    }

    public SpawnedBug SpawnBug()
    {
        // Get the player's save data
        string playerData = PlayerPrefs.GetString("player_data");
        SaveData saveData = JsonUtility.FromJson<SaveData>(playerData);

        // Grab a random bug from the enemyBugs list
        Debug.Log(enemyBugs.Length);
        EnemyBug enemyBug = enemyBugs[Random.Range(0, enemyBugs.Length)];

        return new SpawnedBug
        {
            attack = enemyBug.baseAttack * Random.Range(5, 15) + (3 * saveData.level),
            defense = enemyBug.baseDefense * Random.Range(5, 15) + (3 * saveData.level),
            health = enemyBug.baseHealth * Random.Range(15, 20) + (3 * saveData.level),
            level = saveData.level,
            name = enemyBug.name,
            image = enemyBug.image
        };

    }
}
