namespace FrameWork;

public interface IModule
{
	enmModuleID GetID();

	void Init(IFrameWork iFrameHost);

	void Update(float fCurTime);

	void FixedUpdate();

	void OnGUI();

	void Uninit();
}
