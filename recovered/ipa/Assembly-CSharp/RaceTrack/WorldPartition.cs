using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class WorldPartition<T>
{
	public struct CellIndex
	{
		public int X;

		public int Y;

		public bool IsValid
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public CellIndex(int x, int y)
		{
		}
	}

	public class Cell
	{
		private List<T> entities;

		public List<T> Entities
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public int CellCount
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public void AddEntity(T entity)
		{
		}
	}

	public Bounds2D Boundary
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public int CellCountX
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public int CellCountY
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public Vector2 CellSize
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public Cell[] Cells
	{
		[CompilerGenerated]
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		[CompilerGenerated]
		set
		{
		}
	}

	public void AddEntity(T entity, Vector2 position)
	{
	}

	public void CreateCells(Bounds2D boundary, int cx, int cy)
	{
	}

	[DebuggerHidden]
	public IEnumerable<T> EntitiesNearPos(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void OnDrawGizmos()
	{
	}

	public void OnDrawGizmosCellsNearPos(Vector2 position)
	{
	}

	[DebuggerHidden]
	public IEnumerable<Cell> CellsNearPos(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	[DebuggerHidden]
	private IEnumerable<CellIndex> CellIndicesNearPos(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private CellIndex GetCellIndex(Vector2 position)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Cell CreateGetCell(CellIndex cellIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Cell GetCell(CellIndex cellIndex)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Cell GetCell(int x, int y)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
