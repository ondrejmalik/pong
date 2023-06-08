using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace TemplateGame.Game.Tests.Visual
{
    /// <summary>
    /// A scene to test the layout and positioning and rotation of two pipe sprites.
    /// </summary>
    [TestFixture]
    public partial class TestSceneSkinPicker : TemplateGameTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new SkinPicker()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}
