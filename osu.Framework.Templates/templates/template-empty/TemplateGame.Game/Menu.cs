using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Graphics;

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
                singleplayer = new MenuButton()
                {
                    Y = 100,
                    Text = "Singleplayer",
                },
                multiplayer = new MenuButton()
                {
                    Y = 400,
                    Text = "Multiplayer",
                },
                settings = new MenuButton()
                {
                    Y = 700,
                    Text = "Settings",
                },
            };
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
                this.Push(new MainScreen(false, "127.0.0.1"));
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
