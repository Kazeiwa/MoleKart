using System.Xml;
using UnityEngine;

public class ParticleLoader
{
	public Object LoadParticle(string particleName)
	{
		return Resources.Load(particleName);
	}

	private void ParseXml(string xmlFile)
	{
		Debug.Log("[ParticleLoader] -> ParseXml -> " + xmlFile);
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(xmlFile);
	}
}
