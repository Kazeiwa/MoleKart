using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class WorldPartition<T>
{
	public struct CellIndex
	{
		public int x;

		public int y;

		public bool IsValid => x >= 0 && y >= 0;

		public CellIndex(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public class Cell
	{
		private List<T> entities = new List<T>();

		public List<T> Entities => entities;

		public int CellCount => entities.Count;

		public void AddEntity(T entity)
		{
			entities.Add(entity);
		}
	}

	public Bounds2D Boundary { get; set; }

	public int CellCountX { get; set; }

	public int CellCountY { get; set; }

	public Vector2 CellSize { get; set; }

	public Cell[] Cells { get; set; }

	public void AddEntity(T entity, Vector2 position)
	{
		CellIndex cellIndex = GetCellIndex(position);
		if (!cellIndex.IsValid)
		{
			Debug.LogWarning("WorldPartition.AddEntity: position " + position.ToString() + "out of range");
			return;
		}
		Cell cell = CreateGetCell(cellIndex);
		cell.AddEntity(entity);
	}

	public void CreateCells(Bounds2D boundary, int cx, int cy)
	{
		Boundary = boundary;
		CellCountX = cx;
		CellCountY = cy;
		int num = cx * cy;
		Cells = new Cell[num];
		CellSize = new Vector2(boundary.Size.x / (float)cx, boundary.Size.y / (float)cy);
	}

	public IEnumerable<T> EntitiesNearPos(Vector2 position)
	{
		foreach (Cell cell in CellsNearPos(position))
		{
			foreach (T entity in cell.Entities)
			{
				yield return entity;
			}
		}
	}

	public void OnDrawGizmos()
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.yellow;
		Vector2 min = Boundary.Min;
		Vector2 max = Boundary.Max;
		for (int i = 0; i <= CellCountY; i++)
		{
			float y = min.y + (float)i * CellSize.y;
			GizmosEx.DrawLine(new Vector2(min.x, y), new Vector2(max.x, y));
		}
		for (int j = 0; j <= CellCountX; j++)
		{
			float x = min.x + (float)j * CellSize.x;
			GizmosEx.DrawLine(new Vector2(x, min.y), new Vector2(x, max.y));
		}
		Gizmos.color = color;
	}

	public void OnDrawGizmosCellsNearPos(Vector2 position)
	{
		Color color = Gizmos.color;
		Gizmos.color = Color.magenta;
		foreach (CellIndex item in CellIndicesNearPos(position))
		{
			Vector2 min = Boundary.Min;
			float x = min.x + (0.5f + (float)item.x) * CellSize.x;
			float y = min.y + (0.5f + (float)item.y) * CellSize.y;
			Gizmos.color = Color.magenta;
			GizmosEx.DrawCircle(new Vector2(x, y), 4f);
			Gizmos.color = Color.cyan;
			GizmosEx.DrawCircle(new Vector2(x, y), 5f);
			Gizmos.color = Color.yellow;
			GizmosEx.DrawCircle(new Vector2(x, y), 6f);
		}
		Gizmos.color = color;
	}

	public IEnumerable<Cell> CellsNearPos(Vector2 position)
	{
		foreach (CellIndex index in CellIndicesNearPos(position))
		{
			Cell cell = GetCell(index);
			if (cell != null)
			{
				yield return cell;
			}
		}
	}

	private IEnumerable<CellIndex> CellIndicesNearPos(Vector2 position)
	{
		CellIndex cellIndex = GetCellIndex(position);
		if (!cellIndex.IsValid)
		{
			yield break;
		}
		for (int j = -1; j <= 1; j++)
		{
			int y = cellIndex.y + j;
			if (y < 0 || y >= CellCountY)
			{
				continue;
			}
			for (int i = -1; i <= 1; i++)
			{
				int x = cellIndex.x + i;
				if (x >= 0 && x < CellCountX)
				{
					yield return new CellIndex(x, y);
				}
			}
		}
	}

	private CellIndex GetCellIndex(Vector2 position)
	{
		int num = (int)((position.x - Boundary.Min.x) / CellSize.x);
		if (num >= CellCountX)
		{
			num = CellCountX - 1;
		}
		else if (num < 0)
		{
			num = 0;
		}
		int num2 = (int)((position.y - Boundary.Min.y) / CellSize.y);
		if (num2 >= CellCountY)
		{
			num2 = CellCountY - 1;
		}
		else if (num2 < 0)
		{
			num2 = 0;
		}
		return new CellIndex(num, num2);
	}

	private Cell CreateGetCell(CellIndex cellIndex)
	{
		int num = cellIndex.x + cellIndex.y * CellCountX;
		if (Cells[num] == null)
		{
			Cells[num] = new Cell();
		}
		return Cells[num];
	}

	private Cell GetCell(CellIndex cellIndex)
	{
		int num = cellIndex.x + cellIndex.y * CellCountX;
		return Cells[num];
	}

	private Cell GetCell(int x, int y)
	{
		int num = x + y * CellCountX;
		return Cells[num];
	}
}
