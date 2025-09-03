using Microsoft.Xna.Framework;
using System;
using System.Linq;
using VectorGame.Objects.FoodObject;

namespace VectorGame.Objects.BirdObject.States;

public class RandomSteeringState : BirdState
{
    private const float SteeringIntervalInSeconds = 3f;

    private Vector2 _targetDirection;
    private float _steeringTimer;

    public RandomSteeringState(Bird bird) : base(bird)
    {
        _steeringTimer = SteeringIntervalInSeconds;
        SetNewTargetDirection();
    }

    public override void Update(float elapsedTimeInSeconds)
    {
        if (TryTransitionToChaseFoodState())
        {
            return;
        }

        Steer(elapsedTimeInSeconds);
        Move(elapsedTimeInSeconds);
    }

    private void SetNewTargetDirection()
    {
        var randomAngle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
        _targetDirection = new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
    }

    private void Steer(float elapsedTimeInSeconds)
    {
        _steeringTimer -= elapsedTimeInSeconds;
        if (_steeringTimer <= 0)
        {
            SetNewTargetDirection();
            _steeringTimer = SteeringIntervalInSeconds;
        }

        Vector2 desiredDirection = _targetDirection;
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

    private bool TryTransitionToChaseFoodState()
    {
        if (Bird.GameObjectManager.GetAll<Food>().FirstOrDefault() is Food food)
        {
            Bird.State = new ChaseFoodState(Bird, food.Id);

            return true;
        }

        return false;
    }
}
