using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

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
        private double lastTime = 0;
        private float speedRatio = 1;
        public bool Move = false;
        public Ball()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = textures.Get("mic1")
                    },
                }
            };
            Random r = new Random();
            Direction = (_Direction)r.Next(0, 4);
        }

        protected override void Update()
        {
            base.Update();

            if (Time.Current > lastTime + 1)
            {
                speedRatio = (float)(Time.Current - lastTime);
                lastTime = Time.Current;

                if (Move)
                {
                    switch (Direction)
                    {
                        case _Direction.LU:
                            Position = new Vector2(Position.X - 1 * speedRatio, Position.Y - 1 * speedRatio);
                            break;

                        case _Direction.LD:
                            Position = new Vector2(Position.X - 1 * speedRatio, Position.Y + 1 * speedRatio);
                            break;

                        case _Direction.RU:
                            Position = new Vector2(Position.X + 1 * speedRatio, Position.Y - 1 * speedRatio);
                            break;

                        case _Direction.RD:
                            Position = new Vector2(Position.X + 1 * speedRatio, Position.Y + 1 * speedRatio);
                            break;

                        default:
                            Position = new Vector2(Position.X, Position.Y);
                            break;
                    }
                }
            }
        }

        public Quad CollisionQuad
        {
            get
            {
                RectangleF rect = sprite.ConservativeScreenSpaceDrawQuad.AABBFloat;
                return Quad.FromRectangle(rect);
            }
        }
    }
}
