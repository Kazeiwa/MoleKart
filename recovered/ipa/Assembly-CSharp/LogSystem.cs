using System.Collections.Generic;
using System.IO;

public class LogSystem
{
	private enum LOG_DEST
	{
		LOG_DEST_CONSOLE = 1,
		LOG_DEST_FILE,
		LOG_DEST_BOTH
	}

	private string[] logNames;

	private int[] logSequenceNO;

	public static string[] s_strWtiteBuffer;

	public static int LOG_LEVEL_EMERG;

	public static int LOG_LEVEL_ALERT;

	public static int LOG_LEVEL_CRIT;

	public static int LOG_LEVEL_ERROR;

	public static int LOG_LEVEL_WARING;

	public static int LOG_LEVEL_NOTICE;

	public static int LOG_LEVEL_INFO;

	public static int LOG_LEVEL_DEBUG;

	public static int LOG_LEVEL_MAX;

	public static int STR_BUFFER_SIZE;

	public static int SEQUENCE_NUMBER;

	public static int LOG_FILE_SIZE;

	public static string STR_ERROR;

	public static List<string> moduleLogFileNames;

	public static List<string> moduleLogFileBuffers;

	public static List<int> moduleLogSequenceNO;

	public static List<int> moduleLogLevelIndex;

	private static LogSystem gLog;

	private static bool s_bForceWrite;

	private int currentLogLevel;

	private string storeLogDir;

	private int logFileSize;

	private int logSeqSize;

	private string logFilePreName;

	public static LogSystem Instance
	{
		get
		{
			/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
		}
	}

	~LogSystem()
	{
	}

	private long GetFileSize(string filepath)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private bool checkFileFull(string file)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void InitLogDirInfo()
	{
	}

	private int LogInit(string dir, int lvl, int size, int maxfiles, string pre_name)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public static void LogInit()
	{
	}

	public static void LogShutdown()
	{
	}

	private string GetLogFileName(int levelId)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private string GetLogFileName(int levelId, string moduleName)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void SetSeqNO(int level, string moduleName)
	{
	}

	private static string GetStrSuffix()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void StreamWriteLog(string s, FileStream fs)
	{
	}

	private bool checkSeqNO(int level, string moduleName, string filename)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	private void WriteFile(string StrBuffer, string filename, bool recreatefile)
	{
	}

	private void WriteLog2File(int level, string StrBuffer)
	{
	}

	private void WriteLog2File(int level, string moduleName, string StrBuffer)
	{
	}

	public static void WriteLog(int level, string logString)
	{
	}

	public static void WriteLog(int level, string moduleName, string logString)
	{
	}

	public static void WriteLog(int level, string moduleName, string logString, bool bForce)
	{
	}

	public static void FlushAllLogBuffer()
	{
	}

	public static void SetLogWrite(bool b)
	{
	}
}
