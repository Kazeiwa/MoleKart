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

	public const int MASKALL = -1;

	private List<CCollisionBody> PlayerList = new List<CCollisionBody>();

	private List<CCollisionBody> NpcList = new List<CCollisionBody>();

	private List<CCollisionBody> GroundList = new List<CCollisionBody>();

	private List<CCollisionBody> GardbarList = new List<CCollisionBody>();

	private List<CCollisionBody> TreeList = new List<CCollisionBody>();

	private List<CCollisionBody> DeepWaterList = new List<CCollisionBody>();

	private List<CCollisionBody> ShallowWaterList = new List<CCollisionBody>();

	private List<CCollisionBody> ObstacleList = new List<CCollisionBody>();

	private static CollisionLayer instance;

	public static CollisionLayer Instance
	{
		get
		{
			if (instance == null)
			{
				new CollisionLayer();
			}
			return instance;
		}
	}

	private CollisionLayer()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	public bool Init()
	{
		return true;
	}

	public void Shutdown()
	{
		RemoveAllColliders();
	}

	public void RemoveAllColliders()
	{
		PlayerList.Clear();
		NpcList.Clear();
		GroundList.Clear();
		GardbarList.Clear();
		TreeList.Clear();
		DeepWaterList.Clear();
		ShallowWaterList.Clear();
		ObstacleList.Clear();
	}

	public static string GetName(EnmCollisionLayer clLayer)
	{
		string result = "undefine";
		switch (clLayer)
		{
		case EnmCollisionLayer.CL_PLAYER:
			result = "CL_Player";
			break;
		case EnmCollisionLayer.CL_NPC:
			result = "CL_Npc";
			break;
		case EnmCollisionLayer.CL_GROUND:
			result = "CL_Ground";
			break;
		case EnmCollisionLayer.CL_TREE:
			result = "CL_Tree";
			break;
		}
		return result;
	}

	public List<CCollisionBody> GetCollisionArray(EnmCollisionLayer clLayer)
	{
		return clLayer switch
		{
			EnmCollisionLayer.CL_PLAYER => PlayerList, 
			EnmCollisionLayer.CL_TREE => TreeList, 
			EnmCollisionLayer.CL_NPC => NpcList, 
			EnmCollisionLayer.CL_GROUND => GroundList, 
			EnmCollisionLayer.CL_GARDBAR => GardbarList, 
			_ => null, 
		};
	}

	public bool AddCollider(CCollisionBody collider)
	{
		switch (collider.m_clLayer)
		{
		case EnmCollisionLayer.CL_PLAYER:
			if (PlayerList.Contains(collider))
			{
				return false;
			}
			PlayerList.Add(collider);
			collider.GetGameObject().layer = 10;
			return true;
		case EnmCollisionLayer.CL_NPC:
			if (NpcList.Contains(collider))
			{
				return false;
			}
			NpcList.Add(collider);
			collider.GetGameObject().layer = 12;
			return true;
		case EnmCollisionLayer.CL_GROUND:
			if (GroundList.Contains(collider))
			{
				return false;
			}
			GroundList.Add(collider);
			collider.GetGameObject().layer = 8;
			return true;
		case EnmCollisionLayer.CL_GARDBAR:
			if (GardbarList.Contains(collider))
			{
				return false;
			}
			GardbarList.Add(collider);
			collider.GetGameObject().layer = 9;
			return true;
		case EnmCollisionLayer.CL_TREE:
			if (TreeList.Contains(collider))
			{
				return false;
			}
			TreeList.Add(collider);
			collider.GetGameObject().layer = 14;
			return true;
		case EnmCollisionLayer.CL_DEEPWATER:
			if (DeepWaterList.Contains(collider))
			{
				return false;
			}
			DeepWaterList.Add(collider);
			collider.GetGameObject().layer = 15;
			return true;
		case EnmCollisionLayer.CL_SHALLOWWATER:
			if (ShallowWaterList.Contains(collider))
			{
				return false;
			}
			ShallowWaterList.Add(collider);
			collider.GetGameObject().layer = 16;
			return true;
		case EnmCollisionLayer.CL_OBSTACLE:
			if (ObstacleList.Contains(collider))
			{
				return false;
			}
			ObstacleList.Add(collider);
			collider.GetGameObject().layer = 17;
			return true;
		default:
			return false;
		}
	}

	public void RemoveCollider(CCollisionBody collider)
	{
		switch (collider.m_clLayer)
		{
		case EnmCollisionLayer.CL_PLAYER:
			PlayerList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_NPC:
			NpcList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_GROUND:
			GroundList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_GARDBAR:
			GardbarList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_TREE:
			TreeList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_DEEPWATER:
			DeepWaterList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_SHALLOWWATER:
			ShallowWaterList.Remove(collider);
			break;
		case EnmCollisionLayer.CL_OBSTACLE:
			ObstacleList.Remove(collider);
			break;
		case (EnmCollisionLayer)11:
		case EnmCollisionLayer.CL_BUDDY_SPHERE:
			break;
		}
	}

	public EnmCollisionSide DetectToGuardBar(BoxComponent cComponent, int pos, bool isHero)
	{
		BoxCollider boxCollider = cComponent.GetCollider() as BoxCollider;
		if (boxCollider == null)
		{
			Debug.LogError("DectectToGardbar()--->no collider!");
			return EnmCollisionSide.NOTTOUCH;
		}
		Vector3 vector = ((pos != 0) ? ((cComponent.vBR + cComponent.vBL) * 0.5f) : ((cComponent.vFR + cComponent.vFL) * 0.5f));
		Vector3 vector2 = vector - cComponent.vBoxCenter;
		vector2.y = 0f;
		vector2.Normalize();
		Vector3 vector3 = cComponent.vBoxCenter + vector2 * 3f;
		if (!isHero && !HeightMapManager.Instance.CollideGardbar(cComponent.vBoxCenter.x, cComponent.vBoxCenter.z, vector3.x, vector3.z, 0.5f))
		{
			return EnmCollisionSide.NOTTOUCH;
		}
		vector = cComponent.vBoxCenter;
		Vector3 direction = cComponent.vFR - vector;
		Vector3 direction2 = cComponent.vFL - vector;
		Vector3 direction3 = cComponent.vBR - vector;
		Vector3 direction4 = cComponent.vBL - vector;
		direction.Normalize();
		direction2.Normalize();
		direction3.Normalize();
		direction4.Normalize();
		EnmCollisionSide enmCollisionSide = EnmCollisionSide.NOTTOUCH;
		int layerMask = 512;
		float scale = cComponent.scale;
		scale = 3f;
		if (Physics.Raycast(cComponent.vBoxCenter, direction, out var hitInfo, scale, layerMask))
		{
			cComponent.CollisionNormal = hitInfo.normal;
			cComponent.CollisionPos = hitInfo.point;
			return enmCollisionSide | EnmCollisionSide.TOUCHFRONTRIGHT;
		}
		if (Physics.Raycast(cComponent.vBoxCenter, direction2, out hitInfo, scale, layerMask))
		{
			enmCollisionSide |= EnmCollisionSide.TOUCHFRONTLEFT;
			cComponent.CollisionNormal = hitInfo.normal;
			cComponent.CollisionPos = hitInfo.point;
			return enmCollisionSide;
		}
		if (Physics.Raycast(cComponent.vBoxCenter, direction3, out hitInfo, scale, layerMask))
		{
			enmCollisionSide |= EnmCollisionSide.TOUCHBACKRIGHT;
			cComponent.CollisionNormal = hitInfo.normal;
			cComponent.CollisionPos = hitInfo.point;
			return enmCollisionSide;
		}
		if (Physics.Raycast(cComponent.vBoxCenter, direction4, out hitInfo, scale, layerMask))
		{
			enmCollisionSide |= EnmCollisionSide.TOUCHBACKLEFT;
			cComponent.CollisionPos = hitInfo.point;
			cComponent.CollisionNormal = hitInfo.normal;
			return enmCollisionSide;
		}
		return enmCollisionSide;
	}

	private void CaculateDistancetoGround(BoxComponent cComponent)
	{
		Collider collider = cComponent.GetCollider();
		if (!(collider == null))
		{
			int layerMask = 256;
			Vector3 center = collider.bounds.center;
			Vector3 extents = collider.bounds.extents;
			if (Physics.Raycast(center, -Vector3.up, out var hitInfo, float.MaxValue, layerMask))
			{
				cComponent.DistanceToGround = hitInfo.distance - extents.y;
				cComponent.GroundNormal = hitInfo.normal;
			}
			else
			{
				cComponent.DistanceToGround = float.MinValue;
				cComponent.GroundNormal = Vector3.up;
			}
		}
	}

	public bool VerticalIntersectHitPosition(Vector3 vWorldPos, out Vector3 vGroundNormal, out Vector3 vHitPoint)
	{
		int layerMask = 256;
		vGroundNormal = (vHitPoint = Vector3.zero);
		if (Physics.Raycast(vWorldPos, -Vector3.up, out var hitInfo, float.MaxValue, layerMask))
		{
			vGroundNormal = hitInfo.normal;
			vHitPoint = hitInfo.point;
			return true;
		}
		return false;
	}

	public bool GetTerrainHeight(Vector2 vWorld, out float fHeight)
	{
		fHeight = 0f;
		Vector3 vWorldPos = new Vector3(vWorld.x, 1000f, vWorld.y);
		Vector3 vGroundNormal;
		Vector3 vHitPoint;
		bool result = VerticalIntersectHitPosition(vWorldPos, out vGroundNormal, out vHitPoint);
		fHeight = vHitPoint.y;
		return result;
	}

	public bool RaycastDistancetoGround(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vGroundNormal, out Vector3 vHitPoint)
	{
		int layerMask = 256;
		vGroundNormal = (vHitPoint = Vector3.zero);
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, fDistance, layerMask))
		{
			vGroundNormal = hitInfo.normal;
			vHitPoint = hitInfo.point;
			return true;
		}
		vHitPoint = vWorldPos;
		return false;
	}

	public bool RayCastHitToGuardbar(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		vPos = Vector3.zero;
		vNormal = Vector3.zero;
		int layerMask = 512;
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, fDistance, layerMask))
		{
			vNormal = hitInfo.normal;
			vPos = hitInfo.point;
			return true;
		}
		return false;
	}

	public bool RayCastHitToMushroom(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		vPos = Vector3.zero;
		vNormal = Vector3.zero;
		int layerMask = 4194304;
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, fDistance, layerMask))
		{
			vNormal = hitInfo.normal;
			vPos = hitInfo.point;
			return true;
		}
		return false;
	}

	public bool RayCastHitToTree(Vector3 vWorldPos, Vector3 vDir, float fDistance, out Vector3 vPos, out Vector3 vNormal)
	{
		vPos = Vector3.zero;
		vNormal = Vector3.zero;
		int layerMask = 16384;
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, fDistance, layerMask))
		{
			vNormal = hitInfo.normal;
			vPos = hitInfo.point;
			return true;
		}
		return false;
	}

	public float GetDistanceToGardbar(Vector3 vWorldPos, Vector3 vDir)
	{
		float num = float.PositiveInfinity;
		vDir.Normalize();
		int layerMask = 512;
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, num, layerMask))
		{
			num = (hitInfo.point - vWorldPos).magnitude;
		}
		return num;
	}

	public float GetDistanceToStaticObstacle(Vector3 vWorldPos, Vector3 vDir)
	{
		float num = 30f;
		vDir.Normalize();
		int layerMask = 16896;
		if (Physics.Raycast(vWorldPos, vDir, out var hitInfo, num, layerMask))
		{
			num = (hitInfo.point - vWorldPos).magnitude;
		}
		return num;
	}

	public bool CheckHitDeepWater(Vector3 vWorldPos, Vector3 vDir, out Vector3 vWaterPoint)
	{
		float distance = 100f;
		Ray ray = new Ray
		{
			origin = vWorldPos,
			direction = vDir
		};
		vWaterPoint = new Vector3(0f, -1000f, 0f);
		foreach (CCollisionBody deepWater in DeepWaterList)
		{
			Collider collider = deepWater.GetCollider();
			if (collider.Raycast(ray, out var hitInfo, distance))
			{
				float num = HeightMapManager.Instance.Height(vWorldPos.x, vWorldPos.z);
				vWaterPoint = hitInfo.point;
				if (num < hitInfo.point.y)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool CheckHitShallowWater(Vector3 vWorldPos, Vector3 vDir, out Vector3 vWaterPoint)
	{
		vWaterPoint = new Vector3(0f, -1000f, 0f);
		if (!HeightMapManager.Instance.bValidate)
		{
			return false;
		}
		float distance = 100f;
		Ray ray = new Ray
		{
			origin = vWorldPos,
			direction = vDir
		};
		foreach (CCollisionBody shallowWater in ShallowWaterList)
		{
			Collider collider = shallowWater.GetCollider();
			if (collider.Raycast(ray, out var hitInfo, distance))
			{
				float num = HeightMapManager.Instance.Height(vWorldPos.x, vWorldPos.z);
				vWaterPoint = hitInfo.point;
				if (num < hitInfo.point.y)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsInShallowWater(Vector3 vWorldPos)
	{
		foreach (CCollisionBody shallowWater in ShallowWaterList)
		{
			Collider collider = shallowWater.GetCollider();
			if (collider != null && collider.bounds.Contains(vWorldPos))
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInDeepWater(Vector3 vWorldPos)
	{
		foreach (CCollisionBody deepWater in DeepWaterList)
		{
			Collider collider = deepWater.GetCollider();
			if (collider != null && collider.bounds.Contains(vWorldPos))
			{
				return true;
			}
		}
		return false;
	}
}
