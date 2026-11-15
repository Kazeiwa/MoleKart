using Misc;
using UnityEngine;

public class CrossOverEdgeTool : MonoBehaviour
{
	public const float DefaultY = 160f;

	public PointBehaviour From;

	public PointBehaviour To;

	public PointBehaviour X;

	private CrossOverEdge crossOverEdge;

	private LapCounter lapCounter;

	public bool Available => From != null && To != null && X != null;

	public Vector2 FromPos => From.Pos;

	public Vector2 ToPos => To.Pos;

	public Vector2 Middle => (FromPos + ToPos) / 2f;

	public static void AddCrossOverTool(Transform parent, Vector2 from, Vector2 to)
	{
		CrossOverEdgeTool crossOverEdgeTool = GameObjectUtil.InsertGameObject<CrossOverEdgeTool>(Vector3.zero, Quaternion.identity, "cross_over", parent);
		Transform parent2 = crossOverEdgeTool.transform;
		crossOverEdgeTool.From = GameObjectUtil.InsertGameObject<PointBehaviour>(Vector2Ex.To3D(from, 160f), Quaternion.identity, "from", parent2);
		crossOverEdgeTool.To = GameObjectUtil.InsertGameObject<PointBehaviour>(Vector2Ex.To3D(to, 160f), Quaternion.identity, "to", parent2);
		crossOverEdgeTool.X = GameObjectUtil.InsertGameObject<PointBehaviour>(Vector2Ex.To3D((from + to) / 2f, 160f), Quaternion.identity, "x", parent2);
	}

	protected void Awake()
	{
		if (Available)
		{
			crossOverEdge = new CrossOverEdge(From.Pos, To.Pos);
			lapCounter = new LapCounter(crossOverEdge);
		}
	}

	protected void Update()
	{
		if (Available)
		{
			crossOverEdge.SetEdge(From.Pos, To.Pos);
			lapCounter.InputPosition(X.Pos);
		}
	}

	protected void OnDrawGizmos()
	{
		if (Available)
		{
			Color color = Gizmos.color;
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(From.Position, To.Position);
			Gizmos.color = color;
			if (lapCounter != null)
			{
				GizmosEx.DrawText(Middle, "laps: " + lapCounter.LapCount);
			}
		}
	}
}
