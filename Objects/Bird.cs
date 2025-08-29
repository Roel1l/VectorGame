using Microsoft.Xna.Framework;
using System;

namespace VectorGame.Objects;

public class Bird : GameObject
{
    private const float PixelsMovedPerSecond = 100f;
    private const float SteeringIntervalInSeconds = 3f;

    protected override string TextureName => "bird";

    private Vector2 _targetDirection;
    private float _steeringTimer;
    private Vector2 _actualDirection;

    public float TurningSpeed { get; set; } = 1.5f;

    private GraphicsDeviceManager _graphics;

    public override void Initialize(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;

        Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        FrameCount = 2;
        FramesPerSecond = 2;
        TimePerFrame = (float)1 / FramesPerSecond;

        _steeringTimer = SteeringIntervalInSeconds;
        SetNewTargetDirection();

        _actualDirection = _targetDirection;
    }

    private void SetNewTargetDirection()
    {
        var randomAngle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
        _targetDirection = new Vector2((float)Math.Cos(randomAngle), (float)Math.Sin(randomAngle));
    }

    private void LoopScreen()
    {
        var position = Position;

        if (position.X > _graphics.PreferredBackBufferWidth)
        {
            position.X = 0;
        }
        else if (position.X < 0)
        {
            position.X = _graphics.PreferredBackBufferWidth;
        }

        if (position.Y > _graphics.PreferredBackBufferHeight)
        {
            position.Y = 0;
        }
        else if (position.Y < 0)
        {
            position.Y = _graphics.PreferredBackBufferHeight;
        }

        Position = position;
    }

    public override void Update(GameTime gameTime)
    {
        float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Steer
        _steeringTimer -= elapsed;
        if (_steeringTimer <= 0)
        {
            SetNewTargetDirection();
            _steeringTimer = SteeringIntervalInSeconds;
        }

        _actualDirection = Vector2.Lerp(_actualDirection, _targetDirection, TurningSpeed * elapsed);
        _actualDirection.Normalize();
        
        // Move
        float pixelsToMove = PixelsMovedPerSecond * elapsed;
        Position += _actualDirection * pixelsToMove;

        LoopScreen();

        // Rotate to face direction of movement
        if (_actualDirection != Vector2.Zero)
        {
            float angle = (float)Math.Atan2(_actualDirection.Y, _actualDirection.X);
            Rotation = angle + MathHelper.PiOver2;
        }

        base.Update(gameTime);
    }
}
