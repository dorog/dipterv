using UnityEngine;

public interface ISpellPattern
{
    void Guess(Vector2 point);
    float GetResult();
    void Reset();
    GameObject GetSpell();
    ElementType GetElementType();
    int GetLevelValue();
    float GetCooldown();
    float GetMinCoverage();

    //Only UI:
    Sprite GetIcon();
    string GetSpellTypeUI();
    string[] GetLevelUI();
    string[] GetTypeValueUI();
    string[] GetDifficulty();
    Color[] GetDifficultyColor();
    string GetRequiredExp();
    int GetRequiredExpValue();
    string[] GetCooldownUI();
    bool IsMaxed();
    void LevelUp();
}
