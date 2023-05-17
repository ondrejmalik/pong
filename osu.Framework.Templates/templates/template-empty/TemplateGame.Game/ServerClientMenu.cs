using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace TemplateGame.Game
{
    public partial class ServerClientMenu : Screen
    {
        private Button server;
        private Button client;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Violet,
                    RelativeSizeAxes = Axes.Both,
                },
                server = new BasicButton()
                {
                    Y = 100,
                    Text = "server",
                    Size = new osuTK.Vector2(200, 50),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                client = new BasicButton()
                {
                    Y = 200,
                    Text = "client",
                    Size = new osuTK.Vector2(200, 50),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (server.IsHovered)
            {
                this.Push(new MainScreen(false));
            }

            if (client.IsHovered)
            {
                this.Push(new MainScreenClient(true));
            }

            return base.OnMouseDown(e);
        }
    }
}
