using UnityEngine;

public class EZScreenPlacementMirror
{
	public Vector3 screenPos;

	public EZScreenPlacement.RelativeTo relativeTo = new EZScreenPlacement.RelativeTo();

	public Transform relativeObject;

	public virtual void Mirror(EZScreenPlacement sp)
	{
		screenPos = sp.screenPos;
		relativeTo.Copy(sp.relativeTo);
		relativeObject = sp.relativeObject;
	}

	public virtual bool Validate(EZScreenPlacement sp)
	{
		if (sp.relativeTo.horizontal != EZScreenPlacement.HORIZONTAL_ALIGN.OBJECT && sp.relativeTo.vertical != EZScreenPlacement.VERTICAL_ALIGN.OBJECT)
		{
			sp.relativeObject = null;
		}
		if (sp.relativeObject != null && !EZScreenPlacement.TestDepenency(sp))
		{
			Debug.LogError("ERROR: The Relative Object you recently assigned on \"" + sp.name + "\" which points to \"" + sp.relativeObject.name + "\" would create a circular dependency.  Please check your placement dependencies to resolve this.");
			sp.relativeObject = null;
		}
		return true;
	}

	public virtual bool DidChange(EZScreenPlacement sp)
	{
		if (screenPos != sp.screenPos)
		{
			return true;
		}
		if (!relativeTo.Equals(sp.relativeTo))
		{
			return true;
		}
		if (relativeObject != sp.relativeObject)
		{
			return true;
		}
		return false;
	}
}
