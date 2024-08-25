using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_BtnsCanvas : UI_Base
{
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum GameObjects
    {
        TestObject,
    }
    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));
		Bind<GameObject>(typeof(GameObjects));
		Bind<Image>(typeof(Images));

		GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

		GameObject go = GetImage((int)Images.ItemIcon).gameObject;
		BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
	}

    public void OnButtonClicked(PointerEventData data)
    {
        // ... 
    }

}
