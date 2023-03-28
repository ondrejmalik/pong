using osu.Framework.Allocation;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace TemplateGame.Game
{
    public partial class Player : CompositeDrawable
    {
        private bool isPlayer1 = false;
        private Sprite sprite;

        public Player(bool isPlayer1)
        {
            this.isPlayer1 = isPlayer1;
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
                    },
                }
            };

            if (isPlayer1) { sprite.Texture = textures.Get("BluePlayer"); }

            if (!isPlayer1) { sprite.Texture = textures.Get("RedPlayer"); }
        }

        public bool CheckCollision(Quad quad)
        {
            if (quad.Intersects(sprite.ScreenSpaceDrawQuad))
            {
                return true;
            }

            return false;
        }
    }
}
