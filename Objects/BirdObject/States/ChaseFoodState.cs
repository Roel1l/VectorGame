using Microsoft.Xna.Framework;
using VectorGame.Objects.FoodObject;

namespace VectorGame.Objects.BirdObject.States;

public class ChaseFoodState : BirdState
{
    private readonly int _targetFoodId;
    private const float EatingDistance = 5.0f;

    public ChaseFoodState(Bird bird, int targetFoodId) : base(bird)
    {
        _targetFoodId = targetFoodId;
    }

    public override void Update(float elapsedTimeInSeconds)
    {
        if (Bird.GameObjectManager.GetById(_targetFoodId) is not Food targetFood)
        {
            Bird.State = new RandomSteeringState(Bird);
            return;
        }

        if (CheckForCollisionAndEat(targetFood))
        {
            return;
        }

        Steer(targetFood);
        Move(elapsedTimeInSeconds);

    }

    private void Steer(Food food)
    {
        Vector2 desiredDirection = food.Position - Bird.Position;
        if (desiredDirection != Vector2.Zero)
        {
            desiredDirection.Normalize();
        }

        Vector2 steeringForce = desiredDirection - Bird.Direction;

        if (steeringForce.Length() > Bird.TurningSpeed)
        {
            steeringForce.Normalize();
            steeringForce *= Bird.TurningSpeed;
        }

        Bird.Direction += steeringForce;
        if (Bird.Direction != Vector2.Zero)
        {
            Bird.Direction.Normalize();
        }
    }
    private void Move(float elapsedTimeInSeconds)
    {
        var pixelsToMove = Bird.Speed * elapsedTimeInSeconds;
        Bird.Position += Bird.Direction * pixelsToMove;
    }

    private bool CheckForCollisionAndEat(Food food)
    {
        var distance = Vector2.Distance(Bird.Position, food.Position);

        if (distance < EatingDistance)
        {
            Bird.GameObjectManager.Remove(food);
            Bird.State = new RandomSteeringState(Bird);

            return true;
        }
        return false;
    }
}
