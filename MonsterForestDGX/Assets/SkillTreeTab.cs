using UnityEngine;

public class SkillTreeTab : MonoBehaviour
{
    public GameObject actualTab;

    public void Show(GameObject selectedTab)
    {
        if(actualTab == null)
        {
            actualTab = selectedTab;
            selectedTab.SetActive(true);
        }
        else if(selectedTab == actualTab)
        {
            return;
        }
        else
        {
            actualTab.SetActive(false);
            selectedTab.SetActive(true);
            actualTab = selectedTab;
        }
    }
}
