using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace TemplateGame.Game
{
    public partial class MainScreen : Screen
    {
        private GameContainer gameContainer;
        private GameContainerClient gameContaineClient;

        public MainScreen(bool isPlayer1, string ip)
        {
            if (!isPlayer1) gameContainer = new GameContainer(ip);
            else gameContaineClient = new GameContainerClient(ip);
        }

        public MainScreen()
        {
            gameContainer = new GameContainer();
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

        protected override void Update()
        {
            //backbutton for game
        }
    }
}
