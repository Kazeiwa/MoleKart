using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Misc;
using RaceProtocol;
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
		}
	}

	private List<Wall> walls;

	private List<IAutoCar> vehicles;

	private Dictionary<string, IMonster> monsters;

	private PowerUpFinder powerUpFinder;

	private RacingLine racingLine;

	public float[] PropTimeLengthArray;

	public float[] TimingTimeLengthArray;

	public float[] PropDistanceBiasArray;

	public float[] TimingDistanceBiasArray;

	public static RaceWorld Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RacingLine RacingLine
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<IMonster> Monsters
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IList<IAutoCar> Vehicles
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<NPCCar> NpcCars
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IList<Wall> Walls
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IList<IObstacle> Obstacles
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

	public IDictionary<string, IProp> Props
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

	public NameIndexer NameIndexer
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

	private RaceWorld()
	{
	}

	public static RaceWorld CreateWorld()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddWall(Wall wall)
	{
	}

	public void AddVehicle(IAutoCar vehicle)
	{
	}

	public void AddProp(string name, IProp prop)
	{
	}

	public void RemoveProp(string name)
	{
	}

	public IProp Prop(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool HasProp(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AddWallChain(Chain chain)
	{
	}

	public IMonster Monster(string name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IMonster Monster(int index)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void AttachMonsters()
	{
	}

	public void AttachPropBoxes()
	{
	}

	public IList<PowerUpInfo> GetPowerUpInfos(RichEdge edge)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void ClearMonsters()
	{
	}

	public void UpdateMonsters()
	{
	}

	public IAutoCar GetVehicleInRange(Vector2 position, float sqrDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public IAutoCar GetVehicleNotDeadInRange(Vector2 position, float sqrDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<IAutoCar> VehiclesWeakNear(Vector2 position, float weakDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<IMonster> MonstersWeakNear(Vector2 position, float weakDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	public IEnumerable<IProp> PropsWeakNear(Vector2 position, float weakDistance)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Init()
	{
	}

	public void Uninit()
	{
	}

	public void SetupPeer()
	{
	}

	public void TeardownPeer()
	{
	}

	public void SetupClient()
	{
	}

	public void TeardownClient()
	{
	}

	private void UninitMonsters()
	{
	}

	private void StartMonsters()
	{
	}

	private void RegisterNetHandlerForClient()
	{
	}

	private void RegisterNetHandlerForPeer()
	{
	}

	private void UnregisterNetHandlerForClient()
	{
	}

	private void UnregisterNetHandlerForPeer()
	{
	}

	private void OnReceiveMonsterMovement(string peerId, MsgMonsterMovement message)
	{
	}

	private void OnReceiveMonsterMovementCircular1(string peerId, MsgMonsterMovementCircular1 message)
	{
	}

	private void OnReceiveMonsterStateInfo(string peerId, MsgMonsterStateInfo message)
	{
	}

	private void OnReceiveMonsterCommand(string peerId, MsgMonsterCommand message)
	{
	}

	private void SetupIndicesInSceneForMonsters()
	{
	}
}
