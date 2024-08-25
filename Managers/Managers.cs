using UnityEngine;

public class Managers : MonoBehaviour
{
    // Managers는 프로젝트의 다른 cs에서 쉽게 참조하면서 동시에 하나의 객체만이 유지되도록 한다.
    static Managers s_instance; // 유일성이 보장
    static Managers Instance { get { Init(); return s_instance; } } // 프로퍼티 활용, 유일한 매니저를 생성하고, 반환하도록 한다.

    // 싱글톤 Managers 객체가 관리하는 여러 매니저 클래스들. 
    // 이 매니저들은 MonoBehaviour를 상속받은 클래스의 객체들이 아니다. 
    InputManager m_input = new InputManager();
    ResourceManager m_resource = new ResourceManager();
    SceneManagerEx m_scene = new SceneManagerEx();
    
    public static InputManager Input { get { return Instance.m_input; } }
    public static ResourceManager Resource { get { return Instance.m_resource; } }
    public static SceneManagerEx Scene { get { return Instance.m_scene; } }

    void Start()
    {
        // 씬 상에 빈 게임 오브젝트에 Managers 컴포넌트를 부착하면 Start 함수 호출을 통해 Init() 수행하도록 한다.
        // 만약, 별도의 빈 게임 오브젝트를 생성하여 활용하지 않는다면, Init()을 외부에서 호출하도록 하여(Instance 프로퍼티에 의해 호출),
        // 싱글턴 객체가 생성되도록 한다. 
        Init();
	}

    void Update()
    {
        // 관리되는 매니저 클래스들은 일반 C# 클래스의 인스턴스들이다.
        // MonoBehaviour의 Update 함수(called by framework)와 같은 Update 함수는 Managers의 Update문을 통해 간접적으로 호출하도록 한다.
        m_input.OnUpdate();
    }

    // 유니티 버전의 싱글톤 객체 생성 및 관리 ("@Managers")
    static void Init()
    {
        if (s_instance == null)
        {
			GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            // 씬의 이동이 있더라도 'DontDestroyOnLoad'를 통해 게임 오브젝트가 파괴되지 않도록 할 수 있다.
            // 즉, Managers 싱글턴 객체는 모든 씬에서 유지될 수 있다. 
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }		
	}

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
    }
}
