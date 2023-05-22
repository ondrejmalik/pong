using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;

namespace TemplateGame.Game.Tests.Visual
{
    /// <summary>
    /// A scene to test the layout and positioning and rotation of two pipe sprites.
    /// </summary>
    [TestFixture]
    public partial class ParticleTestScene : TemplateGameTestScene
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Add(new Particle()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}
