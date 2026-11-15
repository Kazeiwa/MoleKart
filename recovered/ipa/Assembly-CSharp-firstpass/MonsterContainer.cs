using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class MonsterContainer
{
	private class Nested
	{
		internal static readonly MonsterContainer Instance;

		[SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "ForSingleton")]
		static Nested()
		{
		}
	}

	private List<MonsterBaseData> monsterList;

	public List<MonsterBaseData> Monters
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public static MonsterContainer Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void AddMonster(MonsterBaseData monster)
	{
	}

	public void RemoveMonster(MonsterBaseData monster)
	{
	}
}
