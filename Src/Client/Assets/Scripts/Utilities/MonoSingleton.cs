using UnityEngine;


public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool global = true;
    static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance =(T)FindObjectOfType<T>();
            }
            return instance;
        }

    }

    void Awake()
    {
        Debug.LogWarningFormat("{0}[{1}] Awake",typeof(T),this.GetInstanceID());
        if (global)
        {
            //如果单例已经存在且自己不是那个已存在的单例，就销毁自己，以免重复创建单例
            if (instance != null && instance != this.gameObject.GetComponent<T>())
            {
                Destroy(this.gameObject);
                return;
            }
            DontDestroyOnLoad(this.gameObject);
            instance = this.gameObject.GetComponent<T> ();
        }
        this.OnStart();
    }

    protected virtual void OnStart()
    {

    }
}