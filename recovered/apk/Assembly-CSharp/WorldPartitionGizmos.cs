using RaceTrack;
using UnityEngine;

public class WorldPartitionGizmos : MonoBehaviour
{
	protected void OnDrawGizmos()
	{
		if (SystemConfig.Instance.GizmosWorldPartition && RaceWorld.Instance.PartitionOfVehicle != null)
		{
			RaceWorld.Instance.PartitionOfVehicle.OnDrawGizmos();
		}
	}
}
