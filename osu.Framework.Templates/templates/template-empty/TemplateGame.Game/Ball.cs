using System;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using UdpTest.Game;
using Color4 = osuTK.Graphics.Color4;

namespace TemplateGame.Game
{
    public enum _Direction
    {
        LU, LD, RU, RD
    }

    public partial class Ball : CompositeDrawable
    {
        public _Direction Direction = _Direction.LD;
        Sprite sprite;
        private Circle circle;
        private double lastTime = 0;
        private float speedRatio = 1;
        private float ballSpeed = 1;
        public bool Move = false;
        public int Skin;
        private TextureStore textures;
        private string textureName = "mic";
        private Container box;
        public Track HitSound;

        public Ball(int skin)
        {
            this.Skin = skin;
            ballSpeed = GameSettings.BallSpeed;
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures, ITrackStore tracks)
        {
            HitSound = tracks.Get("Hit");
            HitSound.Volume.Value = 0.05f;
            this.textures = textures;
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    circle = new Circle()
                    {
                        Size = new Vector2(64, 64),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    },
                }
            };
            Random r = new Random();
            Direction = (_Direction)r.Next(0, 4);
        }

        protected override void LoadComplete()
        {
            //ChangeSkin();
            base.LoadAsyncComplete();
        }

        public void ChangeSkin()
        {
            switch (Skin)
            {
                case 0:
                    circle.Colour = Color4.Yellow;
                    break;

                case 1:
                    circle.Colour = Color4.Red;
                    break;

                case 2:
                    circle.Colour = Color4.Purple;
                    break;

                case 3:
                    circle.Colour = Color4.FloralWhite;
                    break;
            }

            if (textures.Get(textureName + Skin) != null)
            {
                box.Add(sprite = new Sprite()
                {
                    Size = new Vector2(64, 64),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Texture = textures.Get(textureName + Skin),
                });
            }
        }

        protected override void Update()
        {
            if (Time.Current > lastTime + 1)
            {
                speedRatio = (float)(Time.Current - lastTime);
                lastTime = Time.Current;

                if (Move)
                {
                    switch (Direction)
                    {
                        case _Direction.LU:
                            Position = new Vector2(Position.X - 1 * speedRatio * ballSpeed, Position.Y - 1 * speedRatio * ballSpeed);
                            break;

                        case _Direction.LD:
                            Position = new Vector2(Position.X - 1 * speedRatio * ballSpeed, Position.Y + 1 * speedRatio * ballSpeed);
                            break;

                        case _Direction.RU:
                            Position = new Vector2(Position.X + 1 * speedRatio * ballSpeed, Position.Y - 1 * speedRatio * ballSpeed);
                            break;

                        case _Direction.RD:
                            Position = new Vector2(Position.X + 1 * speedRatio * ballSpeed, Position.Y + 1 * speedRatio * ballSpeed);
                            break;

                        default:
                            Position = new Vector2(Position.X, Position.Y);
                            break;
                    }
                }
            }

            base.Update();
        }

        public Quad CollisionQuad
        {
            get
            {
                RectangleF rect = circle.ScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }
    }
}
