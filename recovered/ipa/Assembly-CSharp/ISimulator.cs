public abstract class ISimulator : ABCmdTarget
{
	protected float m_fRotX;

	protected float m_fRotY;

	protected float m_fRotZ;

	public float GetRotAngleX()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetRotAngleY()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public float GetRotAngleZ()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public abstract void Update(float fTickTime);

	public virtual void OnDrawGizmos()
	{
	}
}
