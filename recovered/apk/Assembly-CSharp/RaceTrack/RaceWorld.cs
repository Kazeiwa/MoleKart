using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GameNetwork;
using Misc;
using RaceProtocol;
using TaomeeNetwork;
using UnityEngine;

namespace RaceTrack;

public sealed class RaceWorld
{
	private class Nested
	{
		internal static readonly RaceWorld Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
			Instance = new RaceWorld();
		}
	}

	private List<Wall> walls = new List<Wall>();

	private List<IAutoCar> vehicles = new List<IAutoCar>();

	private Dictionary<string, IMonster> monsters = new Dictionary<string, IMonster>();

	private RacingLine racingLine = new RacingLine();

	private WorldPartition<IAutoCar> partitionOfVehicle;

	public float[] PropTimeLengthArray = new float[3];

	public float[] TimingTimeLengthArray = new float[3];

	public float[] PropDistanceBiasArray = new float[3];

	public float[] TimingDistanceBiasArray = new float[3];

	public static RaceWorld Instance => Nested.Instance;

	public RacingLine RacingLine => racingLine;

	public WorldPartition<IAutoCar> PartitionOfVehicle => partitionOfVehicle;

	public IEnumerable<IMonster> Monsters => monsters.Values;

	public IList<IAutoCar> Vehicles => vehicles;

	public IEnumerable<NPCCar> NpcCars
	{
		get
		{
			foreach (Car car in Vehicles)
			{
				NPCCar npcCar = car as NPCCar;
				if (null != npcCar)
				{
					yield return npcCar;
				}
			}
		}
	}

	public IList<Wall> Walls => walls;

	public IList<IObstacle> Obstacles { get; set; }

	public IDictionary<string, IProp> Props { get; set; }

	public NameIndexer NameIndexer { get; set; }

	private RaceWorld()
	{
		Props = new Dictionary<string, IProp>();
		NameIndexer = new NameIndexer();
	}

	public static RaceWorld CreateWorld()
	{
		return new RaceWorld();
	}

	public void AddWall(Wall wall)
	{
		Walls.Add(wall);
	}

	public void AddVehicle(IAutoCar vehicle)
	{
		Vehicles.Add(vehicle);
	}

	public void AddProp(string name, IProp prop)
	{
		if (!Props.ContainsKey(name))
		{
			Props.Add(name, prop);
		}
	}

	public void RemoveProp(string name)
	{
		Props.Remove(name);
	}

	public IProp Prop(string name)
	{
		return Props[name];
	}

	public bool HasProp(string name)
	{
		return Props.ContainsKey(name);
	}

	public void AddWallChain(Chain chain)
	{
		if (chain.Vectors.Count >= 2)
		{
			int index = 0;
			for (int i = 1; i < chain.Vectors.Count; i++)
			{
				AddWall(new Wall(chain.Vectors[index], chain.Vectors[i]));
				index = i;
			}
		}
	}

	public IMonster Monster(string name)
	{
		if (!monsters.ContainsKey(name))
		{
			ATrace.Say("No monster:" + name);
			Debug.Log("No monster:" + name);
			return null;
		}
		return monsters[name];
	}

	public IMonster Monster(int index)
	{
		string text = NameIndexer.Index2Name(index);
		if (text != null)
		{
			return Monster(text);
		}
		ATrace.Say("No monster with index: " + index);
		Debug.Log("No monster with index: " + index);
		return null;
	}

	public void AttachMonsters()
	{
		string[] array = new string[3] { "monster", "CullObjects", "Scene/npc" };
		string[] array2 = array;
		foreach (string name in array2)
		{
			BaseMonsterBehaviour[] componentsInChildren = GameObjectUtil.GetComponentsInChildren<BaseMonsterBehaviour>(name);
			if (componentsInChildren == null || componentsInChildren.Length <= 0)
			{
				continue;
			}
			BaseMonsterBehaviour[] array3 = componentsInChildren;
			foreach (BaseMonsterBehaviour baseMonsterBehaviour in array3)
			{
				if (monsters.ContainsKey(baseMonsterBehaviour.Name))
				{
					Debug.LogWarning("Duplicated monster name: [" + baseMonsterBehaviour.Name + "]");
				}
				monsters.Add(baseMonsterBehaviour.Name, baseMonsterBehaviour);
			}
		}
		SetupIndicesInSceneForMonsters();
	}

	public void ClearMonsters()
	{
		monsters.Clear();
	}

	public void SetPartition(Bounds2D boundary, int cellCountX, int cellCountY)
	{
		partitionOfVehicle = new WorldPartition<IAutoCar>();
		partitionOfVehicle.CreateCells(boundary, cellCountX, cellCountY);
	}

	public void UpdateMonsters()
	{
		if (AppMain.Instance.Pause)
		{
			return;
		}
		foreach (IMonster monster in Monsters)
		{
			monster.UpdateState();
		}
		if (NetworkManager.Instance.IsServer && !SystemConfig.Instance.DisableMonsterMovementMessageSending)
		{
			ServerInfoCache.Instance.Update(Time.deltaTime);
		}
	}

	public IAutoCar GetVehicleInRange(Vector2 position, float sqrDistance)
	{
		foreach (IAutoCar vehicle in Vehicles)
		{
			if ((vehicle.Pos - position).sqrMagnitude < sqrDistance)
			{
				return vehicle;
			}
		}
		return null;
	}

	public IAutoCar GetVehicleNotDeadInRange(Vector2 position, float sqrDistance)
	{
		foreach (IAutoCar vehicle in Vehicles)
		{
			if (!vehicle.IsDead && (vehicle.Pos - position).sqrMagnitude < sqrDistance)
			{
				return vehicle;
			}
		}
		return null;
	}

	public IEnumerable<IAutoCar> VehiclesWeakNear(Vector2 position, float weakDistance)
	{
		foreach (IAutoCar item in Vehicles)
		{
			if (MathfEx.WeakDistance(item.Pos, position) <= weakDistance)
			{
				yield return item;
			}
		}
	}

	public IEnumerable<IMonster> MonstersWeakNear(Vector2 position, float weakDistance)
	{
		foreach (IMonster item in Monsters)
		{
			if (MathfEx.WeakDistance(item.Pos, position) <= weakDistance)
			{
				yield return item;
			}
		}
	}

	public IEnumerable<IProp> PropsWeakNear(Vector2 position, float weakDistance)
	{
		foreach (IProp item in Props.Values)
		{
			if (item != null)
			{
				if (MathfEx.WeakDistance(item.Pos, position) <= weakDistance)
				{
					yield return item;
				}
			}
			else
			{
				Debug.Log("PropsWeakNear---> prop has been destroyed!");
			}
		}
	}

	public void Init()
	{
		Debug.Log("RaceWorld.Init()");
		StartMonsters();
	}

	public void Uninit()
	{
		Debug.Log("1 --- RaceWorld.Uninit()");
		Vehicles.Clear();
		Props.Clear();
		Walls.Clear();
		UninitMonsters();
		racingLine.Clear();
		partitionOfVehicle = null;
		NameIndexer.Reset();
		Debug.Log("2 --- RaceWorld.Uninit()");
	}

	public void SetupClient()
	{
		RegisterNetHandlerForClient();
	}

	public void TeardownClient()
	{
		UnregisterNetHandlerForClient();
	}

	private void UninitMonsters()
	{
		foreach (IMonster monster in Monsters)
		{
			monster.Uninit();
		}
		ClearMonsters();
	}

	private void StartMonsters()
	{
		foreach (IMonster monster in Monsters)
		{
			monster.StartState();
		}
	}

	private void RegisterNetHandlerForClient()
	{
		if (!SystemConfig.Instance.DisableMonsterMovementMessageReceiving)
		{
			NetHandlerRegistry.Instance.RegistHandler<MsgMonsterMovement>(OnReceiveMonsterMovement);
		}
		NetHandlerRegistry.Instance.RegistHandler<MsgMonsterStateInfo>(OnReceiveMonsterStateInfo);
		NetHandlerRegistry.Instance.RegistHandler<MsgMonsterCommand>(OnReceiveMonsterCommand);
	}

	private void UnregisterNetHandlerForClient()
	{
		if (!SystemConfig.Instance.DisableMonsterMovementMessageReceiving)
		{
			NetHandlerRegistry.Instance.UnregistHandler<MsgMonsterMovement>(OnReceiveMonsterMovement);
		}
		NetHandlerRegistry.Instance.UnregistHandler<MsgMonsterStateInfo>(OnReceiveMonsterStateInfo);
		NetHandlerRegistry.Instance.UnregistHandler<MsgMonsterCommand>(OnReceiveMonsterCommand);
	}

	private void OnReceiveMonsterMovementList(string peerId, MsgMonsterMovementList list)
	{
		foreach (MsgMonsterMovement list2 in list.ListList)
		{
			OnReceiveMonsterMovement(peerId, list2);
		}
	}

	private void OnReceiveMonsterMovement(string peerId, MsgMonsterMovement message)
	{
		IMonster monster = Monster(message.Index);
		if (monster != null)
		{
			if (message.HasH)
			{
				monster.ClientOnReceiveRemoteMovementInfo(new Vector3(message.X, message.H, message.Y), new Vector2(message.Vx, message.Vy), message.RotAngle);
			}
			else
			{
				monster.ClientOnReceiveRemoteMovementInfo(new Vector2(message.X, message.Y), new Vector2(message.Vx, message.Vy), message.RotAngle);
			}
		}
	}

	private void OnReceiveMonsterStateInfo(string peerId, MsgMonsterStateInfo message)
	{
		Monster(message.Index)?.ClientOnReceiveStateInfo(message.StateId);
	}

	private void OnReceiveMonsterCommand(string peerId, MsgMonsterCommand message)
	{
		Monster(message.Index)?.ClientOnReceiveCommand(message.CommandId);
	}

	private void SetupIndicesInSceneForMonsters()
	{
		NameIndexer.SortFrom(monsters.Keys);
		for (int i = 0; i < NameIndexer.Names.Count; i++)
		{
			string name = NameIndexer.Index2Name(i);
			IMonster monster = Monster(name);
			if (monster != null)
			{
				monster.IndexInScene = i;
			}
		}
	}
}
