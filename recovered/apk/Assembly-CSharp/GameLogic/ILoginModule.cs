using FrameWork;

namespace GameLogic;

public interface ILoginModule : IModule
{
	bool Login(uint userid, string password);

	bool Login(string email, string password);

	bool LoginSession(byte[] session);

	void Exit();
}
