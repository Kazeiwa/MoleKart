using System.Xml;
using UnityEngine;

public class ShaderLoader
{
	public Object LoadShader(string shaderName)
	{
		return Shader.Find(shaderName);
	}

	public Object LoadMaterial(string materailName)
	{
		return new Material(Shader.Find(materailName));
	}

	public Object LoadTexture(string textureName)
	{
		return Resources.Load(textureName);
	}

	private void ParseXml(string xmlFile)
	{
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xmlFile);
		if (xmlDocument == null)
		{
		}
	}
}
