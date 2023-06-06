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
    public partial class GameContainerClient : GameLayout
    {
        bool clicked = false;

        public GameContainerClient(bool isPlayer1, string ip)
        {
            this.ip = ip;
            load();
            handShakeUdp = new UdpListener(true, ip);
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

            GameSettings.PaddleColour = 1;
            Scheduler.Add(() => p2.ChangeSkin()); //Change skin before handshake
            string[] gameSettingsString = handShakeUdp.HandShake(true);
            GameSettings.SetSettings(gameSettingsString);
            ball.Skin = Convert.ToInt32(gameSettingsString[6]);
            Scheduler.Add(() => ball.ChangeSkin());
            Scheduler.Add(() => p1.ChangeSkin()); //Change skin after handshake
            Scheduler.Add(() => p1.UpdateSize());
            Scheduler.Add(() => p2.UpdateSize());
            Logger.Log("Handshake complete");
            handShakeUdp.Close();
            udp = new UdpListener(true, ip);

            while (udp.WaitingForDestroy == false)
            {
                if (Time.Current - lastTime > 1)
                {
                    data = udp.Networking(p2.Position, ball.Position, clicked, text.Text.ToString());

                    if (2 < data.Length)
                    {
                        dataQueue.Enqueue(data);
                    }
                    else
                    {
                        Logger.Log("No data");
                        Logger.Log(data.ToString());
                    }

                    lastTime = Time.Current;
                }
            }
        }

        protected override void Update()
        {
            //-----------------Network Movement-----------------
            while (dataQueue.TryDequeue(out UpdateData))
            {
                //Logger.Log(dataQueue.Count.ToString());
                p1.Position = new Vector2(p1.Position.X, Convert.ToSingle(UpdateData[1]));
                ball.Position = new Vector2(Convert.ToSingle(UpdateData[2]), Convert.ToSingle(UpdateData[3]));
                text.Text = UpdateData[5];
                //ball.Move = Convert.ToBoolean(UpdateData[4]);
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
                p2.up = true;
            }

            if (e.Key == Key.S)
            {
                p2.down = true;
            }

            clicked = true;
            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            if (e.Key == Key.W)
            {
                p2.up = false;
            }

            if (e.Key == Key.S)
            {
                p2.down = false;
            }

            clicked = false;
        }

        protected override void Dispose(bool isDisposing)
        {
            udp.Close();
            base.Dispose(isDisposing);
        }
    }
}
