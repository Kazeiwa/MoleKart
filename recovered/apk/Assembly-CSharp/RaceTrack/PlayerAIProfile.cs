namespace RaceTrack;

public class PlayerAIProfile
{
	public enum Trait
	{
		T0,
		T1,
		T2,
		T3,
		Count
	}

	public Trait StartTrait { get; set; }

	public Trait PathTrait { get; set; }

	public Trait PropTrait { get; set; }

	public Trait CollisionTrait { get; set; }

	public PlayerAIProfile()
	{
		StartTrait = Trait.T0;
		PathTrait = Trait.T0;
		PropTrait = Trait.T0;
		CollisionTrait = Trait.T0;
	}

	public PlayerAIProfile(Trait startTrait, Trait pathTrait, Trait propTrait, Trait collisionTrait)
	{
		StartTrait = startTrait;
		PathTrait = pathTrait;
		PropTrait = propTrait;
		CollisionTrait = collisionTrait;
	}

	public PlayerAIProfile(Trait trait)
	{
		StartTrait = trait;
		PathTrait = trait;
		PropTrait = trait;
		CollisionTrait = trait;
	}
}
