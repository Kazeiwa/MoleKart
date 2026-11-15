using FrameWork;

public class PickPropManager
{
	private static PickPropManager instance;

	private PlayerData playerData = new PlayerData();

	public static PickPropManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new PickPropManager();
			}
			return instance;
		}
	}

	private PickPropManager()
	{
	}

	public bool PickProp(Car car)
	{
		if (null == car)
		{
			return false;
		}
		ePropType ePropType2 = ePropType.NONE;
		RaceSceneData.GetPlayerData((ulong)car.ID, ref playerData);
		ePropType2 = RandomPickProp.Instance.GetProp(playerData.Ranking);
		IFrameWork frameWork = CFrameWork.GetFrameWork();
		if (frameWork != null && frameWork.GetModule(enmModuleID.MI_RACELOGIC) is IRaceLogicModule raceLogicModule)
		{
			if (raceLogicModule.IsOnlineMode())
			{
				if (car.isHero && playerData.selProp == ePropType.NONE && playerData.altProp == ePropType.NONE)
				{
					SceneUIManager.Instance.RollPropUI(ePropType2);
				}
			}
			else if (playerData.selProp == ePropType.NONE && playerData.altProp == ePropType.NONE)
			{
				if (car.isHero)
				{
					SceneUIManager.Instance.RollPropUI(ePropType2);
				}
				else
				{
					car.PickProp(ePropType2);
					if (ePropType2 == ePropType.THUNDER_CLOUD)
					{
						SkillManager.Instance.UseProp(car, throwForward: false);
					}
				}
			}
		}
		return true;
	}

	public bool PickProp(Car car, ePropType type)
	{
		if (null == car)
		{
			return false;
		}
		RaceSceneData.GetPlayerData((ulong)car.ID, ref playerData);
		if (playerData.selProp == ePropType.NONE && playerData.altProp == ePropType.NONE && car.isHero)
		{
			SceneUIManager.Instance.RollPropUI(type);
		}
		return true;
	}
}
