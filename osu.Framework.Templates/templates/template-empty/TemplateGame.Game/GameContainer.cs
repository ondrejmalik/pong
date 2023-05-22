using System;
using System.Threading;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class GameContainer : GameLayout
    {
        public GameContainer(bool singlePlayer)
        {
            load();
            udp = new UdpListener(false);
            RelativeSizeAxes = Axes.Both;
            Thread networkThread = new Thread(new ThreadStart(Networking));
            networkThread.Start();
        }

        private void Networking()
        {
            double lastTime = 0;
            string[] data;

            try
            {
                lastTime = this.Time.Current;
                data = new string[4];
            }
            catch (Exception e)
            {
                Thread.Sleep(32);
            }

            while (true)
            {
                if (Time.Current - lastTime > 2)
                {
                    data = udp.Networking(p1.Position, ball.Position, ball.Move);
                    dataQueue.Enqueue(data);
                    lastTime = Time.Current;
                }
            }
        }

        protected override void Update()
        {
            //--------------Network Movement----------------
            while (dataQueue.TryDequeue(out UpdateData))
            {
                p2.Position = new Vector2(p2.Position.X, Convert.ToSingle(UpdateData[1]));
                ball.Move = Convert.ToBoolean(UpdateData[4]);
            }

            FixedUpdate();

            CheckCollisionsWithBorders();
            CheckCollisionsWithPlayers();
            base.Update();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.W)
            {
                p1.up = true;
            }

            if (e.Key == Key.S)
            {
                p1.down = true;
            }

            if (e.Key == Key.Up)
            {
                p2.up = true;
            }

            if (e.Key == Key.Down)
            {
                p2.down = true;
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            if (e.Key == Key.W)
            {
                p1.up = false;
            }

            if (e.Key == Key.S)
            {
                p1.down = false;
            }

            if (e.Key == Key.Up)
            {
                p2.up = false;
            }

            if (e.Key == Key.Down)
            {
                p2.down = false;
            }
        }
    }
}
