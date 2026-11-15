public interface IRaceMode
{
	float LastGameTime { get; set; }

	void Init(SceneRace scene);

	void Uninit();

	void UpdateMode();

	void UpdateUI();

	int TotalLap();

	void RaceOver(bool over);

	void ReStartGame();

	float TotalTime();

	void BeginCountTime();

	void FinishRace();
}
