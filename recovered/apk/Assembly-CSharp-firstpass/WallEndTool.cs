using UnityEngine;

public class WallEndTool : MonoBehaviour
{
	public int Id = -1;

	public GameObject To1;

	public void OnDrawGizmos()
	{
		if (GlobalOptions.Instance.ShouldDrawWallLine)
		{
			Color color = Gizmos.color;
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(base.transform.position, 1.2f);
			Gizmos.color = color;
			if (To1 != null)
			{
				DrawWall(To1.transform.position, Color.red, Color.green);
			}
		}
	}

	protected void DrawWall(Vector3 to, Color colorSegment, Color colorNormal)
	{
		Color color = Gizmos.color;
		Gizmos.color = colorSegment;
		Gizmos.DrawLine(base.transform.position, to);
		Vector3 vector = base.transform.position * 0.5f + to * 0.5f;
		Vector3 vector2 = CalculateWallNormal(to);
		Gizmos.color = colorNormal;
		Gizmos.DrawLine(vector, vector + vector2);
		Gizmos.color = color;
	}

	private Vector3 CalculateWallNormal(Vector3 to)
	{
		Vector3 vector = to - base.transform.position;
		Vector2 vector2 = new Vector2(vector.x, vector.z);
		vector2.Normalize();
		return new Vector3(0f - vector2.y, 0f, vector2.x);
	}
}
