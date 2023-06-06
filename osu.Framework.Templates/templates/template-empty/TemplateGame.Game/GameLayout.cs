using System.Collections.Concurrent;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class GameLayout : CompositeDrawable
    {
        public UdpListener udp;
        public UdpListener handShakeUdp;
        public double lastTime = 0;
        public int bluePoints = 0;
        public int redPoints = 0;
        public SpriteText text;
        public Player p1;
        public Player p2;
        public Ball ball;
        public float speedmultiplayer;
        public Colider upperColider;
        public Colider lowerColider;
        public Colider leftColider;
        public Colider rightColider;
        public BasicButton UpperTouchBox;
        public BasicButton LowerTouchBox;
        public ConcurrentQueue<string[]> dataQueue = new ConcurrentQueue<string[]>();
        public ParticleLayer particleLayer;
        public string[] UpdateData;
        public Container Box;
        public string ip;
        public int collided;

        [BackgroundDependencyLoader]
        public void load()
        {
            InternalChild = Box = new Container
            {
                Size = new Vector2(1920, 1080),
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    text = new SpriteText()
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
                        Origin = Anchor.CentreLeft,
                        Position = new osuTK.Vector2(50, 540)
                    },
                    p2 = new Player(false)
                    {
                        Anchor = Anchor.TopRight,
                        Origin = Anchor.CentreRight,
                        Position = new osuTK.Vector2(-50, 540)
                    },
                    particleLayer = new ParticleLayer()
                    {
                    },
                    ball = new Ball(GameSettings.BallColour)
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
                }
            };
            Box.Add(UpperTouchBox = new BasicButton()
            {
                Colour = Colour4.Transparent,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Position = new osuTK.Vector2(0, 0),
                Size = new osuTK.Vector2(1920, 540)
            });
            Box.Add(LowerTouchBox = new BasicButton()
            {
                Colour = Colour4.Transparent,
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Position = new osuTK.Vector2(0, 540),
                Size = new osuTK.Vector2(1920, 540)
            });
        }

        public int CheckCollisionsWithPlayers()
        {
            int collided = 0;

            if (p1.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LU)
                {
                    collided = 1;
                }

                if (ball.Direction == _Direction.LD)
                {
                    collided = 2;
                }
            }

            if (p2.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.RU)
                {
                    collided = 3;
                }

                if (ball.Direction == _Direction.RD)
                {
                    collided = 4;
                }
            }
            if (collided != 0) ball.HitSound.Restart();
            return collided;
        }

        public int CheckCollisionsWithBorders()
        {
            int collided = 0;

            if (upperColider.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LU)
                {
                    collided = 1;
                }

                if (ball.Direction == _Direction.RU)
                {
                    collided = 2;
                }
            }

            if (lowerColider.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LD)
                {
                    collided = 3;
                }

                if (ball.Direction == _Direction.RD)
                {
                    collided = 4;
                }
            }

            if (leftColider.CheckCollision(ball.CollisionQuad))
            {
                collided = 5;
            }

            if (rightColider.CheckCollision(ball.CollisionQuad))
            {
                collided = 6;
            }

            if (collided != 0) ball.HitSound.Restart();
            return collided;
        }

        public void FixedUpdate()
        {
            //Logger.Log("Update");
            if (Time.Current > lastTime + 1)
            {
                speedmultiplayer = (float)(Time.Current - lastTime);
                lastTime = Time.Current;
                particleLayer.particleFrequencyCount += 1 * speedmultiplayer;

                if (p1.up && p1.Position.Y > 10 + p1.Height / 2)
                {
                    p1.Position = new osuTK.Vector2(p1.Position.X, p1.Position.Y - 1 * speedmultiplayer);
                }

                if (p1.down && p1.Position.Y < DrawHeight - 11 - p1.Height / 2)
                {
                    p1.Position = new osuTK.Vector2(p1.Position.X, p1.Position.Y + 1 * speedmultiplayer);
                }

                if (p2.up && p2.Position.Y > 10 + p2.Height / 2)
                {
                    p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y - 1 * speedmultiplayer);
                }

                if (p2.down && p2.Position.Y < DrawHeight - 11 - p2.Height / 2)
                {
                    p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y + 1 * speedmultiplayer);
                }

                particleLayer.AddParticle(ball.Position);
            }
        }

        public void BallStartMoving()
        {
            if (!ball.Move)
            {
                ball.Move = true;
                text.Text = bluePoints + ":" + redPoints;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            udp.Close();
            base.Dispose(isDisposing);
        }
    }
}
