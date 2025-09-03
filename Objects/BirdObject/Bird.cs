using Microsoft.Xna.Framework;
using System;
using VectorGame.Objects.BirdObject.States;

namespace VectorGame.Objects.BirdObject;

public class Bird : GameObject
{
    public float Speed { get; set; } = 100f;
    public float TurningSpeed { get; set; } = 0.01f;
    public Vector2 Direction { get; set; }
    public BirdState State { get; set; }

    protected override string TextureName => "bird";

    public Bird(GameObjectManager gameObjectManager, GraphicsDeviceManager graphics) : base(gameObjectManager, graphics)
    {
        Position = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2);
        FrameCount = 2;
        FramesPerSecond = 2;
        TimePerFrame = (float)1 / FramesPerSecond;
        State = new RandomSteeringState(this);
    }

    public override void Update(GameTime gameTime)
    {
        var elapsedTimeInSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

        State.Update(elapsedTimeInSeconds);
        
        LoopScreen();
        SetRotation();

        base.Update(gameTime);
    }

    private void LoopScreen()
    {
        var position = Position;

        if (position.X > Graphics.PreferredBackBufferWidth)
        {
            position.X = 0;
        }
        else if (position.X < 0)
        {
            position.X = Graphics.PreferredBackBufferWidth;
        }

        if (position.Y > Graphics.PreferredBackBufferHeight)
        {
            position.Y = 0;
        }
        else if (position.Y < 0)
        {
            position.Y = Graphics.PreferredBackBufferHeight;
        }

        Position = position;
    }

    private void SetRotation()
    {
        if (Direction == Vector2.Zero)
        {
            return;
        }

        var angle = (float)Math.Atan2(Direction.Y, Direction.X);
        Rotation = angle + MathHelper.PiOver2;      
    }
}
