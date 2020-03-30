
public class AliveMonstersManager : SingletonClass<AliveMonstersManager>
{
    public BattlePlace[] battlePlaces;

    private void Awake()
    {
        DataManager dataManager = DataManager.GetInstance();
        AliveMonsters aliveMonsters = dataManager.gameData.AliveMonsters;

        for (int i = 0; i < aliveMonsters.alive.Length; i++)
        {
            battlePlaces[i].id = i;
            battlePlaces[i].gameObject.SetActive(aliveMonsters.alive[i]);
        }

        Init(this);
    }

    public void Won(int id)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SaveMonsterDeath(id);
    }
}
