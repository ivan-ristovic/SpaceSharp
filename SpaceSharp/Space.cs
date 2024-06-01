using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceSharp;

public class Space : Game
{
    private int WinW => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    private int WinH => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
    
    private Random _rng = new();
    private List<Star> _stars = new();
    private List<Comet> _comets = new();
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private IEnumerable<SpaceObject> AllObjects => _stars.Cast<SpaceObject>().Concat(_comets);

    public Space()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
        var starsCount = _rng.Next(100, 200);
        _stars = Enumerable.Repeat(1, starsCount)
                .Select(_ => new Star {
                    X = _rng.Next(WinW),
                    Y = _rng.Next(WinH),
                    Size = _rng.Next(2, 5)
                })
                .ToList()
            ;
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        Star._texture = Content.Load<Texture2D>("star");
        Comet._texture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
        Comet._texture.SetData(new[] {Color.White});
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var obj in AllObjects)
            obj.Move();

        if (_rng.NextDouble() < 0.01f)
        {
            _comets.Add(new Comet {
                X = _rng.Next(WinW / 2, WinW),
                Y = 0,
                Size = _rng.Next(10, 50),
                Speed = _rng.Next(5, 10),
                Thickness = (float)_rng.NextDouble() + 1f,
            }); 
        }
        
        _comets.RemoveAll(c => c.IsOutOfBounds());
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();
        
        foreach (var obj in AllObjects)
            obj.Draw(_spriteBatch);

        _spriteBatch.End();
    }
    
}