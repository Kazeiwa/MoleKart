using System;

namespace Misc;

public class RandomEx
{
	public static void Shuffle<T>(T[] array)
	{
		Random random = new Random();
		for (int num = array.Length; num > 1; num--)
		{
			int num2 = random.Next(num);
			T val = array[num2];
			array[num2] = array[num - 1];
			array[num - 1] = val;
		}
	}
}
