using System;
using System.Threading;
using a;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Input;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class GameContainerClient : GameLayout
    {
        bool clicked = false;

        public GameContainerClient(bool isPlayer1,string ip)
        {
            load();
            udp = new UdpListener(isPlayer1, ip);
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

            GameSettings.ScoreLimit = 1;
            string[] gameSettingsString = udp.HandShake();

            while (true)
            {
                if (Time.Current - lastTime > 2)
                {

                    data = udp.Networking(p2.Position, ball.Position, clicked,text.Text.ToString());
                    dataQueue.Enqueue(data);
                    lastTime = Time.Current;
                }
            }
        }

        protected override void Update()
        {
            //-----------------Network Movement-----------------
            while (dataQueue.TryDequeue(out UpdateData))
            {
                p1.Position = new Vector2(p1.Position.X, Convert.ToSingle(UpdateData[1]));
                ball.Position = new Vector2(Convert.ToSingle(UpdateData[2]), Convert.ToSingle(UpdateData[3]));
                text.Text = UpdateData[5];
                //ball.Move = Convert.ToBoolean(UpdateData[4]);
            }

            FixedUpdate();
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
