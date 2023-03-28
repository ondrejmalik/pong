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
        float width;
        private Box box;
        public Colider(Side side)
        {
            this.side = side;
            RelativeSizeAxes = Axes.X;
            AutoSizeAxes = Axes.Y;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChild = new Container
            {
                RelativeSizeAxes = Axes.X,
                AutoSizeAxes = Axes.Y,
                Children = new Drawable[]
                {
                    box = new Box()
                    {
                        RelativeSizeAxes = Axes.X,
                        Origin = Anchor.TopLeft,
                        Size = new Vector2(1, 10),
                        Colour = Colour4.Blue,
                    },
                }
            };
        }

        protected override void Update()
        {
            width = Parent.Parent.DrawWidth;
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
