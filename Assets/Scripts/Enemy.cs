using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : Battler
{
    public Player player;
    public AttackHandler attackHandler;
    public Button attackButton;

    public BugSpawner bugSprites;

    // Use this for initialization
    public override void Start()
    {
        BugSpawner.SpawnedBug bug = bugSprites.SpawnBug();
        bugStats.SetStats(bug.name, bug.level, bug.health);
        SetImage(bug.image);
        bugRef = new Bug
        {
            attack = bug.attack,
            defense = bug.defense
        };
    }

    public IEnumerator Attack()
    {
        GameObject target = player.attackHandler.gameObject;
        attackHandler.Attack(target, AttackHandler.BackupAngle.Enemy);
        yield return new WaitForSeconds(2f);
        attackButton.gameObject.SetActive(true);
        attackHandler.ResetAttacker();

        if (player.IsDead())
        {
            PlayerPrefs.SetString("battle_result", "loss");
            SceneManager.LoadScene("PostBattle");
        }
      
    }
}
