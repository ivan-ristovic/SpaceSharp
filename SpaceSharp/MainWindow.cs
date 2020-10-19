using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SpaceSharp
{
    public partial class MainWindow : Form
    {
        private readonly List<Star> stars = new List<Star>();
        private readonly List<Comet> comets = new List<Comet>();
        private IEnumerable<SpaceObject> AllObjects => this.stars.Cast<SpaceObject>().Concat(this.comets);


        public MainWindow()
        {
            this.InitializeComponent();

            var rng = new Random();
            int starsCount = rng.Next(100, 200);
            this.stars = Enumerable.Repeat(1, starsCount)
                                   .Select(_ => new Star {
                                       X = rng.Next(this.Width),
                                       Y = rng.Next(this.Height),
                                       Size = rng.Next(2, 5)
                                   })
                                   .ToList()
                                   ;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void tmrTick_Tick(object sender, EventArgs e)
        {
            foreach (SpaceObject obj in this.stars.Cast<SpaceObject>().Concat(this.comets))
                obj.Move();
            this.Invalidate();
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (SpaceObject obj in this.AllObjects)
                obj.Draw(e.Graphics);

            this.comets.RemoveAll(comet => comet.X < -100 || comet.Y < -100);
        }

        private void tmrComet_Tick(object sender, EventArgs e)
        {
            var rng = new Random();
            this.comets.Add(new Comet {
                X = rng.Next(this.Width / 2, this.Width),
                Y = 0,
                Size = rng.Next(50, 100),
                Speed = rng.Next(400, 800),
                Thickness = (float)rng.NextDouble() + 1f,
            });
            this.tmrComet.Interval = rng.Next(3, 5) * 1000;
        }

        private void tmrSupernova_Tick(object sender, EventArgs e)
        {
            this.stars[new Random().Next(stars.Count)].IsSupernova = true;
        }
    }
}
