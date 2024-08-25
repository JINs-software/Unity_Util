using System;
using UnityEngine;
using UnityEngine.EventSystems;

// EventSystem으로부터 발생한 이벤트를 처리하려면 정의된 인터페이스를 구현해야 한다. 
// 클릭 이벤트를 처리하기 위해 IPointerClickHandler 인터페이스 함수를 정의해야 한다.
public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    // IPointerClickHandler 이벤트 핸들러 인터페이스 함수를 정의한다.
    // OnClickHandler 델리게이트를 통해 이벤트 발생 시 호출될 함수를 관리한다. 
    public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClickHandler != null)
			OnClickHandler.Invoke(eventData);
	}

	public void OnDrag(PointerEventData eventData)
    {
		if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
	}
}
