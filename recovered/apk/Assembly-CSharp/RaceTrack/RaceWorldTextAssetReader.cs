using UnityEngine;

namespace RaceTrack;

public class RaceWorldTextAssetReader
{
	private RaceWorld world;

	private string assetName;

	public RaceWorldTextAssetReader(RaceWorld world, string assetName)
	{
		this.world = world;
		this.assetName = assetName;
	}

	public bool Read()
	{
		TextAsset textAsset = Resources.Load(assetName) as TextAsset;
		string text = textAsset.ToString();
		if (text == null)
		{
			return false;
		}
		RaceWorldReader raceWorldReader = new RaceWorldReader(world);
		return raceWorldReader.Read(text);
	}
}
