using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace RaceTrack;

public class RaceGraph
{
	public class Part
	{
		public string Name { get; set; }

		public List<int> NodeIndices { get; set; }

		public Part()
		{
			Name = null;
			NodeIndices = new List<int>();
		}

		public Part(string name)
		{
			Name = name;
			NodeIndices = new List<int>();
		}

		public void AddIndex(int index)
		{
			NodeIndices.Add(index);
		}
	}

	private int nextNodeIndex;

	public IList<RaceNode> Nodes { get; set; }

	public IList<List<RaceEdge>> EdgeLists { get; set; }

	public IList<Part> Parts { get; set; }

	public IEnumerable<RaceEdge> Edges
	{
		get
		{
			foreach (List<RaceEdge> edgeList in EdgeLists)
			{
				foreach (RaceEdge item in edgeList)
				{
					yield return item;
				}
			}
		}
	}

	public List<List<RaceEdge>> ReverseEdgeLists { get; set; }

	public int NodeCount => Nodes.Count;

	public int EdgeCount
	{
		get
		{
			int num = 0;
			foreach (List<RaceEdge> edgeList in EdgeLists)
			{
				num += edgeList.Count;
			}
			return num;
		}
	}

	public WorldPartition<RaceNode> NodePartition { get; set; }

	public WorldPartition<RaceEdge> EdgePartition { get; set; }

	public RaceGraph()
	{
		EdgeLists = new List<List<RaceEdge>>();
		ReverseEdgeLists = new List<List<RaceEdge>>();
		Parts = new List<Part>();
	}

	public void AllocNodes(int count)
	{
		Nodes = new List<RaceNode>(count);
		for (int i = 0; i < count; i++)
		{
			Nodes.Add(null);
		}
	}

	public RaceNode Node(int index)
	{
		RaceNode result = null;
		try
		{
			result = Nodes[index];
		}
		catch (ArgumentOutOfRangeException)
		{
			Debug.LogError("Node Index Out Of Range: " + index);
		}
		return result;
	}

	public int AddNode(RaceNode node)
	{
		Nodes[node.Index] = node;
		EdgeLists.Add(new List<RaceEdge>());
		ReverseEdgeLists.Add(new List<RaceEdge>());
		return nextNodeIndex++;
	}

	public bool HasNode(int idx)
	{
		return Node(idx).Index != -1 && idx < NodeCount;
	}

	public void AddEdge(RaceEdge edge)
	{
		if (!HasEdge(edge.To, edge.From))
		{
			EdgeLists[edge.From].Add(edge);
			ReverseEdgeLists[edge.To].Add(edge);
		}
	}

	public bool HasEdge(int fromIdx, int toIdx)
	{
		if (!HasNode(fromIdx) || !HasNode(toIdx))
		{
			return false;
		}
		foreach (RaceEdge item in EdgeLists[fromIdx])
		{
			if (item.To == toIdx)
			{
				return true;
			}
		}
		return false;
	}

	public int QueryNodeIndex(Vector2 pos)
	{
		foreach (RaceNode node in Nodes)
		{
			if (node.MatchPosition(pos))
			{
				return node.Index;
			}
		}
		return -1;
	}

	public int NextNodeCount(int index)
	{
		return EdgeLists[index].Count;
	}

	public IEnumerable<int> NextNodeIndices(int index)
	{
		List<RaceEdge> edgeList = EdgeLists[index];
		for (int i = 0; i < edgeList.Count; i++)
		{
			yield return edgeList[i].To;
		}
	}

	public IEnumerable<RaceNode> NextNodes(int index)
	{
		List<RaceEdge> edgeList = EdgeLists[index];
		for (int i = 0; i < edgeList.Count; i++)
		{
			yield return Node(edgeList[i].To);
		}
	}

	public int NextNodeIndexOfNode(int nodeIndex, int nth)
	{
		return EdgeLists[nodeIndex][nth].To;
	}

	public RaceNode NextNodeOfNode(int nodeIndex, int nth)
	{
		return Node(EdgeLists[nodeIndex][nth].To);
	}

	public bool IsFromToNodePair(int from, int to)
	{
		foreach (int item in NextNodeIndices(from))
		{
			if (item == to)
			{
				return true;
			}
		}
		return false;
	}

	public IEnumerable<int> PrevNodeIndices(int index)
	{
		List<RaceEdge> edgeList = ReverseEdgeLists[index];
		for (int i = 0; i < edgeList.Count; i++)
		{
			yield return edgeList[i].From;
		}
	}

	public IList<RaceEdge> QueryEdgesByFromNode(int nodeIdx)
	{
		return EdgeLists[nodeIdx];
	}

	public RaceNode QueryClosestNode(Vector2 pos)
	{
		float num = float.PositiveInfinity;
		RaceNode result = null;
		foreach (RaceNode node in Nodes)
		{
			float num2 = (pos - node.Pos).SqrMagnitude();
			if (num2 < num)
			{
				num = num2;
				result = node;
			}
		}
		return result;
	}

	public void SetPartition(Bounds2D boundary, int cellCountX, int cellCountY)
	{
		NodePartition = new WorldPartition<RaceNode>();
		NodePartition.CreateCells(boundary, cellCountX, cellCountY);
		EdgePartition = new WorldPartition<RaceEdge>();
		EdgePartition.CreateCells(boundary, cellCountX, cellCountY);
		foreach (RaceNode node in Nodes)
		{
			NodePartition.AddEntity(node, node.Pos);
			foreach (RaceEdge item in QueryEdgesByFromNode(node.Index))
			{
				EdgePartition.AddEntity(item, node.Pos);
			}
		}
	}

	public IEnumerable<RaceNode> NodesNearPos(Vector2 position)
	{
		if (NodePartition == null)
		{
			return null;
		}
		return NodePartition.EntitiesNearPos(position);
	}

	public IEnumerable<RaceEdge> EdgesNearPos(Vector2 position)
	{
		if (EdgePartition == null)
		{
			return null;
		}
		return EdgePartition.EntitiesNearPos(position);
	}

	public IEnumerable<BareEdge> BareEdgesNearPos(Vector2 position)
	{
		foreach (RaceEdge edge in EdgesNearPos(position))
		{
			yield return new BareEdge(Node(edge.From).Pos, Node(edge.To).Pos);
		}
	}

	public IEnumerable<RichEdge> RichEdgesNearPos(Vector2 position)
	{
		foreach (RaceEdge edge in EdgesNearPos(position))
		{
			yield return new RichEdge(Node(edge.From), Node(edge.To));
		}
	}

	public void Clear()
	{
		nextNodeIndex = 0;
		Nodes.Clear();
		EdgeLists.Clear();
		ReverseEdgeLists.Clear();
	}

	public Part CreatePart(string name)
	{
		Part part = new Part(name);
		Parts.Add(part);
		return part;
	}
}
