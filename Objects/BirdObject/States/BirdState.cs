namespace VectorGame.Objects.BirdObject.States;

public abstract class BirdState
{
    public abstract void Update(Bird bird, float elapsedTimeInSeconds);
}
