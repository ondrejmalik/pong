using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        private GameContainer gameContainer;
        private GameContainerClient gameContaineClient;

        public MainScreen(bool isPlayer1,string ip)
        {
            if (!isPlayer1) gameContainer = new GameContainer(isPlayer1,ip);
            else gameContaineClient = new GameContainerClient(isPlayer1,ip);
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            if (gameContainer != null)
            {
                InternalChildren = new Drawable[]
                {
                    gameContainer
                };
            }
            else
            {
                InternalChildren = new Drawable[]
                {
                    gameContaineClient
                };
            }
        }
    }
}
