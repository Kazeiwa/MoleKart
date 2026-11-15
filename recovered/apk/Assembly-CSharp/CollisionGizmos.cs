using UnityEngine;

internal class CollisionGizmos : MonoBehaviour
{
	private void OnGUI()
	{
		Car car = TSingleton<CarManager>.Instance.GetCar(0);
		Car car2 = TSingleton<CarManager>.Instance.GetCar(7);
		GUILayout.BeginArea(new Rect(10f, 20f, 100f, 200f));
		if (GUILayout.Button("15 vs 5", GUILayout.Width(80f)))
		{
			Vector3 vecGlobal = car.vehicle.GetForwardGlobal() * 15f;
			Vector3 vecGlobal2 = car2.vehicle.GetForwardGlobal() * 5f;
			car.SetVecGlobal(vecGlobal);
			car2.SetVecGlobal(vecGlobal2);
		}
		if (GUILayout.Button("15 vs 15", GUILayout.Width(80f)))
		{
			Vector3 vecGlobal3 = car.vehicle.GetForwardGlobal() * 15f;
			Vector3 vecGlobal4 = car2.vehicle.GetForwardGlobal() * 15f;
			car.SetVecGlobal(vecGlobal3);
			car2.SetVecGlobal(vecGlobal4);
		}
		if (GUILayout.Button("30 vs 4", GUILayout.Width(80f)))
		{
			Vector3 vecGlobal5 = car.vehicle.GetForwardGlobal() * 30f;
			Vector3 vecGlobal6 = car2.vehicle.GetForwardGlobal() * 4f;
			car.SetVecGlobal(vecGlobal5);
			car2.SetVecGlobal(vecGlobal6);
		}
		if (GUILayout.Button("30 vs 30", GUILayout.Width(80f)))
		{
			Vector3 vecGlobal7 = car.vehicle.GetForwardGlobal() * 30f;
			Vector3 vecGlobal8 = car2.vehicle.GetForwardGlobal() * 30f;
			car.SetVecGlobal(vecGlobal7);
			car2.SetVecGlobal(vecGlobal8);
		}
		GUILayout.EndArea();
		GUILayout.BeginArea(new Rect(200f, 50f, 300f, 300f));
		if (GUILayout.Button("Reset 1", GUILayout.Width(80f)))
		{
			car.SetPosition(new Vector3(55f, 16f, 2.4f));
			car.SetRotation(Quaternion.Euler(0f, 180f, 0f));
			car2.SetPosition(new Vector3(55f, 16f, 4.4f));
			car2.SetRotation(Quaternion.Euler(0f, 180f, 0f));
		}
		if (GUILayout.Button("Reset 2", GUILayout.Width(80f)))
		{
			car.SetPosition(new Vector3(50f, 16f, 2.4f));
			car.MoveToGround();
			car.SetRotation(Quaternion.Euler(0f, 150f, 0f));
			car2.SetPosition(new Vector3(53f, 16f, 2.4f));
			car2.MoveToGround();
			car2.SetRotation(Quaternion.Euler(0f, 210f, 0f));
			car.SetVecGlobal(Vector3.zero);
			car2.SetVecGlobal(Vector3.zero);
		}
		if (GUILayout.Button("Reset 3", GUILayout.Width(80f)))
		{
			Vector3 position = new Vector3(55f, 11f, -107.4f);
			car.SetPosition(position);
			car.MoveToGround();
			car.SetRotation(Quaternion.Euler(0f, 150f, 0f));
			Vector3 position2 = new Vector3(56f, 11f, -108.4f);
			car2.SetPosition(position2);
			car2.MoveToGround();
			car2.SetRotation(Quaternion.Euler(0f, 210f, 0f));
			car.SetVecGlobal(Vector3.zero);
			car2.SetVecGlobal(Vector3.zero);
		}
		if (GUILayout.Button("Reset 4", GUILayout.Width(80f)))
		{
			Vector3 position3 = new Vector3(-53.7f, 1f, 72.6f);
			Vector3 position4 = new Vector3(-51f, 1f, 72.6f);
			car.SetPosition(position3);
			car.MoveToGround();
			car.SetRotation(Quaternion.Euler(0f, 80f, 0f));
			car2.SetPosition(position4);
			car2.MoveToGround();
			car2.SetRotation(Quaternion.Euler(0f, 30f, 0f));
			car.SetVecGlobal(Vector3.zero);
			car2.SetVecGlobal(Vector3.zero);
		}
		GUILayout.EndArea();
	}

	private void OnDrawGizmos()
	{
	}
}
