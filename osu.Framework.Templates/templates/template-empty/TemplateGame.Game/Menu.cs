using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osu.Framework.Screens;
using osuTK.Graphics;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class Menu : Screen
    {
        private MenuButton singleplayer;
        private MenuButton multiplayer;
        private MenuButton settings;

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
                new SpriteText()
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    Y = 50,
                    Text = "Pong",
                    Font = new FontUsage(size: 80)
                },
                singleplayer = new MenuButton()
                {
                    Y = 200,
                    Text = "Singleplayer",
                },
                multiplayer = new MenuButton()
                {
                    Y = 425,
                    Text = "Multiplayer",
                },
                settings = new MenuButton()
                {
                    Y = 650,
                    Text = "Settings",
                },
            };
            if (GameSettings.BallSpeed == 0) GameSettings.SetDefaluts();
            Logger.Log(GameSettings.ToString());
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            IsHovered();
            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            IsHovered();
            return base.OnTouchDown(e);
        }

        private void IsHovered()
        {
            if (singleplayer.IsHovered)
            {
                this.Push(new MainScreen());
            }

            if (multiplayer.IsHovered)
            {
                this.Push(new ServerClientMenu());
            }

            if (settings.IsHovered)
            {
                this.Push(new SettingsScreen());
            }
        }
    }
}
