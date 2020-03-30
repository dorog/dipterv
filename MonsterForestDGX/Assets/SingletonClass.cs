using UnityEngine;

public class SingletonClass<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;

    protected void Init(T t)
    {
        if(instance == null)
        {
            instance = t;
        }
        else
        {
            Debug.LogError("There is more than one " +  GetType().Name + "!");
        }
    }

    public static T GetInstance()
    {
        return instance;
    }
}
