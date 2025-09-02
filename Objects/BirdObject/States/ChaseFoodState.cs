using Microsoft.Xna.Framework;
using System;

namespace VectorGame.Objects.BirdObject.States;

public class ChaseFoodState : BirdState
{
    private const float SteeringIntervalInSeconds = 3f;
    private const float TurningSpeed = 1.5f;

    private Vector2 _targetDirection;
    private float _steeringTimer;

    public ChaseFoodState()
    {
    }

    public override void Update(Bird bird, float elapsedTimeInSeconds)
    {
    }
}
