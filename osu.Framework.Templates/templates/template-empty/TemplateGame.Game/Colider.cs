using osu.Framework.Allocation;
using osu.Framework.Extensions.PolygonExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Primitives;
using osuTK;

namespace TemplateGame.Game
{
    public enum Side
    {
        TOP,
        BOTTOM,
        LEFT,
        RIGHT
    }

    public partial class Colider : CompositeDrawable
    {
        Side side;
        private Box box;

        public Colider(Side side)
        {
            this.side = side;
            if (side == Side.LEFT || side == Side.RIGHT) this.Size = new Vector2(10, 1080);
            else this.Size = new Vector2(10, 1920);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    box = new Box()
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Colour4.Blue,
                    },
                }
            };
        }

        protected override void Update()
        {
        }

        public bool CheckCollision(Quad quad)
        {
            if (quad.Intersects(box.ScreenSpaceDrawQuad))
            {
                return true;
            }

            return false;
        }
    }
}
