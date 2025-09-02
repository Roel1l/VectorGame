using Microsoft.Xna.Framework;
using System;

namespace VectorGame.Objects.BirdObject.States;

public class RandomSteeringState : BirdState
{
    private const float SteeringIntervalInSeconds = 3f;
    private const float TurningSpeed = 1.5f;

    private Vector2 _targetDirection;
    private float _steeringTimer;

    public RandomSteeringState()
    {
        _steeringTimer = SteeringIntervalInSeconds;
        SetNewTargetDirection();
    }

    public override void Update(Bird bird, float elapsedTimeInSeconds)
    {
        Steer(bird, elapsedTimeInSeconds);
        Move(bird, elapsedTimeInSeconds);
    }

    private void SetNewTargetDirection()
    {
        var randomAngle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
        _targetDirection = new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
    }

    private void Steer(Bird bird, float elapsedTimeInSeconds)
    {
        _steeringTimer -= elapsedTimeInSeconds;
        if (_steeringTimer <= 0)
        {
            SetNewTargetDirection();
            _steeringTimer = SteeringIntervalInSeconds;
        }

        bird.Direction = Vector2.Lerp(bird.Direction, _targetDirection, TurningSpeed * elapsedTimeInSeconds);
        bird.Direction.Normalize();
    }

    private static void Move(Bird bird, float elapsedTimeInSeconds)
    {
        var pixelsToMove = bird.Speed * elapsedTimeInSeconds;
        bird.Position += bird.Direction * pixelsToMove;
    }
}
