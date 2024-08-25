using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
	// UI 타입(UI 컴포넌트) 별 바인딩된 UI 객체들
	protected Dictionary<Type, UnityEngine.Object[]> m_objects = new Dictionary<Type, UnityEngine.Object[]>();
	public abstract void Init();

    // ex) Bind<Button>(typeof(Buttons));
	// Buttons라는 이름의 enum 멤버들을 Button 객체로 바인딩
    protected void Bind<T>(Type type) where T : UnityEngine.Object
	{
		string[] names = Enum.GetNames(type);	// enum 이름 추출
												// type이 enum이라는 전제하에 호출
		UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
		m_objects.Add(typeof(T), objects);
		 
		for (int i = 0; i < names.Length; i++)
		{
			if (typeof(T) == typeof(GameObject))
				objects[i] = Util.FindChild(gameObject, names[i], true);
			else
				objects[i] = Util.FindChild<T>(gameObject, names[i], true);

			if (objects[i] == null)
				Debug.Log($"Failed to bind({names[i]})");
		}
	}

	protected T Get<T>(int idx) where T : UnityEngine.Object
	{
		UnityEngine.Object[] objects = null;
		// T 타입의 UI 컴포넌트를 갖는 UI 오브젝트 리스트를 배열을 찾고, enum 멤버의 인덱스를 활용하여 객체를 반환
		if (m_objects.TryGetValue(typeof(T), out objects) == false)
		{
            return null;
        }
		return objects[idx] as T;
	}

	protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
	protected Text GetText(int idx) { return Get<Text>(idx); }
	protected Button GetButton(int idx) { return Get<Button>(idx); }
	protected Image GetImage(int idx) { return Get<Image>(idx); }

	public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
	{
        // UI 오브젝트에 UI_EventHandler 컴포넌트를 부착한다. 
        // UI_EventHandler 컴포넌트는 EventSystem으로부터 발생한 이벤트를 처리할 수 있도록 제공된 인터페이스 함수를 구현하였다.
        // 이벤트 발생 시 UI_EventHandler 이벤트 핸들러는 delegate에 등록된 함수를 호출한다.
        // 따라서 BindEvent란 UI 객체에 UI_EventHandler 컴포넌트를 부착하고, 이벤트 시 호출될 함수를 바인딩하는 것이다. 
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

		switch (type)
		{
			case Define.UIEvent.Click:
				evt.OnClickHandler -= action;
				evt.OnClickHandler += action;
				break;
			case Define.UIEvent.Drag:
				evt.OnDragHandler -= action;
				evt.OnDragHandler += action;
				break;
		}
	}
}
