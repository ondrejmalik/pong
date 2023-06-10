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
        public GameContainer(string ip) //multiplayer
        {
            this.ip = ip;
            isServer = true;
            load();
            RelativeSizeAxes = Axes.Both;
            handShakeUdp = new UdpListener(false, ip);
            Thread networkThread = new Thread(new ThreadStart(Networking));
            networkThread.Start();
        }

        public GameContainer() //singleplayer
        {
            this.ip = ip;
            isServer = true;
            load();
            RelativeSizeAxes = Axes.Both;
            Scheduler.Add(() => p1.ChangeSkin());
            Scheduler.Add(() => ball.ChangeSkin());
            Scheduler.Add(() => GameSettings.PaddleColour = 1);
            Scheduler.Add(() => p2.ChangeSkin());
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
                if (!ball.Move && Convert.ToBoolean(UpdateData[4])) BallStartMoving();
            }

            FixedUpdate();

            collided = CheckCollisionsWithBorders();
            SwitchDirectionFromBorders();
            collided = CheckCollisionsWithPlayers();
            SwitchBallDirectionFromPlayers();
            base.Update();
        }

        public void SwitchBallDirectionFromPlayers()
        {
            switch (collided)
            {
                case 1:
                    ball.Direction = _Direction.RU;
                    break;

                case 2:
                    ball.Direction = _Direction.RD;
                    break;

                case 3:
                    ball.Direction = _Direction.LU;
                    break;

                case 4:
                    ball.Direction = _Direction.LD;
                    break;
            }
        }

        public void SwitchDirectionFromBorders()
        {
            switch (collided)
            {
                case 1:
                    ball.Direction = _Direction.LD;
                    break;

                case 2:
                    ball.Direction = _Direction.RD;
                    break;

                case 3:
                    ball.Direction = _Direction.LU;
                    break;

                case 4:
                    ball.Direction = _Direction.RU;
                    break;

                case 5:
                    ball.Position = new osuTK.Vector2(0, 0);
                    ball.Move = false;
                    redPoints++;
                    text.Text = "Player 2 Win!";
                    break;

                case 6:
                    ball.Position = new osuTK.Vector2(0, 0);
                    ball.Move = false;
                    bluePoints++;
                    text.Text = "Player 1 Win!";
                    break;
            }

            if (upperColider.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LU)
                {
                    ball.Direction = _Direction.LD;
                }

                if (ball.Direction == _Direction.RU)
                {
                    ball.Direction = _Direction.RD;
                }
            }
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

            if (e.Key == Key.Up && ip == null)
            {
                p2.up = true;
            }

            if (e.Key == Key.Down && ip == null)
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

            if (e.Key == Key.Up && ip == null)
            {
                p2.up = false;
            }

            if (e.Key == Key.Down && ip == null)
            {
                p2.down = false;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
        }
    }
}
