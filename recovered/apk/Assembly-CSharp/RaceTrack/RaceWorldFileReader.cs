using System.IO;

namespace RaceTrack;

public class RaceWorldFileReader
{
	private RaceWorld world;

	private string dataFilePath;

	public RaceWorldFileReader(RaceWorld world, string dataFilePath)
	{
		this.world = world;
		this.dataFilePath = dataFilePath;
	}

	public bool Read()
	{
		string text = ReadStringFromFile();
		if (text == null)
		{
			return false;
		}
		RaceWorldReader raceWorldReader = new RaceWorldReader(world);
		return raceWorldReader.Read(text);
	}

	private string ReadStringFromFile()
	{
		try
		{
			TextReader textReader = new StreamReader(dataFilePath);
			string result = textReader.ReadToEnd();
			textReader.Close();
			return result;
		}
		catch (IOException)
		{
		}
		return null;
	}
}
