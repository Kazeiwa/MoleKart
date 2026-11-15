using System.Collections.Generic;

namespace Misc;

public class ObjTimerManager<T> where T : class
{
	private IDictionary<T, CrudeElapsedTimer> mapping = new Dictionary<T, CrudeElapsedTimer>();

	private IList<T> tempKeysToRemove = new List<T>();

	public IDictionary<T, CrudeElapsedTimer> Mapping => mapping;

	public IEnumerable<T> Objects => mapping.Keys;

	public void Add(T obj, float limit)
	{
		CrudeElapsedTimer value = new CrudeElapsedTimer(limit);
		mapping[obj] = value;
	}

	public bool HasObject(T obj)
	{
		return mapping.ContainsKey(obj);
	}

	public void Remove(T obj)
	{
		if (mapping.ContainsKey(obj))
		{
			mapping.Remove(obj);
		}
	}

	public void Update(float deltaTime)
	{
		foreach (KeyValuePair<T, CrudeElapsedTimer> item in Mapping)
		{
			item.Value.Advance(deltaTime);
			if (item.Value.TimeOutCount > 0)
			{
				tempKeysToRemove.Add(item.Key);
			}
		}
		foreach (T item2 in tempKeysToRemove)
		{
			Mapping.Remove(item2);
		}
		tempKeysToRemove.Clear();
	}
}
