using UnityEngine;

public interface ISpellPattern
{
    void Guess(Vector2 point);
    float GetResult();
    void Reset();
    GameObject GetSpell();
    ElementType GetElementType();
    string GetSpellType();
    string GetLevel();
    int GetLevelValue();
    Sprite GetIcon();
    float GetTypeValue();
    float GetCooldown();
    string GetDifficulty();
    Color GetDifficultyColor();
    float GetMinCoverage();
}
