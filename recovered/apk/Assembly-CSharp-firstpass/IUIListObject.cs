using UnityEngine;

public interface IUIListObject : IUIObject
{
	Vector2 TopLeftEdge { get; }

	Vector2 BottomRightEdge { get; }

	bool Managed { get; }

	Rect3D ClippingRect { get; set; }

	bool Clipped { get; set; }

	int Index { get; set; }

	string Text { get; set; }

	SpriteText TextObj { get; }

	object Data { get; set; }

	bool selected { get; set; }

	bool IsContainer();

	void FindOuterEdges();

	void Hide(bool tf);

	void Unclip();

	void UpdateCollider();

	void SetList(UIScrollList c);

	void Delete();
}
