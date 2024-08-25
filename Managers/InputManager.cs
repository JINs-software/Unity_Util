using System;
using UnityEngine;
using UnityEngine.EventSystems;

// 별개의 게임 오브젝트 컴포넌트의 Update 문에서 입력을 처리하는 것은 코드 관리 차원에서 좋지 않다.
// 클라이언트의 입력을 통합적으로 관리할 InputManager 클래스이다. 
public class InputManager
{
    // Delegate를 활용 (Listener Pattern)
    // Action<매개변수 T>: 반환 값이 없는 매서드에 대한 대리자
    public Action KeyAction = null;                         // 키보드 입력에 대한 대리자
    public Action<Define.MouseEvent> MouseAction = null;    // 마우스 입력에 대한 대리자
    // 다른 컴포넌트에서 이벤트 발생 시 호출될 함수를 정의하고 InputManager에 등록한다.
    // (ex, in Start())
    // Managers.Input.KeyAction -= OnKeyboard;  // (이벤트 처리 함수 중복 등록을 방지)
    // Managers.Input.KeyAction += OnKeyboard;

    bool bPressed = false;

    public void OnUpdate()  // called by Managers.Update
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)  // 키보드 입력이 이다면 KeyAction 대리자에 등록된 함수 호출
            KeyAction.Invoke();

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                bPressed = true;
            }
            else
            {
                if (bPressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                bPressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
