using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Graphics;
using osuTK.Input;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        bool p1up = false;
        bool p1down = false;
        bool p2up = false;
        bool p2down = false;
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
        }

        protected override void Update()
        {
            if (Time.Current > lastTime + 1)
            {
                lastTime = (int)Time.Current;
                FixedUpdate();
            }

            //-----------------------Check collision with borders-----------------------
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

            if (lowerColider.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LD)
                {
                    ball.Direction = _Direction.LU;
                }
                if (ball.Direction == _Direction.RD)
                {
                    ball.Direction = _Direction.RU;
                }
            }

            if (leftColider.CheckCollision(ball.CollisionQuad))
            {
                ball.Position = new osuTK.Vector2(0, 0);
                ball.Move = false;
                redPoints++;
                text.Text = "Red Win!";
            }

            if (rightColider.CheckCollision(ball.CollisionQuad))
            {
                ball.Position = new osuTK.Vector2(0, 0);
                ball.Move = false;
                bluePoints++;
                text.Text = "Blue Win!";
            }

            //-----------------------Check collision with players-----------------------
            if (p1.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.LU)
                {
                    ball.Direction = _Direction.RU;
                }
                if (ball.Direction == _Direction.LD)
                {
                    ball.Direction = _Direction.RD;
                }
            }

            if (p2.CheckCollision(ball.CollisionQuad))
            {
                if (ball.Direction == _Direction.RU)
                {
                    ball.Direction = _Direction.LU;
                }
                if (ball.Direction == _Direction.RD)
                {
                    ball.Direction = _Direction.LD;
                }
            }

            base.Update();
        }

        private void FixedUpdate()
        {
            if (p1up && p1.Position.Y > 10)
            {
                if (!ball.Move)
                {
                    ball.Move = true;
                    text.Text = bluePoints + ":" + redPoints;
                }

                p1.Position = new osuTK.Vector2(p1.Position.X, p1.Position.Y - 1);
            }

            if (p1down && p1.Position.Y < DrawHeight - 110)
            {
                if (!ball.Move)
                {
                    ball.Move = true;
                    text.Text = bluePoints + ":" + redPoints;
                }

                p1.Position = new osuTK.Vector2(p1.Position.X, p1.Position.Y + 1);
            }

            if (p2up && p2.Position.Y > 10)
            {
                if (!ball.Move)
                {
                    ball.Move = true;
                    text.Text = bluePoints + ":" + redPoints;
                }

                p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y - 1);
            }

            if (p2down && p2.Position.Y < DrawHeight - 110)
            {
                if (!ball.Move)
                {
                    ball.Move = true;
                    text.Text = bluePoints + ":" + redPoints;
                }

                p2.Position = new osuTK.Vector2(p2.Position.X, p2.Position.Y + 1);
            }
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (e.Key == Key.W)
            {
                p1up = true;
            }

            if (e.Key == Key.S)
            {
                p1down = true;
            }

            if (e.Key == Key.Up)
            {
                p2up = true;
            }

            if (e.Key == Key.Down)
            {
                p2down = true;
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            if (e.Key == Key.W)
            {
                p1up = false;
            }

            if (e.Key == Key.S)
            {
                p1down = false;
            }

            if (e.Key == Key.Up)
            {
                p2up = false;
            }

            if (e.Key == Key.Down)
            {
                p2down = false;
            }
        }
    }
}
