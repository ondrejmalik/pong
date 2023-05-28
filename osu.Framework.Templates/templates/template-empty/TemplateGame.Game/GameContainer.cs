using System;
using System.Threading;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Input;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class GameContainer : GameLayout
    {
        public GameContainer(bool singlePlayer, string ip)
        {
            this.ip = ip;
            GameSettings.BallColour = 0;
            load();
            handShakeUdp = new UdpListener(false, ip);
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
                Thread.Sleep(320);
            }

            GameSettings.BallColour = 0;
            GameSettings.PaddleColour = 2;
            Scheduler.Add(() => p1.ChangeSkin()); //Change skin before handshake
            Scheduler.Add(() => ball.ChangeSkin());
            string[] gameSettingsString = handShakeUdp.HandShake(false);
            GameSettings.SetSettings(gameSettingsString);
            Scheduler.Add(() => p2.ChangeSkin()); //Change skin after handshake
            Logger.Log("Handshake complete");
            Logger.Log(GameSettings.ToString());
            handShakeUdp.Close();
            udp = new UdpListener(false, ip);

            while (udp.WaitingForDestroy == false)
            {
                if (Time.Current - lastTime > 1)
                {
                    data = udp.Networking(p1.Position, ball.Position, ball.Move, text.Text.ToString());
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
                //Logger.Log(dataQueue.Count.ToString());
                p2.Position = new Vector2(p2.Position.X, Convert.ToSingle(UpdateData[1]));
                if (!ball.Move) ball.Move = Convert.ToBoolean(UpdateData[4]);
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

            BallStartMoving();
            return base.OnKeyDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            if (UpperTouchBox.IsHovered)
            {
                p1.up = true;
            }

            if (LowerTouchBox.IsHovered)
            {
                p1.down = true;
            }

            BallStartMoving();
            return base.OnTouchDown(e);
        }

        protected override void OnTouchUp(TouchUpEvent e)
        {
            p1.up = false;
            p1.down = false;
            base.OnTouchUp(e);
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

        protected override void Dispose(bool isDisposing)
        {
            udp.Close();
            base.Dispose(isDisposing);
        }
    }
}
