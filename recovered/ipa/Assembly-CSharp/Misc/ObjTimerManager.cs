using System.Collections.Generic;

namespace Misc;

public class ObjTimerManager<T> where T : class
{
	private IDictionary<T, CrudeElapsedTimer> mapping;

	private IList<T> tempKeysToRemove;

	public IDictionary<T, CrudeElapsedTimer> Mapping
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public IEnumerable<T> Objects
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public void Add(T obj, float limit)
	{
	}

	public bool HasObject(T obj)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Remove(T obj)
	{
	}

	public void Update(float deltaTime)
	{
	}
}
