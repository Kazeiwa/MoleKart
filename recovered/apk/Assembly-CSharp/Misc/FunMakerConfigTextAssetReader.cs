using UnityEngine;

namespace Misc;

public class FunMakerConfigTextAssetReader
{
	private FunMakerConfig maker;

	private string assetName;

	public FunMakerConfigTextAssetReader(FunMakerConfig maker, string assetName)
	{
		this.maker = maker;
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
		FunMakerConfigReader funMakerConfigReader = new FunMakerConfigReader(maker);
		return funMakerConfigReader.Read(text);
	}
}
