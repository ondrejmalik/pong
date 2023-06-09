using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK;
using osuTK.Graphics;

namespace TemplateGame.Game
{
    public partial class ServerClientMenu : Screen
    {
        private MenuButton server;
        private MenuButton client;
        private BasicTextBox ipText;
        private MenuButton back;
        private TooltipContainer.Tooltip tooltip;

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
                back = new MenuButton()
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Size = new Vector2(50, 50),
                },
                new SpriteText()
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Y = 50,
                    Text = "Multiplayer",
                    Font = new FontUsage(size: 80)
                },
                server = new MenuButton()
                {
                    Y = 200,
                    Text = "server",
                    Size = new osuTK.Vector2(400, 100),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                client = new MenuButton()
                {
                    Y = 425,
                    Text = "client",
                    Size = new osuTK.Vector2(400, 100),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                ipText = new BasicTextBox()
                {
                    Position = new osuTK.Vector2(0, 650),
                    Size = new osuTK.Vector2(400, 100),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Text = "127.0.0.1",
                    LengthLimit = 16,
                },
                tooltip = new TooltipContainer.Tooltip()
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    TooltipText = "Set this to other player's IP address",
                },
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            isHovered();
            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            isHovered();
            return base.OnTouchDown(e);
        }

        private void isHovered()
        {
            if (back.IsHovered) this.Push(new Menu());

            if (server.IsHovered) this.Push(new MainScreen(false, ipText.Text.ToString()));

            if (client.IsHovered) this.Push(new MainScreen(true, ipText.Text.ToString()));
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            if (ipText.IsHovered)
            {
                tooltip.Position = new Vector2(e.MousePosition.X + 50, e.MousePosition.Y + 50);
                tooltip.Show();
            }
            else
            {
                tooltip.Hide();
            }

            return base.OnMouseMove(e);
        }
    }
}
