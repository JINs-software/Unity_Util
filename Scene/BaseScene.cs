using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    // Init 호출, Start -> Awake로 변경
    // 씬에 씬 객체(BaseScene 파생 컴포넌트가 부착된 빈 객체)가 비활성화된 상태에서 씬이 재생되면 Init이 호출되지 않는다.
    // Start는 활성화된 상태에서 씬이 시작되어야 호출된다.
    // Awake는 Start에 앞서 호출된다. Awake는 활성화 상태와 상관없이 호출된다. 
    void Awake()
	{
		Init();
	}

	protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            // 씬에 EventSystem 객체가 없다면 자동 생성
            GameObject eventSystemObject = new GameObject("@EventSystem");
            eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<StandaloneInputModule>();
        }
    }

    public abstract void Clear();
}
