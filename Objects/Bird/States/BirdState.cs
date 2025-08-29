namespace VectorGame.Objects.Bird.States;

public abstract class BirdState
{
    public abstract void Update(Bird bird, float elapsedTimeInSeconds);
}
