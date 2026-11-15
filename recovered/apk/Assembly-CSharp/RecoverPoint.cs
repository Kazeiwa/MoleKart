using UnityEngine;

public class RecoverPoint : MonoBehaviour
{
	private void Start()
	{
		RecoverPointManager.Instance.m_RecoverPoints.Clear();
		for (int i = 0; i < base.gameObject.transform.GetChildCount(); i++)
		{
			Vector3 position = base.gameObject.transform.GetChild(i).transform.position;
			Vector2 vector = new Vector2(position.x, position.z);
			RecoverPointManager.Instance.m_RecoverPoints.Add(vector);
		}
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
	}
}
