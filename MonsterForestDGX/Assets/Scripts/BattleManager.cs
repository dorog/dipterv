using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject monsterGO;
    public IEnemy monster;
    public Health monsterHealth;
    public Player player;
    public SceneLoader sceneLoader;
    //public Text turn;
    public GameObject turnGO;
    public int id;

    private GameObject battlePlace;

    public delegate void MonsterTurnEndDelegate();
    public MonsterTurnEndDelegate monsterTurnStartDelegateEvent;

    public bool petEnable = true;

    public void Battle(int _id, GameObject battlePlace)
    {
        this.battlePlace = battlePlace;

        id = _id;
        turnGO.SetActive(true);
        //turn.text = "Battle!";

        player.battleManager = this;

        monster = monsterGO.GetComponent<IEnemy>();
        monster.Appear();
        player.Battle(this, monsterHealth.resistant, petEnable);
    }

    public void BattleStart()
    {
        MonsterTurn();
        player.BattleStarted();
    }

    public void PlayerAttack()
    {
        monster.React();
    }

    public void PlayerTurn()
    {
        player.StartTurn();
        //turn.text = "Player Turn";
    }

    public void MonsterTurn()
    {
        player.DefTurn();

        monster.StartTurn();

        //turn.text = "Monster Turn";

        monsterTurnStartDelegateEvent?.Invoke();
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
        //battlePlace.SetActive(true);
    }

    public void FinishedTraining()
    {
        turnGO.SetActive(false);
        battlePlace.SetActive(true);
    }
}
