namespace VectorGame.Objects.BirdObject.States;

public abstract class BirdState
{
    protected Bird Bird { get; private set; }

    protected BirdState(Bird bird)
    {
        Bird = bird;
    }

    public abstract void Update(float elapsedTimeInSeconds);
}
