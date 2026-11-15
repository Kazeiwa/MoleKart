using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DataCrypto;

public class SymmetricMethod
{
	private SymmetricAlgorithm mobjCryptoService;

	private string Key;

	public SymmetricMethod()
	{
		mobjCryptoService = new RijndaelManaged();
		Key = "Guz(%&hj7x89H$yuBI0456FtmaT5&fvHUFCy76*h%(HilJ$lhj!y6&(*jkP87jH7";
	}

	private byte[] GetLegalKey()
	{
		string text = Key;
		mobjCryptoService.GenerateKey();
		byte[] key = mobjCryptoService.Key;
		int num = key.Length;
		if (text.Length > num)
		{
			text = text.Substring(0, num);
		}
		else if (text.Length < num)
		{
			text = text.PadRight(num, ' ');
		}
		return Encoding.ASCII.GetBytes(text);
	}

	private byte[] GetLegalIV()
	{
		string text = "E4ghj*Ghg7!rNIfb&95GUY86GfghUb#er57HBh(u%g6HJ($jhWk7&!hg4ui%$hjk";
		mobjCryptoService.GenerateIV();
		byte[] iV = mobjCryptoService.IV;
		int num = iV.Length;
		if (text.Length > num)
		{
			text = text.Substring(0, num);
		}
		else if (text.Length < num)
		{
			text = text.PadRight(num, ' ');
		}
		return Encoding.ASCII.GetBytes(text);
	}

	public string Encrypto(string Source)
	{
		if (Source.Length <= 0)
		{
			return string.Empty;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(Source);
		MemoryStream memoryStream = new MemoryStream();
		mobjCryptoService.Key = GetLegalKey();
		mobjCryptoService.IV = GetLegalIV();
		ICryptoTransform transform = mobjCryptoService.CreateEncryptor();
		CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
		cryptoStream.Write(bytes, 0, bytes.Length);
		cryptoStream.FlushFinalBlock();
		memoryStream.Close();
		byte[] inArray = memoryStream.ToArray();
		return Convert.ToBase64String(inArray);
	}

	public string Decrypto(string Source)
	{
		if (Source.Length <= 0)
		{
			return string.Empty;
		}
		try
		{
			byte[] array = Convert.FromBase64String(Source);
			MemoryStream stream = new MemoryStream(array, 0, array.Length);
			mobjCryptoService.Key = GetLegalKey();
			mobjCryptoService.IV = GetLegalIV();
			ICryptoTransform transform = mobjCryptoService.CreateDecryptor();
			CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
			StreamReader streamReader = new StreamReader(stream2);
			return streamReader.ReadToEnd();
		}
		catch (Exception)
		{
			Debug.Break();
			Debug.Log("Decrypto error!" + Source);
			return string.Empty;
		}
	}
}
