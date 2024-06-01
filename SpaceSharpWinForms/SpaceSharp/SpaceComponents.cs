using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SpaceSharp
{
    internal abstract class SpaceObject
    {
        protected static Random _rng = new Random();
        protected static float _delta = 0.05f;

        public float X { get; set; }
        public float Y { get; set; }
        public float Size { get; set; }
   
        public abstract void Move();
        public abstract void Draw(Graphics g);
    }


    internal sealed class Star : SpaceObject
    {
        public bool IsSupernova { get; set; }

        private List<SupernovaShard> shards;


        public override void Move()
        {
            this.X += _rng.NextDouble() >= 0.5 ? _delta : -_delta;
            this.Y += _rng.NextDouble() >= 0.5 ? _delta : -_delta;

            if (this.shards is { }) {
                foreach (SupernovaShard shard in shards)
                    shard.Move();
            }
        }

        public override void Draw(Graphics g)
        {
            if (this.IsSupernova) {
                if (this.shards is null) {
                    var rng = new Random();
                    this.shards = Enumerable.Repeat(1, rng.Next(300, 500))
                        .Select(_ => new SupernovaShard {
                            X = this.X,
                            Y = this.Y,
                            Angle = rng.Next(360) * (MathF.PI / 180f),
                            Speed = rng.Next(-16, 16),
                            Brush = new SolidBrush(Color.FromArgb(rng.Next(256), rng.Next(256), rng.Next(256)))
                        })
                        .ToList();
                }
                g.FillEllipse(Brushes.Orange, this.X, this.Y, this.Size, this.Size);
                foreach (SupernovaShard shard in this.shards)
                    shard.Draw(g);
            } else {
                g.FillEllipse(Brushes.White, this.X, this.Y, this.Size, this.Size);
            }
        }
    }

    internal sealed class SupernovaShard : SpaceObject
    {
        public float Angle { get; set; }
        public float Speed { get; set; }
        public Color Color { get; set; }
        public Brush Brush { get; set; }

        public override void Draw(Graphics g)
        {
            g.FillEllipse(this.Brush, this.X, this.Y, 2, 2);
        }

        public override void Move()
        {
            this.X += this.Speed * MathF.Cos(this.Angle);
            this.Y += this.Speed * MathF.Sin(this.Angle);
            this.Speed /= 1.5f;
            if (MathF.Abs(this.Speed) < 1e-6f)
                this.Speed = 0;
        }
    }


    internal sealed class Comet : SpaceObject
    {
        public float Speed { get; set; }
        public float Thickness { get; set; }

        public override void Move()
        {
            this.X -= _delta * this.Speed;
            this.Y += _delta * this.Speed;
        }

        public override void Draw(Graphics g)
        {
            var cometPen = new Pen(Brushes.WhiteSmoke, this.Thickness);
            g.DrawLine(cometPen, this.X, this.Y, this.X + this.Size, this.Y - this.Size);
        }
    }
}
