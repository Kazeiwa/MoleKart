using System.IO;
using System.Xml;
using DataCrypto;

public class EncryptXMLDocument
{
	private static SymmetricMethod code = new SymmetricMethod();

	public static void SaveToEncryptXML(XmlDocument doc, string strFile)
	{
		string contents = code.Encrypto(doc.InnerXml);
		File.WriteAllText(strFile, contents);
	}

	public static XmlDocument Load(string strFile)
	{
		string source = File.ReadAllText(strFile);
		string text = code.Decrypto(source);
		XmlDocument xmlDocument = new XmlDocument();
		if (text == string.Empty)
		{
			return xmlDocument;
		}
		xmlDocument.LoadXml(text);
		return xmlDocument;
	}
}
