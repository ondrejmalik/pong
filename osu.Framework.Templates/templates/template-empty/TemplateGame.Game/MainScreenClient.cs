using System;
using System.Collections.Concurrent;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class MainScreenClient : Screen
    {
        bool p1up = false;
        bool p1down = false;
        bool p2up = false;
        bool p2down = false;
        bool isPlayer1;
        UdpListener udp;
        int lastTime = 0;
        int bluePoints = 0;
        int redPoints = 0;
        SpriteText text;
        Player p1;
        Player p2;
        Ball ball;
        Colider upperColider;
        Colider lowerColider;
        Colider leftColider;
        Colider rightColider;
        private string[] UpdateData = new string[4];
        ConcurrentQueue<string[]> dataQueue = new ConcurrentQueue<string[]>();

        public MainScreenClient(bool isPlayer1)
        {
            udp = new UdpListener(isPlayer1);
            this.isPlayer1 = isPlayer1;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                },
                text = new SpriteText
                {
                    Y = 20,
                    Text = "Pong",
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Font = FontUsage.Default.With(size: 75)
                },
                p1 = new Player(true)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new osuTK.Vector2(50, 540)
                },
                p2 = new Player(false)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Position = new osuTK.Vector2(-50, 540)
                },
                ball = new Ball()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Position = new osuTK.Vector2(0, 0)
                },
                upperColider = new Colider(Side.TOP)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new osuTK.Vector2(0, 0)
                },
                lowerColider = new Colider(Side.BOTTOM)
                {
                    Anchor = Anchor.BottomLeft,
                    Origin = Anchor.BottomLeft,
                    Position = new osuTK.Vector2(0, 0)
                },
                leftColider = new Colider(Side.LEFT)
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Position = new osuTK.Vector2(10, 0),
                    Rotation = 90
                },
                rightColider = new Colider(Side.RIGHT)
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Position = new osuTK.Vector2(-10, 0),
                    Rotation = -90
                }
            };
            Thread networkThread = new Thread(new ThreadStart(Networking));
            networkThread.Start();
            ball.Move = false;
        }

        private void Networking()
        {
            double lastTime = this.Time.Current;
            string[] data = new string[4];

            while (true)
            {
                if (Time.Current - lastTime > 2)
                {
                    data = udp.Networking(p2.Position, ball.Position, ball.Move);
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
                ball.Move = Convert.ToBoolean(UpdateData[4]);
            }

            if (Time.Current > lastTime + 1)
            {
                lastTime = (int)Time.Current;
                FixedUpdate();
            }

            base.Update();
        }

        private void FixedUpdate()
        {
            if (p2up && p2.Position.Y > 10)
            {
                if (!ball.Move)
                {
                    text.Text = bluePoints + ":" + redPoints;
                }

                p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y - 1);
            }

            if (p2down && p2.Position.Y < DrawHeight - 110)
            {
                if (!ball.Move)
                {
                    text.Text = bluePoints + ":" + redPoints;
                }

                p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y + 1);
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.W)
            {
                p2up = true;
            }

            if (e.Key == Key.S)
            {
                p2down = true;
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            if (e.Key == Key.W)
            {
                p2up = false;
            }

            if (e.Key == Key.S)
            {
                p2down = false;
            }
        }
    }
}
