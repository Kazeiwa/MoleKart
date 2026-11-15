using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
[ExecuteInEditMode]
public class SpriteManager : MonoBehaviour
{
	public SpriteRoot.WINDING_ORDER winding;

	public int allocBlockSize;

	public bool autoUpdateBounds;

	public bool drawBoundingBox;

	public bool persistent;

	protected bool initialized;

	protected EZLinkedList<SpriteMesh_Managed> availableBlocks;

	protected bool vertsChanged;

	protected bool uvsChanged;

	protected bool colorsChanged;

	protected bool vertCountChanged;

	protected bool updateBounds;

	protected SpriteMesh_Managed[] sprites;

	protected EZLinkedList<SpriteMesh_Managed> activeBlocks;

	protected List<SpriteMesh_Managed> spriteDrawOrder;

	protected SpriteDrawLayerComparer drawOrderComparer;

	protected float boundUpdateInterval;

	protected List<SpriteRoot> spriteAddQueue;

	protected SkinnedMeshRenderer meshRenderer;

	protected Mesh mesh;

	protected Texture texture;

	protected Transform[] bones;

	protected BoneWeight[] boneWeights;

	protected Matrix4x4[] bindPoses;

	protected Vector3[] vertices;

	protected int[] triIndices;

	protected Vector2[] UVs;

	protected Color[] colors;

	protected SpriteMesh_Managed tempSprite;

	public bool IsInitialized
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public Vector2 PixelSpaceToUVSpace(Vector2 xy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelSpaceToUVSpace(int x, int y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelCoordToUVCoord(Vector2 xy)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public Vector2 PixelCoordToUVCoord(int x, int y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected void SetupBoneWeights(SpriteMesh_Managed s)
	{
	}

	private void Awake()
	{
	}

	protected void InitArrays()
	{
	}

	protected int EnlargeArrays(int count)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool AlreadyAdded(SpriteRoot sprite)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteMesh_Managed AddSprite(GameObject go)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteMesh_Managed AddSprite(SpriteRoot sprite)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteRoot CreateSprite(GameObject prefab)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteRoot CreateSprite(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveSprite(SpriteRoot sprite)
	{
	}

	public void RemoveSprite(SpriteMesh_Managed sprite)
	{
	}

	public void MoveToFront(SpriteMesh_Managed s)
	{
	}

	public void MoveToBack(SpriteMesh_Managed s)
	{
	}

	public void MoveInfrontOf(SpriteMesh_Managed toMove, SpriteMesh_Managed reference)
	{
	}

	public void MoveBehind(SpriteMesh_Managed toMove, SpriteMesh_Managed reference)
	{
	}

	public void SortDrawingOrder()
	{
	}

	public SpriteMesh_Managed GetSprite(int i)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void UpdatePositions()
	{
	}

	public void UpdateUVs()
	{
	}

	public void UpdateColors()
	{
	}

	public void UpdateBounds()
	{
	}

	public void ScheduleBoundsUpdate(float seconds)
	{
	}

	public void CancelBoundsUpdate()
	{
	}

	public virtual void LateUpdate()
	{
	}

	public virtual void DoMirror()
	{
	}

	public virtual void OnDrawGizmos()
	{
	}

	public void OnDrawGizmosSelected()
	{
	}

	protected void DrawCenter()
	{
	}
}
