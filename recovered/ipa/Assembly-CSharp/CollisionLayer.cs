using System.Collections.Generic;
using UnityEngine;

public class CollisionLayer
{
	public const int GROUNDMASK = 256;

	public const int GARDBARMASK = 512;

	public const int PLAYERMASK = 1024;

	public const int HEROMASK = 2048;

	public const int NPCMASK = 4096;

	public const int TREEMASH = 16384;

	public const int DEEPWATERMASK = 32768;

	public const int SHALLOWWATERMASK = 65536;

	public const int OBSTACLEMASK = 131072;

	public const int MUSHROOMMASK = 4194304;

	public const int HIDEGARDBARMASK = 536870912;

	public const int MASKALL = -1;

	private List<CCollisionBody> PlayerList;

	private List<CCollisionBody> NpcList;

	private List<CCollisionBody> GroundList;

	private List<CCollisionBody> GardbarList;

	private List<CCollisionBody> TreeList;

	private List<CCollisionBody> DeepWaterList;

	private List<CCollisionBody> ShallowWaterList;

	private List<CCollisionBody> ObstacleList;

	private static CollisionLayer instance;

	public static CollisionLayer Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	private CollisionLayer()
	{
	}

	public bool Init()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Shutdown()
	{
	}

	public void RemoveAllColliders()
	{
	}

	public static string GetName(EnmCollisionLayer clLayer)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public List<CCollisionBody> GetCollisionArray(EnmCollisionLayer clLayer)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool AddCollider(CCollisionBody collider)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void RemoveCollider(CCollisionBody collider)
	{
	}

	public EnmCollisionSide DetectToGuardBar(BoxComponent cComponent, int pos, bool isHero)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void CaculateDistancetoGround(BoxComponent cComponent)
	{
	}

	public bool VerticalIntersectHitPosition(Vector3 vWorldPos, out Vector3 vGroundNormal, out Vector3 vHitPoint)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool GetTerrainHeight(Vector2 vWorld, out float fHeight)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RaycastDistancetoGround(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vGroundNormal, out Vector3 vHitPoint)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RayCastHit(Vector3 vWorldPos, Vector3 vDir, float fDistance, int layer, out Vector3 vPos, out Vector3 vNormal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RayCastHitToGuardbar(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RayCastHitToMushroom(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool RayCastHitToTree(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetDistanceToGardbar(Vector3 vWorldPos, Vector3 vDir)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetDistanceToStaticObstacle(Vector3 vWorldPos, Vector3 vDir)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckHitDeepWater(Vector3 vWorldPos, Vector3 vDir, out Vector3 vWaterPoint)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool CheckHitShallowWater(Vector3 vWorldPos, Vector3 vDir, out Vector3 vWaterPoint)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsInShallowWater(Vector3 vWorldPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool IsInDeepWater(Vector3 vWorldPos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
