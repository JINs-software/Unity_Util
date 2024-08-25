using UnityEngine;

/*
 * [Resources.Load<T>(path)]
 * Resources.Load<T>(path): Assets/Resources/path 경로의 프리팩 오브젝트를 로드
 * ex) GameObject prefab = Resources.Load<GameObject>("Prefabs/testPrefab");
 * 
 * [Object.Instantiate(..)]
 * Object.Instantiate(프리팹 객체): 프리팹 객체를 씬에 생성
 * Object.Instantiate(프리팹 객체, parent transform): 프리팹 객체를 씬의 parent 하위에 생성
 * 
 * [Destroy(gameObject)]
 * Destroy(게임 오브젝트): 게임 오브젝트 파괴
 * 
 */

public class ResourceManager
{
    // Resources.Load<T>(path) 맵핑
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);
            }
        }

        return Resources.Load<T>(path);
    }

    // Instantiate 맵핑 (+ 오브젝트 풀 반환)
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");  // 문자열 보간($"..") + 중괄호 표현식({..}) 활용
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    // Destroy 맵핑 (+ 오브젝트 풀 반납)
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        Object.Destroy(go);
    }
}
