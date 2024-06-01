using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceSharp
{
    internal abstract class SpaceObject
    {
        protected static Random _rng = new();
        protected static int _delta = 1;

        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; init; }
   
        public abstract void Move();
        public abstract void Draw(SpriteBatch sb);
    }

    internal sealed class Star : SpaceObject
    {
        internal static Texture2D _texture;
        
        public override void Move()
        {
            this.X += _rng.NextDouble() >= 0.5 ? _delta : -_delta;
            this.Y += _rng.NextDouble() >= 0.5 ? _delta : -_delta;
        }

        public override void Draw(SpriteBatch sb)
        {
            var src = new Rectangle(0, 0, _texture.Width, _texture.Height);
            var dst = new Rectangle(X, Y, Size, Size);
            sb.Draw(_texture, dst, src, Color.White);
        }
    }

    internal sealed class Comet : SpaceObject
    {
        internal static Texture2D _texture;
        
        public int Speed { get; set; }
        public float Thickness { get; set; }
    
        public override void Move()
        {
            this.X -= _delta * this.Speed;
            this.Y += _delta * this.Speed;
        }

        public bool IsOutOfBounds()
        {
            return X < -100 || Y < -100;
        }
    
        public override void Draw(SpriteBatch sb)
        {
            DrawLine(sb, new Vector2(X, Y), new Vector2(X + Size, Y - Size), Color.WhiteSmoke, Thickness);
        }
        
        private static void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            var angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        private static void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            var origin = new Vector2(0f, 0.5f);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(_texture, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
    }
}