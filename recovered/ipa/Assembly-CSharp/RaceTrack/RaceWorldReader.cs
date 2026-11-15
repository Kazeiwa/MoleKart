using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RaceTrack;

public class RaceWorldReader
{
	public struct Token
	{
		public enum TokenType
		{
			OBJECT_BEGIN,
			OBJECT_END,
			ARRAY_BEGIN,
			ARRAY_END,
			L_PAREN,
			R_PAREN,
			ID,
			STRING,
			NUM_INT,
			NUM_REAL,
			BOOL_TRUE,
			BOOL_FALSE,
			NULL_TOKEN,
			COMMA,
			COLON,
			EQUAL,
			TO,
			NEWLINE,
			END_OF_INPUT,
			INVALID_TOKEN
		}

		public TokenType Type;

		public int Begin;

		public int Length;

		private int numInt;

		private float numReal;

		public string Text;

		public int NumInt
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			set
			{
			}
		}

		public float NumReal
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
			set
			{
			}
		}

		public float RealValue
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public static Token EndOfInput
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public static Token InvalidToken
		{
			get
			{
				/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
			}
		}

		public Token(TokenType t, int begin, int lenght)
		{
		}

		public bool IsNumber()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public bool IsInt()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public bool IsReal()
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static Token IdentifierToken(int begin, int length)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static Token StringToken(int begin, int length)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static Token NumberToken(int begin, int length, float num)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}

		public static Token NumberToken(int begin, int length, int num)
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public enum RaceWorldParseError
	{
		NO_ERROR,
		INVALID_ESCAPE,
		SYNTAX_ERROR,
		UNSUPPORTED_ENCODING,
		UNEXPECTED_DATA_AFTER_WORLD
	}

	private const int BUILDER_CAPACITY = 2000;

	private const int TrackLevelCount = 3;

	private const string kNullString = "null";

	private const string kTrueString = "true";

	private const string kFalseString = "false";

	private RaceWorld world;

	private List<Vector2> wallEnds;

	public RaceWorldParseError ErrorCode;

	private RaceGraph.Part CurrentRacingLinePart
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

	public string ErrorMessage
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	public RaceWorldReader(RaceWorld world)
	{
	}

	public bool Read(string data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public bool ReadAndReadError(char[] data, ref int error_code_out, ref string error_msg_out)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static string ErrorCodeToString(RaceWorldParseError error_code)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool DataToWorld(char[] data)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private string FormatErrorMessage(int line, int column, string description)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool BuildWorld(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Chain ParseChain(char[] data, ref int pos, bool reverse)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseWallEnds(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseWallEdges(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void AddWallEnd(Vector2 v)
	{
	}

	private bool ParseRaceNodes(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseEdges(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseEdge(char[] data, ref int pos, ref int fromIdx, ref int toIdx)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseNumber(char[] data, ref int pos, ref float v)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseVector2(char[] data, ref int pos, out Vector2 v)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private IList<float> ParseFloatList(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool ParseSimplePair(char[] data, ref int pos, ref string name, ref Token val)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private RaceNode ParseRaceNode(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static void EatWhitespaceAndComments(char[] data, ref int pos)
	{
	}

	private static bool EatComment(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Token LookAhead(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private Token NextToken(char[] data, ref int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static bool NextStringMatch(char[] data, ref int pos, string str)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static int GetLastPositionOfNumber(char[] data, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static Token ParseNumberToken(char[] data, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	protected static Token ParseStringToken(char[] data, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static Token ParseIdentifierToken(char[] data, int pos)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static bool IsAlphaOrUnderscore(char c)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static bool IsAlphaBetaOrUnderscore(char c)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private static bool ReadHexDigits(char[] data, ref Token token, int digits)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SetErrorCode(char[] data, RaceWorldParseError error, int error_pos)
	{
	}
}
