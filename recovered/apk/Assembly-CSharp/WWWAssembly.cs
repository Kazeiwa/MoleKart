using System.Reflection;

public class WWWAssembly
{
	private string m_URL;

	private Assembly m_Assembly;

	public string URL => m_URL;

	public Assembly Assembly => m_Assembly;

	public WWWAssembly(string url, Assembly assembly)
	{
		m_URL = url;
		m_Assembly = assembly;
	}
}
