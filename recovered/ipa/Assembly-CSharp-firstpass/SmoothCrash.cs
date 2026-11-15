using System;
using UnityEngine;

public class SmoothCrash : EZAnimation
{
	protected const float PIx2 = (float)Math.PI * 2f;

	protected Vector3 start;

	protected Vector3 magnitude;

	protected Vector3 oscillations;

	protected GameObject subject;

	protected Transform subTrans;

	protected Vector3 temp;

	protected float factor;

	protected float invFactor;

	public override object GetSubject()
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override void _end()
	{
	}

	protected override void WaitDone()
	{
	}

	protected override void DoAnim()
	{
	}

	public static SmoothCrash Do(GameObject sub, Vector3 mag, Vector3 oscill, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public override bool Start(GameObject sub, AnimParams parms)
	{
		/*Error: Method body consists only of 'ret', but nothing is being returned. Decompiled assembly might be a reference assembly.*/;
	}

	public void Start(GameObject sub, Vector3 mag, Vector3 oscill, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}

	public void Start(GameObject sub, Vector3 begin, Vector3 mag, Vector3 oscill, float dur, float delay, CompletionDelegate startDel, CompletionDelegate del)
	{
	}
}
