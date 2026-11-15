public class CollisionTable
{
	private static float[,] m_fCTable = new float[13, 13]
	{
		{
			1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			4f, 1f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			8f, 4f, 1f, 1f, 0f, 0f, 0f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			12f, 8f, 4f, 4f, 1f, 0f, 0f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			16f, 12f, 8f, 6f, 4f, 1f, 0f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			20f, 16f, 12f, 8f, 6f, 4f, 1f, 0f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			20f, 20f, 16f, 12f, 8f, 8f, 5f, 1f, 0f, 0f,
			0f, 0f, 0f
		},
		{
			20f, 20f, 20f, 16f, 12f, 8f, 4f, 6f, 1f, 2f,
			0f, 0f, 0f
		},
		{
			20f, 20f, 20f, 20f, 16f, 16f, 4f, 5f, 6f, 4f,
			6f, 0f, 0f
		},
		{
			20f, 20f, 20f, 20f, 20f, 16f, 14f, 12f, 10f, 7f,
			7f, 6f, 0f
		},
		{
			20f, 20f, 20f, 20f, 20f, 20f, 16f, 14f, 12f, 10f,
			8f, 8f, 6f
		},
		{
			20f, 20f, 20f, 20f, 20f, 20f, 20f, 16f, 12f, 10f,
			10f, 10f, 8f
		},
		{
			20f, 20f, 20f, 20f, 20f, 20f, 20f, 20f, 16f, 12f,
			12f, 12f, 10f
		}
	};

	public static float GetCollisionOffset(int iFrom, int iTo)
	{
		if (iFrom > 12)
		{
			iFrom = 12;
		}
		if (iTo > 12)
		{
			iTo = 12;
		}
		if (iFrom < 0)
		{
			iFrom = 0;
		}
		if (iTo < 0)
		{
			iTo = 0;
		}
		return m_fCTable[iTo, iFrom];
	}
}
