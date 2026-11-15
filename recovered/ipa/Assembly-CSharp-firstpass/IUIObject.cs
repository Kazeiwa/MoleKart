using UnityEngine;

public interface IUIObject
{
	bool controlIsEnabled { get; set; }

	bool DetargetOnDisable { get; set; }

	IUIContainer Container { get; set; }

	GameObject gameObject { get; }

	Transform transform { get; }

	bool RequestContainership(IUIContainer cont);

	bool GotFocus();

	void OnInput(POINTER_INFO ptr);

	void SetInputDelegate(EZInputDelegate del);

	void AddInputDelegate(EZInputDelegate del);

	void RemoveInputDelegate(EZInputDelegate del);

	void SetValueChangedDelegate(EZValueChangedDelegate del);

	void AddValueChangedDelegate(EZValueChangedDelegate del);

	void RemoveValueChangedDelegate(EZValueChangedDelegate del);
}
