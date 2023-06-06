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
        private MenuButton server;
        private MenuButton client;
        private BasicTextBox ipText;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Goldenrod,
                    RelativeSizeAxes = Axes.Both,
                },
                server = new MenuButton()
                {
                    Y = 100,
                    Text = "server",
                    Size = new osuTK.Vector2(400, 100),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                client = new MenuButton()
                {
                    Y = 300,
                    Text = "client",
                    Size = new osuTK.Vector2(400, 100),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                ipText = new BasicTextBox()
                {
                    Position = new osuTK.Vector2(0, 500),
                    Size = new osuTK.Vector2(200, 50),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = "127.0.0.1",
                    LengthLimit = 16,
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (server.IsHovered)
            {
                this.Push(new MainScreen(false, ipText.Text.ToString()));
            }

            if (client.IsHovered)
            {
                this.Push(new MainScreen(true, ipText.Text.ToString()));
            }

            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            if (server.IsHovered)
            {
                this.Push(new MainScreen(false, ipText.Text.ToString()));
            }

            if (client.IsHovered)
            {
                this.Push(new MainScreen(true, ipText.Text.ToString()));
            }

            return base.OnTouchDown(e);
        }
    }
}
