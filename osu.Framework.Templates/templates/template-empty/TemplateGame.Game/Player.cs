using osu.Framework.Allocation;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;
using osuTK.Graphics;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class Player : CompositeDrawable
    {
        private bool isPlayer1 = false;
        private Sprite sprite;
        private Box hitbox;
        public bool up, down;
        private TextureStore textures;
        private string textureName = "player";
        private Container box;
        private Vector2 hitboxSize;

        public Player(bool isPlayer1, bool isServer)
        {
            if (isServer) hitboxSize = new Vector2(15, GameSettings.PaddleSize * 15);
            else hitboxSize = new Vector2(15, 7 * 15);
            this.isPlayer1 = isPlayer1;
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            this.textures = textures;
            InternalChild = box = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    hitbox = new Box()
                    {
                        Size = hitboxSize,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    }
                }
            };
        }

        public void ChangeSkin()
        {
            switch (GameSettings.PaddleColour)
            {
                case 0:
                    hitbox.Colour = Color4.Red;
                    break;

                case 1:
                    hitbox.Colour = Color4.Blue;
                    break;

                case 2:
                    hitbox.Colour = Color4.Purple;
                    break;

                case 3:
                    hitbox.Colour = Color4.FloralWhite;
                    break;
            }

            if (textures.Get(textureName + GameSettings.PaddleColour) != null)
            {
                box.Add(sprite = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = hitboxSize,
                    Texture = textures.Get(textureName + GameSettings.PaddleColour),
                });
            }
        }

        public void UpdateSize()
        {
            hitbox.Size = new Vector2(15, GameSettings.PaddleSize * 15);
        }

        public bool CheckCollision(Quad quad)
        {
            if (quad.Intersects(hitbox.ScreenSpaceDrawQuad))
            {
                return true;
            }

            return false;
        }
    }
}
