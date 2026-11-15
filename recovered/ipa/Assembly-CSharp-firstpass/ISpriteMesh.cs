using UnityEngine;

public interface ISpriteMesh
{
	SpriteRoot sprite { get; set; }

	Texture texture { get; }

	Material material { get; }

	Vector3[] vertices { get; }

	Vector2[] uvs { get; }

	void Init();

	void UpdateVerts();

	void UpdateUVs();

	void UpdateColors(Color color);

	void Hide(bool tf);

	bool IsHidden();
}
