using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Monster monster;
    public Player player;
    public SceneLoader sceneLoader;
    public Text turn;
    public GameObject turnGO;

    public void Battle()
    {
        turnGO.SetActive(true);
        turn.text = "Battle!";

        player.battleManager = this;

        monster.Appear();
        player.Battle();
        MonsterTurn();
    }

    public void PlayerAttack()
    {
        monster.React();
    }

    public void PlayerTurn()
    {
        player.StartTurn();
        turn.text = "Player Turn";
    }

    public void MonsterTurn()
    {
        monster.StartTurn();
        player.DefTurn();
        turn.text = "Monster Turn";
    }

    public void MonsterDied()
    {
        turnGO.SetActive(false);
        player.BattleEnd();
    }

    public void PlayerDied()
    {
        sceneLoader.LoadMainMenu();
    }
}
