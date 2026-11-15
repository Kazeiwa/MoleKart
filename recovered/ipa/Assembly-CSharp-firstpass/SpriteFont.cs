using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SpriteFont
{
	protected delegate void ParserDel(string line);

	public TextAsset fontDef;

	protected Dictionary<char, int> charMap;

	protected SpriteChar[] chars;

	protected int lineHeight;

	protected int baseHeight;

	protected int texWidth;

	protected int texHeight;

	protected string face;

	protected int pxSize;

	protected float charSpacing;

	private int kerningsCount;

	public int LineHeight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public int BaseHeight
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public int PixelSize
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public float CharacterSpacing
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
		set
		{
		}
	}

	public SpriteFont(TextAsset def)
	{
	}

	public void Load(TextAsset def)
	{
	}

	private int ParseSection(string tag, string[] lines, ParserDel parser, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int FindField(string label, string[] fields, int pos, bool logError)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int FindField(string label, string[] fields, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private int FindFieldOptional(string label, string[] fields, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void HeaderParser(string line)
	{
	}

	private void CommonParser(string line)
	{
	}

	private void CharCountParser(string line)
	{
	}

	private bool CharParser(string line, int charNum)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void KerningCountParser(string line)
	{
	}

	private bool KerningParser(string line)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public SpriteChar GetSpriteChar(char ch)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool ContainsCharacter(char ch)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetWidth(string str)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetWidth(string str, int start, int end)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetWidth(StringBuilder sb, int start, int end)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetWidth(char prevChar, char c)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public string RemoveUnsupportedCharacters(string str)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}
}
