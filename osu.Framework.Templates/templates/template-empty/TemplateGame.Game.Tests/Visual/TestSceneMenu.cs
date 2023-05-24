using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;

namespace TemplateGame.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneMenu : TemplateGameTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.
        ScreenStack stack;
        public TestSceneMenu()
        {
            Add(stack = new ScreenStack(new Menu()) { RelativeSizeAxes = Axes.Both });
            AddStep("Show", () => stack.Push(new Menu() { RelativeSizeAxes = Axes.Both }));
        }
    }
}
