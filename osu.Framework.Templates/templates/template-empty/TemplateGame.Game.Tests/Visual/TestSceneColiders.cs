using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace TemplateGame.Game.Tests.Visual
{
    /// <summary>
    /// A scene to test the layout and positioning and rotation of two pipe sprites.
    /// </summary>
    [TestFixture]
    public partial class TestSceneColiders : TemplateGameTestScene
    {
        public TestSceneColiders()
        {
            AddStep("Load", () => load());
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new Colider(Side.LEFT)
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Position = new osuTK.Vector2(0, 0),
            });

            Add(new Colider(Side.TOP)
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.TopLeft,
                Position = new osuTK.Vector2(10, 10),
                Rotation = 270
            });
            Add(new Colider(Side.BOTTOM)
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.TopLeft,
                Position = new osuTK.Vector2(0, 0),
                Rotation = 270
            });

            Add(new Colider(Side.RIGHT)
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Position = new osuTK.Vector2(0, 0),
            });
        }
    }
}
