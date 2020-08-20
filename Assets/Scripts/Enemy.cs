using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : Battler
{
    public Player player;
    public AttackHandler attackHandler;
    public Button attackButton;

    // Use this for initialization
    public override void Start()
    {
        // Pick a random bug from a "bug pool" here
        bugStats.SetStats("Wasp", 3, 100);
        bugRef = new Bug
        {
            attack = 30,
            defense = 30
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
