
using UnityEngine;

public class AliveMonstersManager : SingletonClass<AliveMonstersManager>
{
    public BattlePlace[] battlePlaces;

    private void Awake()
    {
        DataManager dataManager = DataManager.GetInstance();

        for (int i = 0; i < SharedData.GameConfig.aliveMonsters.Length; i++)
        {
            battlePlaces[i].id = i;
            battlePlaces[i].gameObject.SetActive(SharedData.GameConfig.aliveMonsters[i]);
        }

        Init(this);
    }

    public void Won(int id)
    {
        DataManager dataManager = DataManager.GetInstance();
        dataManager.SaveMonsterDeath(id);
    }
}
