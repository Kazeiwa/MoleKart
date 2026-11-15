using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class LogSystem
{
	private enum LOG_DEST
	{
		LOG_DEST_CONSOLE = 1,
		LOG_DEST_FILE,
		LOG_DEST_BOTH
	}

	private string[] logNames = new string[8] { "emerg", "alert", "crit", "error", "warn", "notice", "info", "debug" };

	private int[] logSequenceNO = new int[8];

	public static string[] s_strWtiteBuffer = new string[8]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty
	};

	public static int LOG_LEVEL_EMERG = 0;

	public static int LOG_LEVEL_ALERT = LOG_LEVEL_EMERG + 1;

	public static int LOG_LEVEL_CRIT = LOG_LEVEL_ALERT + 1;

	public static int LOG_LEVEL_ERROR = LOG_LEVEL_CRIT + 1;

	public static int LOG_LEVEL_WARING = LOG_LEVEL_ERROR + 1;

	public static int LOG_LEVEL_NOTICE = LOG_LEVEL_WARING + 1;

	public static int LOG_LEVEL_INFO = LOG_LEVEL_NOTICE + 1;

	public static int LOG_LEVEL_DEBUG = LOG_LEVEL_INFO + 1;

	public static int LOG_LEVEL_MAX = LOG_LEVEL_DEBUG + 1;

	public static int STR_BUFFER_SIZE = 256;

	public static int SEQUENCE_NUMBER = 3;

	public static int LOG_FILE_SIZE = 10485760;

	public static string STR_ERROR = "__error";

	public static List<string> moduleLogFileNames = new List<string>();

	public static List<string> moduleLogFileBuffers = new List<string>();

	public static List<int> moduleLogSequenceNO = new List<int>();

	public static List<int> moduleLogLevelIndex = new List<int>();

	private static LogSystem gLog;

	private static bool s_bForceWrite = false;

	private int currentLogLevel = LOG_LEVEL_DEBUG;

	private string storeLogDir;

	private int logFileSize = LOG_FILE_SIZE;

	private int logSeqSize = SEQUENCE_NUMBER;

	private string logFilePreName = string.Empty;

	public static LogSystem Instance
	{
		get
		{
			if (gLog == null)
			{
				gLog = new LogSystem();
			}
			return gLog;
		}
	}

	public LogSystem()
	{
		LogInit();
	}

	~LogSystem()
	{
		Debug.Log("~LogSystem:()");
	}

	private long GetFileSize(string filepath)
	{
		long num = 0L;
		using FileStream fileStream = new FileStream(filepath, FileMode.Open);
		return fileStream.Length;
	}

	private bool checkFileFull(string file)
	{
		string filepath = Path.Combine(storeLogDir, file);
		if (GetFileSize(filepath) > logFileSize)
		{
			return true;
		}
		return false;
	}

	private void InitLogDirInfo()
	{
		if (!Directory.Exists(storeLogDir))
		{
			return;
		}
		string[] files = Directory.GetFiles(storeLogDir);
		string[] array = files;
		foreach (string text in array)
		{
			int num = text.LastIndexOf('_');
			string item = text.Substring(num + 1);
			int num2 = Convert.ToInt32(text.Substring(num - 1, 1));
			if (!moduleLogFileNames.Contains(item))
			{
				moduleLogFileNames.Add(item);
				moduleLogFileBuffers.Add(string.Empty);
				if (!checkFileFull(text))
				{
					moduleLogSequenceNO.Add(num2);
				}
				else
				{
					moduleLogSequenceNO.Add(0);
				}
			}
			else if (moduleLogFileNames.Contains(item) && !checkFileFull(text))
			{
				int num3 = moduleLogFileNames.IndexOf(item, 0);
				if (num3 != -1)
				{
					moduleLogSequenceNO[num3] = num2;
				}
			}
		}
	}

	private int LogInit(string dir, int lvl, int size, int maxfiles, string pre_name)
	{
		if (dir == null || dir == string.Empty)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			string text = Path.Combine(currentDirectory, "Log");
			storeLogDir = text;
			Console.WriteLine("Dir :" + storeLogDir);
		}
		else
		{
			storeLogDir = dir;
		}
		currentLogLevel = lvl;
		if (size == -1)
		{
			logFileSize = LOG_FILE_SIZE;
		}
		else
		{
			logFileSize = size;
		}
		if (maxfiles == -1)
		{
			logSeqSize = SEQUENCE_NUMBER;
		}
		else
		{
			logSeqSize = maxfiles;
		}
		logFilePreName = pre_name;
		InitLogDirInfo();
		return 0;
	}

	public static void LogInit()
	{
		Debug.Log("LogSystem:LogInit()");
		Instance.LogInit(null, LOG_LEVEL_DEBUG, -1, -1, "TaoMee");
	}

	public static void LogShutdown()
	{
		FlushAllLogBuffer();
	}

	private string GetLogFileName(int levelId)
	{
		if (levelId < 0 || levelId > LOG_LEVEL_MAX - 1)
		{
			return null;
		}
		return logNames[levelId] + logSequenceNO[levelId];
	}

	private string GetLogFileName(int levelId, string moduleName)
	{
		if (levelId < 0 || levelId > LOG_LEVEL_MAX - 1)
		{
			return STR_ERROR;
		}
		int num = moduleLogFileNames.IndexOf(moduleName, 0);
		if (num == -1)
		{
			return STR_ERROR;
		}
		return logNames[levelId] + moduleLogSequenceNO[num] + "_" + moduleName;
	}

	private void SetSeqNO(int level, string moduleName)
	{
		int num = moduleLogFileNames.IndexOf(moduleName, 0);
		if (num != -1)
		{
			moduleLogSequenceNO[num] = (moduleLogSequenceNO[num] + 1) % logSeqSize;
		}
	}

	private static string GetStrSuffix()
	{
		return " \r\n";
	}

	private void StreamWriteLog(string s, FileStream fs)
	{
		using StreamWriter streamWriter = new StreamWriter(fs, Encoding.GetEncoding("gb2312"));
		streamWriter.Write(s);
		streamWriter.Write("\r\n");
		streamWriter.Write("\r\n");
		streamWriter.Flush();
	}

	private bool checkSeqNO(int level, string moduleName, string filename)
	{
		if (Directory.Exists(storeLogDir))
		{
			string text = Path.Combine(storeLogDir, logFilePreName + "_" + filename);
			if (File.Exists(text) && GetFileSize(text) > logFileSize)
			{
				SetSeqNO(level, moduleName);
				return true;
			}
		}
		return false;
	}

	private void WriteFile(string StrBuffer, string filename, bool recreatefile)
	{
		string text = storeLogDir;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		text = Path.Combine(text, logFilePreName + "_" + filename);
		FileStream fileStream = null;
		if (!File.Exists(text) || recreatefile)
		{
			using (fileStream = File.Create(text))
			{
				StreamWriteLog(StrBuffer, fileStream);
				return;
			}
		}
		using (fileStream = File.Open(text, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
		{
			StreamWriteLog(StrBuffer, fileStream);
		}
	}

	private void WriteLog2File(int level, string StrBuffer)
	{
		WriteFile(StrBuffer, GetLogFileName(level), recreatefile: false);
	}

	private void WriteLog2File(int level, string moduleName, string StrBuffer)
	{
		string logFileName = GetLogFileName(level, moduleName);
		bool recreatefile = checkSeqNO(level, moduleName, logFileName);
		WriteFile(StrBuffer, GetLogFileName(level, moduleName), recreatefile);
	}

	public static void WriteLog(int level, string logString)
	{
		if (level < 0 || level > LOG_LEVEL_MAX - 1)
		{
			Debug.Log("!!!Error log level!!!");
		}
		else if (level <= gLog.currentLogLevel)
		{
			s_strWtiteBuffer[level] = s_strWtiteBuffer[level] + logString + GetStrSuffix();
			if (s_strWtiteBuffer[level].Length > STR_BUFFER_SIZE || s_bForceWrite)
			{
				gLog.WriteLog2File(level, s_strWtiteBuffer[level]);
				s_strWtiteBuffer[level] = null;
			}
		}
	}

	public static void WriteLog(int level, string moduleName, string logString)
	{
		WriteLog(level, moduleName, logString, bForce: false);
	}

	public static void WriteLog(int level, string moduleName, string logString, bool bForce)
	{
		if (level < 0 || level > LOG_LEVEL_MAX - 1)
		{
			Debug.Log("!!!Error log level!!!");
		}
		else
		{
			if (level > gLog.currentLogLevel)
			{
				return;
			}
			if (!moduleLogFileNames.Contains(moduleName) && !moduleLogLevelIndex.Contains(level))
			{
				moduleLogFileNames.Add(moduleName);
				string text = "--------------------------------------------------------------" + GetStrSuffix();
				text = text + moduleName + " Module " + GetStrSuffix();
				text = text + "--------------------------------------------------------------" + GetStrSuffix();
				moduleLogFileBuffers.Add(string.Empty);
				moduleLogSequenceNO.Add(0);
				moduleLogLevelIndex.Add(level);
				moduleLogFileBuffers[moduleLogFileBuffers.Count - 1] = text;
			}
			int num = moduleLogFileNames.IndexOf(moduleName, 0);
			if (num != -1)
			{
				moduleLogFileBuffers[num] = moduleLogFileBuffers[num] + logString + GetStrSuffix();
				if (moduleLogFileBuffers[num].Length > STR_BUFFER_SIZE || bForce || s_bForceWrite)
				{
					gLog.WriteLog2File(level, moduleName, moduleLogFileBuffers[num]);
					moduleLogFileBuffers[num] = string.Empty;
				}
			}
		}
	}

	public static void FlushAllLogBuffer()
	{
		if (moduleLogFileNames.Count != 0)
		{
			SetLogWrite(b: true);
			int count = moduleLogFileNames.Count;
			for (int i = 0; i < count; i++)
			{
				WriteLog(moduleLogLevelIndex[i], moduleLogFileNames[i], string.Empty);
			}
			SetLogWrite(b: false);
		}
	}

	public static void SetLogWrite(bool b)
	{
		s_bForceWrite = b;
	}
}
