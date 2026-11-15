using UnityEngine;

public class FXShadow : MonoBehaviour
{
	private int _Layer;

	private float _fwdScale = 1f;

	private float _sidScale = 1f;

	private bool _bWorldSpace = true;

	private Vector4 _uvShadow;

	private bool GetProjected(Vector3 pt, out Vector3 ptHit)
	{
		if (Physics.Raycast(pt, -Vector3.up, out var hitInfo, 10000f, _Layer))
		{
			ptHit = hitInfo.point;
			return true;
		}
		ptHit = Vector3.zero;
		return false;
	}

	public void Init(EnmCollisionLayer Layer, FXShadowType st, float fwdScale, float sidScale, bool bWorldSpace)
	{
		_Layer = 1 << (int)Layer;
		_fwdScale = fwdScale;
		_sidScale = sidScale;
		_bWorldSpace = bWorldSpace;
		switch (st)
		{
		case FXShadowType.kSTDot:
			_uvShadow = new Vector4(16f, 16f, 176f, 176f);
			break;
		case FXShadowType.kSTSquare:
			_uvShadow = new Vector4(192f, 0f, 192f, 192f);
			break;
		}
	}

	private void Update()
	{
		if (!base.gameObject.active)
		{
			return;
		}
		MeshRenderer component = GetComponent<MeshRenderer>();
		if (null != component && !component.enabled)
		{
			return;
		}
		Transform transform = base.gameObject.transform;
		if ((EffectManager.sceneCamera.transform.position - transform.position).sqrMagnitude > 1600f)
		{
			return;
		}
		Vector3 vector = transform.position + Vector3.up * 10f;
		Vector3 pt = vector;
		Vector3 ptHit = vector;
		if (!GetProjected(pt, out ptHit))
		{
			return;
		}
		Vector3 zero = Vector3.zero;
		Vector3 zero2 = Vector3.zero;
		Vector3 zero3 = Vector3.zero;
		Vector3 zero4 = Vector3.zero;
		Vector3 ptHit2 = zero;
		Vector3 ptHit3 = zero2;
		Vector3 ptHit4 = zero3;
		Vector3 ptHit5 = zero4;
		Vector3 forward = Vector3.forward;
		Vector3 right = Vector3.right;
		if (!_bWorldSpace)
		{
			forward = transform.forward;
			right = transform.right;
		}
		zero = vector + forward * _fwdScale + right * _sidScale;
		bool projected = GetProjected(zero, out ptHit2);
		if (projected)
		{
			zero2 = vector + forward * _fwdScale - right * _sidScale;
			projected = GetProjected(zero2, out ptHit3);
			if (projected)
			{
				zero3 = vector - forward * _fwdScale - right * _sidScale;
				projected = GetProjected(zero3, out ptHit4);
				if (projected)
				{
					zero4 = vector - forward * _fwdScale + right * _sidScale;
					projected = GetProjected(zero4, out ptHit5);
				}
			}
		}
		if (projected)
		{
			float num = 0f;
			float num2 = transform.position.y - ptHit.y;
			if (num2 > 0f)
			{
				num = num2 / 4f;
				if (num > 1f)
				{
					num = 1f;
				}
			}
			float num3 = Mathf.Lerp(0.85f, 0f, num);
			if (num3 > 0f)
			{
				Vector3 vector2 = Vector3.up * 0.02f;
				EffectManager.fxMgrShadow.AddShadow(ptHit2 + vector2, ptHit3 + vector2, ptHit4 + vector2, ptHit5 + vector2, _uvShadow, new Color(1f, 1f, 1f, num3));
			}
			return;
		}
		float num4 = _fwdScale * 0.5f;
		float num5 = _sidScale * 0.5f;
		zero = vector + forward * num4 + right * num5;
		projected = GetProjected(zero, out ptHit2);
		if (projected)
		{
			zero2 = vector + forward * num4 - right * num5;
			projected = GetProjected(zero2, out ptHit3);
			if (projected)
			{
				zero3 = vector - forward * num4 - right * num5;
				projected = GetProjected(zero3, out ptHit4);
				if (projected)
				{
					zero4 = vector - forward * num4 + right * num5;
					projected = GetProjected(zero4, out ptHit5);
				}
			}
		}
		if (!projected)
		{
			return;
		}
		float num6 = Mathf.Sqrt(_fwdScale * _fwdScale + _sidScale * _sidScale);
		ptHit2 = ptHit + (ptHit2 - ptHit).normalized * num6;
		ptHit3 = ptHit + (ptHit3 - ptHit).normalized * num6;
		ptHit4 = ptHit + (ptHit4 - ptHit).normalized * num6;
		ptHit5 = ptHit + (ptHit5 - ptHit).normalized * num6;
		float num7 = 0f;
		float num8 = transform.position.y - ptHit.y;
		if (num8 > 0f)
		{
			num7 = num8 / 4f;
			if (num7 > 1f)
			{
				num7 = 1f;
			}
		}
		float num9 = Mathf.Lerp(0.85f, 0f, num7);
		if (num9 > 0f)
		{
			Vector3 vector3 = Vector3.up * 0.02f;
			EffectManager.fxMgrShadow.AddShadow(ptHit2 + vector3, ptHit3 + vector3, ptHit4 + vector3, ptHit5 + vector3, _uvShadow, new Color(1f, 1f, 1f, num9));
		}
	}
}
