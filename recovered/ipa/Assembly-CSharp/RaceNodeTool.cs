using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RaceNodeTool : MonoBehaviour
{
	public const float DefaultY = 160f;

	public const float NoSpeedLimit = float.PositiveInfinity;

	public int Index;

	public GameObject nextNode1;

	public GameObject nextNode2;

	public GameObject nextNode3;

	public GameObject nextNode4;

	public List<RaceEdgeTool> InEdges;

	public List<RaceEdgeTool> OutEdges;

	public float leftWidth;

	public float rightWidth;

	public float MaxSpeed;

	public bool IsEnd;

	public Vector2 Pos
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int InCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int OutCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool IsSlim
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public bool HasMaxSpeed
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int NextNodeCount
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<GameObject> Slots
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<GameObject> NextNodes
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<GameObject> ReverseNextNodes
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<RaceNodeTool> NextRaceNodeTools
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<RaceNodeTool> ReverseNextRaceNodeTools
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float DistanceFromGoal
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public void AddInEdge(RaceEdgeTool edge)
	{
	}

	public void AddOutEdge(RaceEdgeTool edge)
	{
	}

	public RaceEdgeTool GetInEdge(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public RaceEdgeTool GetOutEdge(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetIndex(int index)
	{
	}

	public static string Index2Name(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static RaceNodeTool CreateWayPoint(Vector2 pos, int index, GameObject parent)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void SetNextNode(int nth, GameObject node)
	{
	}

	public void AddNextNode(GameObject node)
	{
	}

	public void OnDrawGizmos()
	{
	}

	protected void DrawEdge(GameObject to, Color color1, Color color2)
	{
	}

	private void DrawRaceLine(GameObject to, Color color)
	{
	}

	private void DrawRaceBoundary(GameObject gameObjectTo, Color color)
	{
	}
}
