using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Misc;
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
				if (Type != TokenType.NUM_INT)
				{
					Debug.LogError("Should not access the NumInt property of a token without type NUM_INT.");
				}
				return numInt;
			}
			set
			{
				if (Type != TokenType.NUM_INT)
				{
					Debug.LogError("Should not access the NumInt property of a token without type NUM_INT.");
				}
				numInt = value;
			}
		}

		public float NumReal
		{
			get
			{
				if (Type != TokenType.NUM_REAL)
				{
					Debug.LogError("Should not access the NumReal property of a token without type NUM_REAL.");
				}
				return numReal;
			}
			set
			{
				if (Type != TokenType.NUM_REAL)
				{
					Debug.LogError("Should not access the NumReal property of a token without type NUM_REAL.");
				}
				numReal = value;
			}
		}

		public float RealValue
		{
			get
			{
				if (Type == TokenType.NUM_INT)
				{
					return NumInt;
				}
				if (Type == TokenType.NUM_REAL)
				{
					return NumReal;
				}
				return 0f;
			}
		}

		public static Token EndOfInput => new Token(TokenType.END_OF_INPUT, 0, 0);

		public static Token InvalidToken => new Token(TokenType.INVALID_TOKEN, 0, 0);

		public Token(TokenType t, int begin, int lenght)
		{
			Type = t;
			Begin = begin;
			Length = lenght;
			numInt = 0;
			numReal = 0f;
			Text = null;
		}

		public bool IsNumber()
		{
			return Type == TokenType.NUM_INT || Type == TokenType.NUM_REAL;
		}

		public bool IsInt()
		{
			return Type == TokenType.NUM_INT;
		}

		public bool IsReal()
		{
			return Type == TokenType.NUM_REAL;
		}

		public static Token IdentifierToken(int begin, int length)
		{
			return new Token(TokenType.ID, begin, length);
		}

		public static Token StringToken(int begin, int length)
		{
			return new Token(TokenType.STRING, begin, length);
		}

		public static Token NumberToken(int begin, int length, float num)
		{
			Token result = new Token(TokenType.NUM_REAL, begin, length);
			result.NumReal = num;
			return result;
		}

		public static Token NumberToken(int begin, int length, int num)
		{
			Token result = new Token(TokenType.NUM_INT, begin, length);
			result.NumInt = num;
			return result;
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

	private List<Vector2> wallEnds = new List<Vector2>();

	public RaceWorldParseError ErrorCode;

	private RaceGraph.Part CurrentRacingLinePart { get; set; }

	public string ErrorMessage => string.Empty;

	public RaceWorldReader(RaceWorld world)
	{
		this.world = world;
		CurrentRacingLinePart = null;
	}

	public bool Read(string data)
	{
		if (data == null)
		{
			return false;
		}
		char[] data2 = data.ToCharArray();
		int error_code_out = 0;
		string error_msg_out = string.Empty;
		return ReadAndReadError(data2, ref error_code_out, ref error_msg_out);
	}

	public bool ReadAndReadError(char[] data, ref int error_code_out, ref string error_msg_out)
	{
		bool result = DataToWorld(data);
		error_code_out = (int)ErrorCode;
		error_msg_out = ErrorMessage;
		return result;
	}

	public static string ErrorCodeToString(RaceWorldParseError error_code)
	{
		return string.Empty;
	}

	private bool DataToWorld(char[] data)
	{
		int pos = 0;
		ErrorCode = RaceWorldParseError.NO_ERROR;
		bool flag = BuildWorld(data, ref pos);
		if (flag && LookAhead(data, ref pos).Type != Token.TokenType.END_OF_INPUT)
		{
			flag = false;
			SetErrorCode(data, RaceWorldParseError.UNEXPECTED_DATA_AFTER_WORLD, pos);
		}
		return flag;
	}

	private string FormatErrorMessage(int line, int column, string description)
	{
		return string.Empty;
	}

	private bool BuildWorld(char[] data, ref int pos)
	{
		while (true)
		{
			Token token = LookAhead(data, ref pos);
			if (token.Type == Token.TokenType.END_OF_INPUT)
			{
				break;
			}
			if (token.Type != Token.TokenType.ID)
			{
				return false;
			}
			string text = token.Text;
			switch (text)
			{
			case "race_nodes":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.STRING)
				{
					return false;
				}
				pos += token.Length;
				CurrentRacingLinePart = world.RacingLine.CreatePart(token.Text);
				if (!ParseRaceNodes(data, ref pos))
				{
					return false;
				}
				continue;
			case "race_edges":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseEdges(data, ref pos))
				{
					return false;
				}
				continue;
			case "wall":
			case "rwall":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				bool reverse = text == "rwall";
				Chain chain = ParseChain(data, ref pos, reverse);
				if (chain == null)
				{
					return false;
				}
				world.AddWallChain(chain);
				continue;
			}
			case "wall_ends":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseWallEnds(data, ref pos))
				{
					return false;
				}
				continue;
			case "wall_edges":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseWallEdges(data, ref pos))
				{
					return false;
				}
				continue;
			case "time1":
			case "time2":
			case "dist_bias1":
			case "dist_bias2":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				IList<float> list = ParseFloatList(data, ref pos);
				if (list.Count != 3)
				{
					Debug.LogError("RaceWorld read error: " + text + " should be an list of " + 3 + " floats.");
					return false;
				}
				switch (text)
				{
				case "time1":
				{
					for (int l = 0; l < 3; l++)
					{
						world.PropTimeLengthArray[l] = list[l];
					}
					break;
				}
				case "time2":
				{
					for (int j = 0; j < 3; j++)
					{
						world.TimingTimeLengthArray[j] = list[j];
					}
					break;
				}
				case "dist_bias1":
				{
					for (int k = 0; k < 3; k++)
					{
						world.PropDistanceBiasArray[k] = list[k];
					}
					break;
				}
				case "dist_bias2":
				{
					for (int i = 0; i < 3; i++)
					{
						world.TimingDistanceBiasArray[i] = list[i];
					}
					break;
				}
				}
				continue;
			}
			case "cross_over":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseVector2(data, ref pos, out var v))
				{
					Debug.LogError("Cross over FROM parse error.");
					return false;
				}
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.TO)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseVector2(data, ref pos, out var v2))
				{
					Debug.LogError("Cross over TO parse error.");
					return false;
				}
				world.RacingLine.CreateCrossOverEdge(v, v2);
				continue;
			}
			}
			string name = null;
			Token val = Token.InvalidToken;
			if (!ParseSimplePair(data, ref pos, ref name, ref val))
			{
				continue;
			}
			if (name == "node_count")
			{
				world.RacingLine.AllocNodes(val.NumInt);
			}
			else if (name == "end_index")
			{
				if (val.IsInt())
				{
					world.RacingLine.EndIndex = val.NumInt;
				}
				else
				{
					Debug.LogError("RaceWorld read error: end_index should be an integer.");
				}
			}
		}
		world.RacingLine.SetDistanceFromGoalOnNode(world.RacingLine.EndIndex);
		world.Obstacles = CollisionLayer.Instance.GetCollisionArray(EnmCollisionLayer.CL_TREE).ConvertAll((CCollisionBody x) => x.GetIObstacle());
		Dictionary<int, Car> carsDictionary = TSingleton<CarManager>.Instance.GetCarsDictionary();
		if (carsDictionary != null)
		{
			foreach (KeyValuePair<int, Car> item in carsDictionary)
			{
				if (item.Value != null)
				{
					world.AddVehicle(item.Value);
				}
			}
		}
		Bounds2D boundary = new Bounds2D(Vector2.zero, Vector2.zero);
		foreach (RaceNode node in world.RacingLine.Nodes)
		{
			boundary.Encapsulate(node.Pos);
		}
		boundary.Expand(40f);
		world.RacingLine.SetPartition(boundary, 6, 10);
		world.SetPartition(boundary, 6, 10);
		WorldPartition<IAutoCar> partitionOfVehicle = world.PartitionOfVehicle;
		foreach (IAutoCar vehicle in world.Vehicles)
		{
			vehicle.InitSteeringBehavior(world.RacingLine);
			partitionOfVehicle.AddEntity(vehicle, vehicle.Pos);
		}
		world.AttachMonsters();
		return true;
	}

	private Chain ParseChain(char[] data, ref int pos, bool reverse)
	{
		if (LookAhead(data, ref pos).Type != Token.TokenType.L_PAREN)
		{
			return null;
		}
		Chain chain = new Chain();
		while (LookAhead(data, ref pos).Type == Token.TokenType.L_PAREN)
		{
			if (!ParseVector2(data, ref pos, out var v))
			{
				return null;
			}
			chain.Add(v, reverse);
		}
		return chain;
	}

	private bool ParseWallEnds(char[] data, ref int pos)
	{
		while (LookAhead(data, ref pos).Type == Token.TokenType.L_PAREN)
		{
			if (!ParseVector2(data, ref pos, out var v))
			{
				return false;
			}
			AddWallEnd(v);
		}
		return true;
	}

	private bool ParseWallEdges(char[] data, ref int pos)
	{
		while (LookAhead(data, ref pos).IsNumber())
		{
			int fromIdx = -1;
			int toIdx = -1;
			if (!ParseEdge(data, ref pos, ref fromIdx, ref toIdx))
			{
				return false;
			}
			Wall wall = new Wall(wallEnds[fromIdx], wallEnds[toIdx]);
			world.AddWall(wall);
		}
		return true;
	}

	private void AddWallEnd(Vector2 v)
	{
		wallEnds.Add(v);
	}

	private bool ParseRaceNodes(char[] data, ref int pos)
	{
		while (LookAhead(data, ref pos).Type == Token.TokenType.L_PAREN)
		{
			RaceNode raceNode = ParseRaceNode(data, ref pos);
			if (raceNode == null)
			{
				return false;
			}
			if (world.RacingLine != null)
			{
				world.RacingLine.AddNode(raceNode);
			}
			if (CurrentRacingLinePart != null)
			{
				CurrentRacingLinePart.AddIndex(raceNode.Index);
			}
		}
		return true;
	}

	private bool ParseEdges(char[] data, ref int pos)
	{
		RacingLine racingLine = world.RacingLine;
		while (LookAhead(data, ref pos).IsNumber())
		{
			int fromIdx = -1;
			int toIdx = -1;
			if (!ParseEdge(data, ref pos, ref fromIdx, ref toIdx))
			{
				return false;
			}
			racingLine?.AddEdge(new RaceEdge(fromIdx, toIdx));
		}
		return true;
	}

	private bool ParseEdge(char[] data, ref int pos, ref int fromIdx, ref int toIdx)
	{
		float v = 0f;
		if (!ParseNumber(data, ref pos, ref v))
		{
			return false;
		}
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.TO)
		{
			return false;
		}
		pos += token.Length;
		float v2 = 0f;
		if (!ParseNumber(data, ref pos, ref v2))
		{
			return false;
		}
		fromIdx = (int)v;
		toIdx = (int)v2;
		return true;
	}

	private bool ParseNumber(char[] data, ref int pos, ref float v)
	{
		Token token = LookAhead(data, ref pos);
		if (!token.IsNumber())
		{
			return false;
		}
		pos += token.Length;
		v = token.RealValue;
		return true;
	}

	private bool ParseVector2(char[] data, ref int pos, out Vector2 v)
	{
		v = Vector2.zero;
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.L_PAREN)
		{
			return false;
		}
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (!token.IsNumber())
		{
			return false;
		}
		v.x = token.RealValue;
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (!token.IsNumber())
		{
			return false;
		}
		v.y = token.RealValue;
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.R_PAREN)
		{
			return false;
		}
		pos += token.Length;
		return true;
	}

	private IList<float> ParseFloatList(char[] data, ref int pos)
	{
		IList<float> list = new List<float>();
		while (true)
		{
			Token token = LookAhead(data, ref pos);
			if (token.IsNumber())
			{
				list.Add(token.RealValue);
				pos += token.Length;
				continue;
			}
			break;
		}
		return list;
	}

	private bool ParseSimplePair(char[] data, ref int pos, ref string name, ref Token val)
	{
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.ID)
		{
			return false;
		}
		string text = token.Text;
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.COLON)
		{
			return false;
		}
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (token.Type == Token.TokenType.INVALID_TOKEN || token.Type == Token.TokenType.END_OF_INPUT)
		{
			return false;
		}
		pos += token.Length;
		name = text;
		val = token;
		return true;
	}

	private RaceNode ParseRaceNode(char[] data, ref int pos)
	{
		int idx = -1;
		Vector2 zero = Vector2.zero;
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.L_PAREN)
		{
			return null;
		}
		pos += token.Length;
		string name = null;
		Token val = Token.InvalidToken;
		float maxSpeed = float.PositiveInfinity;
		float leftWidth = 2f;
		float rightWidth = 2f;
		while (ParseSimplePair(data, ref pos, ref name, ref val))
		{
			switch (name)
			{
			case "x":
				zero.x = val.RealValue;
				break;
			case "y":
				zero.y = val.RealValue;
				break;
			case "index":
				idx = val.NumInt;
				break;
			case "MaxSpeed":
				maxSpeed = val.RealValue;
				break;
			case "lwidth":
				leftWidth = val.RealValue;
				break;
			case "rwidth":
				rightWidth = val.RealValue;
				break;
			}
		}
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.R_PAREN)
		{
			return null;
		}
		pos += token.Length;
		RaceNode raceNode = new RaceNode(idx, zero);
		raceNode.MaxSpeed = maxSpeed;
		raceNode.LeftWidth = leftWidth;
		raceNode.RightWidth = rightWidth;
		return raceNode;
	}

	private static void EatWhitespaceAndComments(char[] data, ref int pos)
	{
		while (pos < data.Length)
		{
			switch (data[pos])
			{
			default:
				return;
			case '\t':
			case '\n':
			case '\r':
			case ' ':
				pos++;
				break;
			case '/':
				if (!EatComment(data, ref pos))
				{
					return;
				}
				break;
			}
		}
	}

	private static bool EatComment(char[] data, ref int pos)
	{
		if (data[pos] != '/')
		{
			return false;
		}
		switch (data[pos + 1])
		{
		case '/':
			pos += 2;
			while (pos < data.Length)
			{
				switch (data[pos])
				{
				case '\n':
				case '\r':
					pos++;
					return true;
				}
				pos++;
			}
			break;
		case '*':
			pos += 2;
			while (pos < data.Length)
			{
				if (data[pos] == '*' && data[pos + 1] == '/')
				{
					pos += 2;
					return true;
				}
				pos++;
			}
			break;
		default:
			return false;
		}
		return true;
	}

	private Token LookAhead(char[] data, ref int pos)
	{
		return NextToken(data, ref pos);
	}

	private Token NextToken(char[] data, ref int pos)
	{
		EatWhitespaceAndComments(data, ref pos);
		Token result = Token.EndOfInput;
		if (pos == data.Length)
		{
			return result;
		}
		char c = data[pos];
		switch (c)
		{
		case '\0':
			result = Token.EndOfInput;
			break;
		case 'n':
			result = ((!NextStringMatch(data, ref pos, "null")) ? ParseIdentifierToken(data, pos) : new Token(Token.TokenType.NULL_TOKEN, pos, 4));
			break;
		case 't':
			result = ((!NextStringMatch(data, ref pos, "true")) ? ParseIdentifierToken(data, pos) : new Token(Token.TokenType.BOOL_TRUE, pos, 4));
			break;
		case 'f':
			result = ((!NextStringMatch(data, ref pos, "false")) ? ParseIdentifierToken(data, pos) : new Token(Token.TokenType.BOOL_FALSE, pos, 5));
			break;
		case '[':
			result = new Token(Token.TokenType.ARRAY_BEGIN, pos, 1);
			break;
		case ']':
			result = new Token(Token.TokenType.ARRAY_END, pos, 1);
			break;
		case ',':
			result = new Token(Token.TokenType.COMMA, pos, 1);
			break;
		case '{':
			result = new Token(Token.TokenType.OBJECT_BEGIN, pos, 1);
			break;
		case '}':
			result = new Token(Token.TokenType.OBJECT_END, pos, 1);
			break;
		case '(':
			result = new Token(Token.TokenType.L_PAREN, pos, 1);
			break;
		case ')':
			result = new Token(Token.TokenType.R_PAREN, pos, 1);
			break;
		case ':':
			result = new Token(Token.TokenType.COLON, pos, 1);
			break;
		case '-':
		case '0':
		case '1':
		case '2':
		case '3':
		case '4':
		case '5':
		case '6':
		case '7':
		case '8':
		case '9':
			if (pos + 1 < data.Length)
			{
				char c2 = data[pos + 1];
				result = ((c2 != '>') ? ParseNumberToken(data, pos) : new Token(Token.TokenType.TO, pos, 2));
			}
			break;
		case '"':
			result = ParseStringToken(data, pos);
			break;
		default:
			if (('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || c == '_')
			{
				result = ParseIdentifierToken(data, pos);
			}
			break;
		}
		return result;
	}

	private static bool NextStringMatch(char[] data, ref int pos, string str)
	{
		if (pos + str.Length > data.Length)
		{
			return false;
		}
		if (str.Length == 0)
		{
			return true;
		}
		char[] array = str.ToCharArray();
		for (int i = 0; i < array.Length; i++)
		{
			if (data[pos + i] != array[i])
			{
				return false;
			}
		}
		return true;
	}

	private static int GetLastPositionOfNumber(char[] data, int pos)
	{
		int i;
		for (i = pos; i < data.Length && "0123456789+-.eE".IndexOf(data[i]) != -1; i++)
		{
		}
		return i - 1;
	}

	private static Token ParseNumberToken(char[] data, int pos)
	{
		if (pos >= data.Length)
		{
			return Token.InvalidToken;
		}
		int lastPositionOfNumber = GetLastPositionOfNumber(data, pos);
		int length = lastPositionOfNumber - pos + 1;
		string s = new string(data, pos, length);
		if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var result))
		{
			return Token.NumberToken(pos, length, result);
		}
		if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var result2))
		{
			return Token.NumberToken(pos, length, (float)result2);
		}
		return Token.InvalidToken;
	}

	protected static Token ParseStringToken(char[] data, int pos)
	{
		Token token = Token.StringToken(pos, 0);
		int num = pos;
		StringBuilder stringBuilder = new StringBuilder();
		char c = data[num++];
		bool flag = false;
		while (!flag && num != data.Length)
		{
			c = data[num++];
			switch (c)
			{
			case '"':
				flag = true;
				break;
			case '\\':
				if (num != data.Length)
				{
					switch (data[num++])
					{
					case '"':
						stringBuilder.Append('"');
						continue;
					case '\\':
						stringBuilder.Append('\\');
						continue;
					case '/':
						stringBuilder.Append('/');
						continue;
					case 'b':
						stringBuilder.Append('\b');
						continue;
					case 'f':
						stringBuilder.Append('\f');
						continue;
					case 'n':
						stringBuilder.Append('\n');
						continue;
					case 'r':
						stringBuilder.Append('\r');
						continue;
					case 't':
						stringBuilder.Append('\t');
						continue;
					case 'u':
						break;
					default:
						return Token.InvalidToken;
					}
					int num2 = data.Length - num;
					if (num2 >= 4 && uint.TryParse(new string(data, num, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var result))
					{
						stringBuilder.Append(char.ConvertFromUtf32((int)result));
						num += 4;
						continue;
					}
				}
				break;
			default:
				stringBuilder.Append(c);
				continue;
			}
			break;
		}
		if (flag)
		{
			token.Text = stringBuilder.ToString();
			token.Length = num - pos;
		}
		return (!flag) ? Token.InvalidToken : token;
	}

	private static Token ParseIdentifierToken(char[] data, int pos)
	{
		Token result = Token.IdentifierToken(pos, 0);
		char c = data[pos];
		if (!IsAlphaOrUnderscore(c))
		{
			return Token.InvalidToken;
		}
		StringBuilder stringBuilder = new StringBuilder(2000);
		do
		{
			result.Length++;
			stringBuilder.Append(c);
			c = data[pos + result.Length];
		}
		while (IsAlphaBetaOrUnderscore(c));
		result.Text = stringBuilder.ToString();
		return result;
	}

	private static bool IsAlphaOrUnderscore(char c)
	{
		return ('A' <= c && c <= 'Z') || ('a' <= c && c <= 'z') || c == '_';
	}

	private static bool IsAlphaBetaOrUnderscore(char c)
	{
		return IsAlphaOrUnderscore(c) || ('0' <= c && c <= '9');
	}

	private static bool ReadHexDigits(char[] data, ref Token token, int digits)
	{
		for (int i = 1; i <= digits; i++)
		{
			char c = data[token.Begin + token.Length + i];
			if (('0' > c || c > '9') && ('a' > c || c > 'f') && ('A' > c || c > 'F'))
			{
				return false;
			}
		}
		token.Length += digits;
		return true;
	}

	private void SetErrorCode(char[] data, RaceWorldParseError error, int error_pos)
	{
		int num = 1;
		int num2 = 1;
		for (int i = 0; i < error_pos; i++)
		{
			if (data[i] == '\n')
			{
				num++;
				num2 = 1;
			}
			else
			{
				num2++;
			}
		}
		ErrorCode = error;
	}
}
