using System.Collections.Generic;
using System.Globalization;
using System.Text;
using UnityEngine;

namespace Misc;

public class FunMakerConfigReader
{
	public enum RaceWorldParseError
	{
		NO_ERROR,
		INVALID_ESCAPE,
		SYNTAX_ERROR,
		UNSUPPORTED_ENCODING,
		UNEXPECTED_DATA_AFTER_WORLD
	}

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
			PLUS,
			MINUS,
			SHARP,
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

	public class SpeedTreatmentArguments
	{
		public float Threshold1 { get; set; }

		public float Threshold2 { get; set; }

		public float SpeedDelta1 { get; set; }

		public float SpeedDelta2 { get; set; }

		public float SpeedDelta3 { get; set; }
	}

	private const int BUILDER_CAPACITY = 2000;

	private const string kNullString = "null";

	private const string kTrueString = "true";

	private const string kFalseString = "false";

	public RaceWorldParseError ErrorCode;

	private FunMakerConfig maker;

	public string ErrorMessage => string.Empty;

	public FunMakerConfigReader(FunMakerConfig maker)
	{
		this.maker = maker;
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
			case "visible_interval":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseInterval(data, ref pos, out var interval))
				{
					Debug.LogError("Parse visible_interval error!");
					return false;
				}
				maker.VisibleInterval = interval;
				continue;
			}
			case "slow_down":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				SpeedTreatmentArguments speedTreatmentArguments2 = ParseSpeedTreatmentArguments(data, ref pos);
				if (speedTreatmentArguments2 == null)
				{
					return false;
				}
				maker.SlowDownThreshold1 = speedTreatmentArguments2.Threshold1;
				maker.SlowDownThreshold2 = speedTreatmentArguments2.Threshold2;
				maker.SlowDownSpeedDelta1 = speedTreatmentArguments2.SpeedDelta1;
				maker.SlowDownSpeedDelta2 = speedTreatmentArguments2.SpeedDelta2;
				maker.SlowDownSpeedDelta3 = speedTreatmentArguments2.SpeedDelta3;
				continue;
			}
			case "speed_up":
			{
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				SpeedTreatmentArguments speedTreatmentArguments = ParseSpeedTreatmentArguments(data, ref pos);
				if (speedTreatmentArguments == null)
				{
					return false;
				}
				maker.SpeedUpThreshold1 = speedTreatmentArguments.Threshold1;
				maker.SpeedUpThreshold2 = speedTreatmentArguments.Threshold2;
				maker.SpeedUpSpeedDelta1 = speedTreatmentArguments.SpeedDelta1;
				maker.SpeedUpSpeedDelta2 = speedTreatmentArguments.SpeedDelta2;
				maker.SpeedUpSpeedDelta3 = speedTreatmentArguments.SpeedDelta3;
				continue;
			}
			case "visible_speed":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseSpeedIntervals(data, ref pos, maker.VisibleSpeedIntervals))
				{
					Debug.LogError("Parse visible_speed fail!");
					return false;
				}
				continue;
			case "invisible_speed":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseSpeedIntervals(data, ref pos, maker.InvisibleSpeedIntervals))
				{
					return false;
				}
				continue;
			case "speed_treatment_matix":
				pos += token.Length;
				token = LookAhead(data, ref pos);
				if (token.Type != Token.TokenType.COLON)
				{
					return false;
				}
				pos += token.Length;
				if (!ParseSpeedTreatmentMatrix(data, ref pos, maker.speedTreatmentMatrix))
				{
					Debug.LogError("ParseSpeedTreatmentMatrix error");
					return false;
				}
				continue;
			}
			string name = null;
			Token val = Token.InvalidToken;
			if (!ParseSimplePair(data, ref pos, ref name, ref val))
			{
				continue;
			}
			if (name == "refresh_time_length")
			{
				if (!val.IsNumber())
				{
					Debug.LogError(name + " is not a number!");
					return false;
				}
				maker.RefreshTimeLength = val.RealValue;
			}
			else if (name == "check_point_ratio")
			{
				if (!val.IsNumber())
				{
					Debug.LogError(name + " is not a number!");
					return false;
				}
				maker.CheckPointRatio = val.RealValue;
			}
			else if (text == "final_rush_distance")
			{
				if (!val.IsNumber())
				{
					Debug.LogError(name + " is not a number!");
					return false;
				}
				maker.FinalRushDistance = val.RealValue;
			}
			else if (text == "final_rush_speed_delta")
			{
				if (!val.IsNumber())
				{
					Debug.LogError(name + " is not a number!");
					return false;
				}
				maker.FinalRushSpeedDelta = val.RealValue;
			}
		}
		return true;
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

	private bool ParseSpeedIntervals(char[] data, ref int pos, FunMakerConfig.Interval[] intervals)
	{
		while (true)
		{
			Token token = LookAhead(data, ref pos);
			if (token.Type != Token.TokenType.NUM_INT)
			{
				break;
			}
			pos += token.Length;
			int numInt = token.NumInt;
			token = LookAhead(data, ref pos);
			if (token.Type != Token.TokenType.TO)
			{
				return false;
			}
			pos += token.Length;
			if (!ParseInterval(data, ref pos, out var interval))
			{
				return false;
			}
			intervals[numInt] = interval;
		}
		return true;
	}

	private bool ParseInterval(char[] data, ref int pos, out FunMakerConfig.Interval interval)
	{
		interval = new FunMakerConfig.Interval(0f, 0f);
		float num = 0f;
		float num2 = 0f;
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.ARRAY_BEGIN)
		{
			return false;
		}
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (!token.IsNumber())
		{
			return false;
		}
		num = token.RealValue;
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.COMMA)
		{
			return false;
		}
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (!token.IsNumber())
		{
			return false;
		}
		num2 = token.RealValue;
		pos += token.Length;
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.ARRAY_END)
		{
			return false;
		}
		pos += token.Length;
		interval = new FunMakerConfig.Interval(num, num2);
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

	private SpeedTreatmentArguments ParseSpeedTreatmentArguments(char[] data, ref int pos)
	{
		float v = 0f;
		float v2 = 0f;
		float v3 = 0f;
		float v4 = 0f;
		float v5 = 0f;
		SpeedTreatmentArguments speedTreatmentArguments = new SpeedTreatmentArguments();
		if (!ParseNumber(data, ref pos, ref v))
		{
			return null;
		}
		Token token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.TO)
		{
			return null;
		}
		pos += token.Length;
		if (!ParseNumber(data, ref pos, ref v3))
		{
			return null;
		}
		if (!ParseNumber(data, ref pos, ref v2))
		{
			return null;
		}
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.TO)
		{
			return null;
		}
		pos += token.Length;
		if (!ParseNumber(data, ref pos, ref v4))
		{
			return null;
		}
		token = LookAhead(data, ref pos);
		if (token.Type != Token.TokenType.TO)
		{
			return null;
		}
		pos += token.Length;
		if (!ParseNumber(data, ref pos, ref v5))
		{
			return null;
		}
		speedTreatmentArguments.Threshold1 = v;
		speedTreatmentArguments.Threshold2 = v2;
		speedTreatmentArguments.SpeedDelta1 = v3;
		speedTreatmentArguments.SpeedDelta2 = v4;
		speedTreatmentArguments.SpeedDelta3 = v5;
		return speedTreatmentArguments;
	}

	private bool ParseSpeedTreatmentMatrix(char[] data, ref int pos, IList<IList<FunMakerConfig.SpeedTreatment>> speedTreatmentMatrix)
	{
		while (true)
		{
			Token token = LookAhead(data, ref pos);
			if (token.Type != Token.TokenType.NUM_INT)
			{
				break;
			}
			pos += token.Length;
			token = LookAhead(data, ref pos);
			if (token.Type != Token.TokenType.COLON)
			{
				Debug.LogError("Not colon!");
				return false;
			}
			pos += token.Length;
			IList<FunMakerConfig.SpeedTreatment> list = new List<FunMakerConfig.SpeedTreatment>();
			speedTreatmentMatrix.Add(list);
			while (true)
			{
				token = LookAhead(data, ref pos);
				if (token.Type == Token.TokenType.NUM_INT || token.Type == Token.TokenType.END_OF_INPUT)
				{
					break;
				}
				if (token.Type == Token.TokenType.ID)
				{
					if (token.Text == "d")
					{
						token.Type = Token.TokenType.MINUS;
					}
					else if (token.Text == "u")
					{
						token.Type = Token.TokenType.PLUS;
					}
					else if (token.Text == "n")
					{
						token.Type = Token.TokenType.SHARP;
					}
					else if (token.Text == "z")
					{
						token.Type = Token.TokenType.EQUAL;
					}
				}
				if (!TokenType2SpeedTreatment(token.Type, out var treatment))
				{
					return false;
				}
				pos += token.Length;
				list.Add(treatment);
			}
		}
		return true;
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

	private static bool TokenType2SpeedTreatment(Token.TokenType type, out FunMakerConfig.SpeedTreatment treatment)
	{
		switch (type)
		{
		case Token.TokenType.PLUS:
			treatment = FunMakerConfig.SpeedTreatment.Up;
			return true;
		case Token.TokenType.MINUS:
			treatment = FunMakerConfig.SpeedTreatment.Down;
			return true;
		case Token.TokenType.SHARP:
			treatment = FunMakerConfig.SpeedTreatment.Normal;
			return true;
		case Token.TokenType.EQUAL:
			treatment = FunMakerConfig.SpeedTreatment.None;
			return true;
		default:
			Debug.LogError("Illegal token type: " + type);
			treatment = FunMakerConfig.SpeedTreatment.None;
			return false;
		}
	}
}
