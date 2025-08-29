using Microsoft.Xna.Framework;
using System;
using VectorGame.Objects.Bird.States;

namespace VectorGame.Objects.Bird;

public class Bird : GameObject
{
    public float Speed { get; set; } = 100f;

    protected override string TextureName => "bird";

    public Vector2 Direction { get; set; }

    private GraphicsDeviceManager _graphics;

    private readonly BirdState _state = new RandomSteeringState();

    public override void Initialize(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;

        Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        FrameCount = 2;
        FramesPerSecond = 2;
        TimePerFrame = (float)1 / FramesPerSecond;
    }

    public override void Update(GameTime gameTime)
    {
        var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _state.Update(this, elapsedTimeInSeconds);
        
        LoopScreen();
        SetDirection();

        base.Update(gameTime);
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

    private void SetDirection()
    {
        if (Direction == Vector2.Zero)
        {
            return;
        }

        var angle = (float)Math.Atan2(Direction.Y, Direction.X);
        Rotation = angle + MathHelper.PiOver2;      
    }
}
