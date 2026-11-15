using System;
using UnityEngine;

public class NiLog
{
	public static int iDebug;

	private static string ms_strLogMessage = string.Empty;

	private static string ms_strLogMessageX = string.Empty;

	private static int m_iLines;

	private static int m_iLinesX;

	public static string strMessage => ms_strLogMessage + ms_strLogMessageX;

	public static void Log(string strNewLine)
	{
		Log(strNewLine, 1);
	}

	public static void Log(string strNewLine, int iLevel)
	{
		if ((iDebug & iLevel) == iLevel)
		{
			strNewLine += "\r\n";
			ms_strLogMessage += strNewLine;
			m_iLines++;
		}
	}

	public static void LogX(string strNewLine)
	{
		LogX(strNewLine, 1);
	}

	public static void LogX(string strNewLine, int iLevel)
	{
		if ((iDebug & iLevel) == iLevel)
		{
			strNewLine += "\r\n";
			ms_strLogMessageX += strNewLine;
			m_iLinesX++;
		}
	}

	public static void NIASSERT(bool condition)
	{
		if (!condition)
		{
			if (iDebug > 0)
			{
				Debug.Break();
			}
			throw new Exception("Condition not true.");
		}
	}

	public static void Clear()
	{
		m_iLines = 0;
		ms_strLogMessage = string.Empty;
	}

	public static void ClearX()
	{
		m_iLinesX = 0;
		ms_strLogMessageX = string.Empty;
	}

	public static int GetLinesNumber()
	{
		return m_iLines;
	}

	public static int GetLinesNumberX()
	{
		return m_iLinesX;
	}
}
