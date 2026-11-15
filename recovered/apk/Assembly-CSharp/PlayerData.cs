using Misc;

public class PlayerData
{
	public uint PlayerId;

	public string peerID;

	public bool IsMaster;

	public int Ranking;

	public float SpendTime;

	public bool NormalFinish;

	public int ShowLaps;

	public ePropType selProp;

	public ePropType altProp;

	public float DriftValue;

	public bool IsFinish;

	public int TotalBlockNum;

	public int PreBlockNum;

	public string name;

	public RoleType role;

	public CarModeType carType;

	public int StartPositionId;

	public float m_Distance;

	public int CurLaps;

	public CrossOverEdge.AreaIndex areaIndex;

	public float distanceFromBeginning;

	public float distanceInLapFromGoal;

	public PlayerData()
	{
		PlayerId = 0u;
		peerID = null;
		IsMaster = false;
		Ranking = 0;
		SpendTime = 0f;
		NormalFinish = false;
		ShowLaps = 0;
		selProp = ePropType.NONE;
		altProp = ePropType.NONE;
		DriftValue = 0f;
		IsFinish = false;
		PreBlockNum = 0;
		name = null;
		role = RoleType.EBear;
		carType = CarModeType.CAR_BEAR;
		StartPositionId = 0;
		m_Distance = 0f;
		CurLaps = 0;
		areaIndex = CrossOverEdge.AreaIndex.Left;
		distanceFromBeginning = 0f;
		distanceInLapFromGoal = 0f;
	}
}
