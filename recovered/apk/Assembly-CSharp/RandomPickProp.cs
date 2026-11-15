using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RandomPickProp
{
	private static RandomPickProp instance;

	private Dictionary<int, Dictionary<int, ePropType>> m_PickPropTable = new Dictionary<int, Dictionary<int, ePropType>>();

	private Dictionary<int, ePropType> m_IDToType = new Dictionary<int, ePropType>
	{
		{
			10,
			ePropType.BANANA
		},
		{
			11,
			ePropType.BANANA_THREE
		},
		{
			12,
			ePropType.TORTOISE_RED
		},
		{
			13,
			ePropType.TORTOISE_RED_THREE
		},
		{
			14,
			ePropType.TORTOISE_GREEN
		},
		{
			15,
			ePropType.TORTOISE_GREEN_THREE
		},
		{
			16,
			ePropType.TORTOISE_BLUE
		},
		{
			17,
			ePropType.MUSHROOM_ONE
		},
		{
			18,
			ePropType.MUSHROOM_THREE
		},
		{
			19,
			ePropType.MUSHROOM_GOLDERN
		},
		{
			20,
			ePropType.LUCKY_STAR
		},
		{
			21,
			ePropType.MISSILE
		},
		{
			22,
			ePropType.BOMB
		},
		{
			23,
			ePropType.LIGHTNING
		},
		{
			24,
			ePropType.CUTTLE_FISH
		},
		{
			26,
			ePropType.CHEAT_BOX
		},
		{
			27,
			ePropType.MUSHROOM_BIG
		},
		{
			28,
			ePropType.QUAKE
		},
		{
			29,
			ePropType.THUNDER_CLOUD
		}
	};

	private int m_PreMode = -1;

	public static RandomPickProp Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new RandomPickProp();
			}
			return instance;
		}
	}

	private RandomPickProp()
	{
	}

	public void Init()
	{
		TextAsset textAsset = null;
		if (AppMain.Instance.IsOnlineMode)
		{
			if (m_PreMode == 1)
			{
				return;
			}
			textAsset = Resources.Load("Config/prop/OnlineRandomPickProp") as TextAsset;
			m_PreMode = 1;
		}
		else
		{
			if (m_PreMode == 2)
			{
				return;
			}
			textAsset = Resources.Load("Config/prop/RandomPickProp") as TextAsset;
			m_PreMode = 2;
		}
		m_PickPropTable.Clear();
		Stream stream = new MemoryStream(textAsset.bytes);
		StreamReader streamReader = new StreamReader(stream);
		if (streamReader == null)
		{
			return;
		}
		bool flag = true;
		string[] array = null;
		string text;
		while ((text = streamReader.ReadLine()) != null)
		{
			if (string.IsNullOrEmpty(text))
			{
				continue;
			}
			char[] separator = new char[1] { '\t' };
			if (flag)
			{
				array = text.Split(separator);
				flag = false;
				continue;
			}
			string[] array2 = text.Split(separator);
			if (array2 == null)
			{
				continue;
			}
			Dictionary<int, ePropType> dictionary = new Dictionary<int, ePropType>();
			m_PickPropTable.Add(Convert.ToInt32(array2[0]), dictionary);
			int num = 0;
			for (int i = 1; i < array2.Length; i++)
			{
				if (Convert.ToInt32(array2[i]) > 0)
				{
					num += Convert.ToInt32(array2[i]);
					dictionary.Add(num, m_IDToType[Convert.ToInt32(array[i])]);
				}
			}
		}
	}

	public ePropType GetProp(int rank)
	{
		if (rank <= 0 || rank >= 8)
		{
			return ePropType.NONE;
		}
		System.Random random = new System.Random((int)((Time.timeSinceLevelLoad - (float)(int)Time.timeSinceLevelLoad) * 1000f));
		int num = random.Next() % 100;
		int[] array = new int[m_PickPropTable[rank].Count];
		m_PickPropTable[rank].Keys.CopyTo(array, 0);
		for (int i = 0; i < m_PickPropTable[rank].Count; i++)
		{
			if (num < array[i])
			{
				return m_PickPropTable[rank][array[i]];
			}
		}
		return ePropType.NONE;
	}
}
