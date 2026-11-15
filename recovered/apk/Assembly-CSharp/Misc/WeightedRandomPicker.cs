using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class WeightedRandomPicker<T>
{
	public struct WeightedItem
	{
		public T item;

		public float weight;

		public WeightedItem(T item, float weight)
		{
			this.item = item;
			this.weight = weight;
		}
	}

	private float totalWeight;

	public IList<WeightedItem> WeightedItems;

	public WeightedRandomPicker()
	{
		WeightedItems = new List<WeightedItem>();
	}

	public void AddItem(T item, float weight)
	{
		WeightedItems.Add(new WeightedItem(item, weight));
	}

	public T Pick()
	{
		float num = Random.Range(0f, totalWeight);
		float num2 = 0f;
		T item = WeightedItems[0].item;
		foreach (WeightedItem weightedItem in WeightedItems)
		{
			item = weightedItem.item;
			num2 += weightedItem.weight;
			if (num <= num2)
			{
				break;
			}
		}
		return item;
	}

	public void ComputeTotalWeight()
	{
		float num = 0f;
		foreach (WeightedItem weightedItem in WeightedItems)
		{
			num += weightedItem.weight;
		}
		totalWeight = num;
	}
}
