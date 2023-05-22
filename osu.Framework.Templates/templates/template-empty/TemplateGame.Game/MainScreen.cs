using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        private GameContainer gameContainer;

        public MainScreen(bool isPlayer1)
        {
            gameContainer = new GameContainer(isPlayer1);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                gameContainer
            };
        }
    }
}
