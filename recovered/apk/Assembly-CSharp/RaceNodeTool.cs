using System.Collections.Generic;
using Misc;
using UnityEngine;

public class RaceNodeTool : MonoBehaviour
{
	public const float DefaultY = 160f;

	public const float NoSpeedLimit = float.PositiveInfinity;

	public int Index = -1;

	public GameObject nextNode1;

	public GameObject nextNode2;

	public GameObject nextNode3;

	public GameObject nextNode4;

	public List<RaceEdgeTool> InEdges = new List<RaceEdgeTool>();

	public List<RaceEdgeTool> OutEdges = new List<RaceEdgeTool>();

	public float leftWidth = 2f;

	public float rightWidth = 2f;

	public float MaxSpeed = float.PositiveInfinity;

	public bool IsEnd;

	public Vector2 Pos => Vector3Ex.To2D(base.transform.position);

	public int InCount => InEdges.Count;

	public int OutCount => OutEdges.Count;

	public bool IsSlim => InCount == 1 && OutCount == 1;

	public bool HasMaxSpeed => MaxSpeed != float.PositiveInfinity;

	public int NextNodeCount
	{
		get
		{
			int num = 0;
			foreach (GameObject nextNode in NextNodes)
			{
				if (null != nextNode)
				{
					num++;
				}
			}
			return num;
		}
	}

	public IEnumerable<GameObject> Slots
	{
		get
		{
			yield return nextNode1;
			yield return nextNode2;
			yield return nextNode3;
			yield return nextNode4;
		}
	}

	public IEnumerable<GameObject> NextNodes
	{
		get
		{
			if (null != nextNode1)
			{
				yield return nextNode1;
			}
			if (null != nextNode2)
			{
				yield return nextNode2;
			}
			if (null != nextNode3)
			{
				yield return nextNode3;
			}
			if (null != nextNode4)
			{
				yield return nextNode4;
			}
		}
	}

	public IEnumerable<GameObject> ReverseNextNodes
	{
		get
		{
			if (null != nextNode4)
			{
				yield return nextNode4;
			}
			if (null != nextNode3)
			{
				yield return nextNode3;
			}
			if (null != nextNode2)
			{
				yield return nextNode2;
			}
			if (null != nextNode1)
			{
				yield return nextNode1;
			}
		}
	}

	public IEnumerable<RaceNodeTool> NextRaceNodeTools
	{
		get
		{
			foreach (GameObject go in NextNodes)
			{
				RaceNodeTool node = go.GetComponent<RaceNodeTool>();
				if (null != node)
				{
					yield return node;
				}
			}
		}
	}

	public IEnumerable<RaceNodeTool> ReverseNextRaceNodeTools
	{
		get
		{
			foreach (GameObject go in ReverseNextNodes)
			{
				RaceNodeTool node = go.GetComponent<RaceNodeTool>();
				if (null != node)
				{
					yield return node;
				}
			}
		}
	}

	public float DistanceFromGoal { get; set; }

	public void AddInEdge(RaceEdgeTool edge)
	{
		InEdges.Add(edge);
	}

	public void AddOutEdge(RaceEdgeTool edge)
	{
		OutEdges.Add(edge);
	}

	public RaceEdgeTool GetInEdge(int index)
	{
		return InEdges[index];
	}

	public RaceEdgeTool GetOutEdge(int index)
	{
		return OutEdges[index];
	}

	public void SetIndex(int index)
	{
		Index = index;
		base.name = ((index >= 0) ? Index2Name(index) : "---");
	}

	public static string Index2Name(int index)
	{
		return $"{index:000}";
	}

	public static RaceNodeTool CreateWayPoint(Vector2 pos, int index, GameObject parent)
	{
		GameObject gameObject = GameObjectUtil.CreateEmptyUnderGameObject(new Vector3(pos.x, 160f, pos.y), Quaternion.identity, Index2Name(index), parent);
		return gameObject.AddComponent<RaceNodeTool>();
	}

	public void SetNextNode(int nth, GameObject node)
	{
		switch (nth)
		{
		case 0:
			nextNode1 = node;
			break;
		case 1:
			nextNode2 = node;
			break;
		case 2:
			nextNode3 = node;
			break;
		case 3:
			nextNode4 = node;
			break;
		default:
			Debug.LogError("Slot " + nth + " does not exist on RaceNodeTool class.");
			break;
		}
	}

	public void AddNextNode(GameObject node)
	{
		int num = 0;
		foreach (GameObject slot in Slots)
		{
			if (null == slot)
			{
				SetNextNode(num, node);
				break;
			}
			num++;
		}
	}

	public void OnDrawGizmos()
	{
		if (!GlobalOptions.Instance.ShouldDrawRaceLine)
		{
			return;
		}
		Color color = Gizmos.color;
		if (HasMaxSpeed)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(base.transform.position, 1.8f);
		}
		else
		{
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(base.transform.position, 1.5f);
		}
		Gizmos.color = color;
		foreach (GameObject nextNode in NextNodes)
		{
			DrawEdge(nextNode, Color.yellow, Color.cyan);
		}
		string text = Index.ToString();
		if (HasMaxSpeed)
		{
			text = text + " MS: " + MaxSpeed;
		}
		GizmosEx.DrawText(base.transform.position, text);
	}

	protected void DrawEdge(GameObject to, Color color1, Color color2)
	{
		if (to != null)
		{
			DrawRaceLine(to, color1);
		}
	}

	private void DrawRaceLine(GameObject to, Color color)
	{
		Color color2 = Gizmos.color;
		float magnitude = (base.transform.position - to.transform.position).magnitude;
		Gizmos.color = ((magnitude < 15f) ? Color.red : ((!(magnitude > 17f)) ? color : Color.blue));
		GizmosEx.DrawArrow(base.transform.position, to.transform.position);
		Gizmos.color = color2;
	}

	private void DrawRaceBoundary(GameObject gameObjectTo, Color color)
	{
		Color color2 = Gizmos.color;
		Gizmos.color = color;
		Vector3 position = base.transform.position;
		Vector3 position2 = gameObjectTo.transform.position;
		BareEdge bareEdge = BareEdge.Extend(4f, Vector3Ex.To2D(base.transform.position), Vector3Ex.To2D(gameObjectTo.transform.position));
		position = Vector2Ex.To3D(bareEdge.From, position.y);
		position2 = Vector2Ex.To3D(bareEdge.To, position2.y);
		RaceNodeTool component = gameObjectTo.GetComponent<RaceNodeTool>();
		Vector2 normalized = Vector3Ex.To2D(position2 - position).normalized;
		Vector2 vector = Vector2Ex.RotateLeft(normalized);
		Vector2 vector2 = -vector;
		Vector3 vector3 = Vector3Ex.Add(position, leftWidth * vector);
		Vector3 vector4 = Vector3Ex.Add(position2, component.leftWidth * vector);
		Gizmos.DrawLine(vector3, vector4);
		Vector3 vector5 = Vector3Ex.Add(position, rightWidth * vector2);
		Vector3 to = Vector3Ex.Add(position2, component.rightWidth * vector2);
		Gizmos.DrawLine(vector5, to);
		Gizmos.DrawLine(vector3, vector5);
		Gizmos.DrawLine(vector4, to);
		Gizmos.color = color2;
	}
}
