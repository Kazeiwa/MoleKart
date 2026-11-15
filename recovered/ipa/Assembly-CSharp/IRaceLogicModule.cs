public interface IRaceLogicModule
{
	void CreateGame(int iSceneID);

	void DestroyGame();

	RacingType GetCurrentRaceType();

	void SetCurrentRaceType(RacingType eType);

	bool IsOnlineMode();

	void ServerIsReady();

	void StopServerLoop();

	void StartServerLoop();

	bool YouAreServer();

	void ServerIsFinishRace();

	IRaceMode GetCurrentRaceMode();
}
