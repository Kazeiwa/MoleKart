using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class PlayerAIProfileBuilder
{
	public int Level { get; set; }

	public PlayerAIProfileBuilder(int level)
	{
		Level = level;
	}

	public static PlayerAIProfile.Trait RandomTrait()
	{
		return (PlayerAIProfile.Trait)Random.Range(0, 4);
	}

	public static void Build(IEnumerable<Car> vehicles, int level)
	{
		PlayerAIProfileBuilder playerAIProfileBuilder = new PlayerAIProfileBuilder(level);
		playerAIProfileBuilder.Build(vehicles);
	}

	public void Build(IEnumerable<Car> vehicles)
	{
		switch (Level)
		{
		case 0:
			SetProfile(PlayerAIProfile.Trait.T3, vehicles);
			break;
		case 1:
			BuildForLevel1(vehicles);
			break;
		case 2:
			SetProfile(PlayerAIProfile.Trait.T0, vehicles);
			break;
		default:
			Debug.LogError("Unsupported level: " + Level);
			break;
		}
	}

	private static void SetProfile(PlayerAIProfile.Trait trait, IEnumerable<Car> vehicles)
	{
		foreach (Car vehicle in vehicles)
		{
			vehicle.AttachAIProfile(new PlayerAIProfile(trait));
		}
	}

	private static int CountOfNpcVehicle(IEnumerable<Car> vehicles)
	{
		int num = 0;
		foreach (Car vehicle in vehicles)
		{
			if (vehicle is NPCCar)
			{
				num++;
			}
		}
		return num;
	}

	private void BuildForLevel1(IEnumerable<Car> vehicles)
	{
		int num = CountOfNpcVehicle(vehicles);
		int num2 = num / 2;
		PlayerAIProfile.Trait[] array = new PlayerAIProfile.Trait[num];
		int i;
		for (i = 0; i < num2; i++)
		{
			array[i] = PlayerAIProfile.Trait.T1;
			array[num - i - 1] = PlayerAIProfile.Trait.T2;
		}
		if (num % 2 != 0)
		{
			array[i] = PlayerAIProfile.Trait.T0;
		}
		RandomEx.Shuffle(array);
		int num3 = 0;
		foreach (Car vehicle in vehicles)
		{
			PlayerAIProfile.Trait trait = ((!(vehicle is NPCCar)) ? PlayerAIProfile.Trait.T2 : array[num3++]);
			vehicle.AttachAIProfile(new PlayerAIProfile(trait));
		}
	}
}
