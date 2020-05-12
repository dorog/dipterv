using System;

[Serializable]
public class AliveMonsters
{
    public bool[] alive;

    public AliveMonsters(int number)
    {
        alive = new bool[number];
        for(int i = 0; i < alive.Length; i++)
        {
            alive[i] = true;
        }
    }

    public AliveMonsters(bool[] aliveMonsters)
    {
        alive = aliveMonsters;
    }
}
