public abstract class ISimulator : ABCmdTarget
{
	protected float m_fRotX;

	protected float m_fRotY;

	protected float m_fRotZ;

	public float GetRotAngleX()
	{
		return m_fRotX;
	}

	public float GetRotAngleY()
	{
		return m_fRotY;
	}

	public float GetRotAngleZ()
	{
		return m_fRotZ;
	}

	public abstract void Update(float fTickTime);

	public virtual void OnDrawGizmos()
	{
	}
}
