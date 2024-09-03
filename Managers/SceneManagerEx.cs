using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public Action LoadSceneHandler;

    public BaseScene CurrentScene {
        // BaseScene 컴포넌트를 갖는 객체를 반환한다.
        // (BaseScene 파생 컴포넌트도 포함)
        get { return GameObject.FindObjectOfType<BaseScene>(); } 
    }

    // Defines.cs에서 enum으로 관리되는 씬을 통해 새로운 씬 로드
	public void LoadScene(Define.Scene type)
    {
        //Manager_Sample.Clear();
        // => 씬 로드 이벤트 핸들러에 처리 함수를 외부에서 부착하고, 이를 호출하는 방식으로 변경
        if(LoadSceneHandler != null)
        {
            LoadSceneHandler.Invoke();
        }
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.Scene type)
    {
        // 리플랙션을 통해 씬 이름 추출
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
