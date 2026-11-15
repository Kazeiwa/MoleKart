using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc;

public class NameIndexer
{
	public List<string> Names { get; set; }

	public NameIndexer()
	{
		Names = new List<string>();
	}

	public void SortFrom(IEnumerable<string> names)
	{
		Names.Clear();
		foreach (string name in names)
		{
			Names.Add(name);
		}
		Names.Sort();
	}

	public int Name2Index(string name)
	{
		int num = 0;
		foreach (string name2 in Names)
		{
			if (name2 == name)
			{
				return num;
			}
			num++;
		}
		return -1;
	}

	public string Index2Name(int index)
	{
		if (index < Names.Count)
		{
			try
			{
				return Names[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				Debug.LogError("NameIndexer.Index2name: error: index: " + index + ", count: " + Names.Count);
			}
		}
		return null;
	}

	public void Reset()
	{
		Names.Clear();
	}
}
