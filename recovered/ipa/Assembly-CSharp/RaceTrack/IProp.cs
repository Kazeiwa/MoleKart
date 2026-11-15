namespace RaceTrack;

public interface IProp : IAgent
{
	bool IsPickable { get; set; }

	bool IsDangerous { get; set; }

	bool IsMoving { get; set; }
}
