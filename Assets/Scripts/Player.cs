using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Battler
{
    public Enemy enemy;
    public AttackHandler attackHandler;
    public Button attackButton;

    // Use this for initialization
    public override void Start()
    {
        string playerSaveData = PlayerPrefs.GetString("player_data");
        SaveData saveData = JsonUtility.FromJson<SaveData>(playerSaveData);
        bugStats.SetStats(saveData.name, saveData.level, saveData.health);
        bugRef = new Bug
        {
            attack = saveData.attack,
            defense = saveData.defense
        };
    }

    public void Attack()
    {
        StartCoroutine(PlayAttackAnimation());
    }

    IEnumerator PlayAttackAnimation()
    {
        GameObject target = enemy.attackHandler.gameObject;
        attackHandler.Attack(target, AttackHandler.BackupAngle.Player);
        attackButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        attackHandler.ResetAttacker();

        if (enemy.IsDead())
        {
            PlayerPrefs.SetString("battle_result", "win");
            SceneManager.LoadScene("PostBattle");
        } else
        {
            yield return StartCoroutine(enemy.Attack());
        }
    }
}
