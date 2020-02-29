using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public Monster monster;
    public Player player;
    public SceneLoader sceneLoader;

    public void Start()
    {
        monster.StartTurn();
    }
    
    public void PlayerAttack()
    {
        monster.React();
    }

    public void PlayerTurn()
    {
        player.StartTurn();
    }

    public void MonsterTurn()
    {
        monster.StartTurn();
        player.DefTurn();
    }

    public void MonsterDied()
    {
        sceneLoader.LoadMainMenu();
    }

    public void PlayerDied()
    {
        sceneLoader.LoadMainMenu();
    }
}
