using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Monster monster;
    public MonsterHealth monsterHealth;
    public Player player;
    public SceneLoader sceneLoader;
    public Text turn;
    public GameObject turnGO;
    public int id;

    private GameObject battlePlace;

    public delegate void MonsterTurnEndDelegate();
    public MonsterTurnEndDelegate monsterTurnStartDelegateEvent;

    public void Battle(int _id, GameObject battlePlace)
    {
        this.battlePlace = battlePlace;

        id = _id;
        turnGO.SetActive(true);
        turn.text = "Battle!";

        player.battleManager = this;

        monster.Appear();
        player.Battle(this, monsterHealth.resistant);
    }

    public void BattleStart()
    {
        player.BattleStarted();
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

        monsterTurnStartDelegateEvent();
    }

    public void MonsterDied()
    {
        turnGO.SetActive(false);
        player.BattleEnd(id);
    }

    public void PlayerDied()
    {
        turnGO.SetActive(false);

        player.Died();

        monster.ResetMonster();
        battlePlace.SetActive(true);

        //sceneLoader.LoadMainMenu();
    }

    public void Run()
    {
        turnGO.SetActive(false);

        monster.Disappear();
        player.Run();
        battlePlace.SetActive(true);
    }
}
