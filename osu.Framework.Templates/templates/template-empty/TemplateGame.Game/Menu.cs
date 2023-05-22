using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace TemplateGame.Game
{
    public partial class Menu : Screen
    {
        private Button singleplayer;
        private Button multiplayer;

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
                singleplayer = new BasicButton()
                {
                    Y = 100,
                    Text = "Singleplayer",
                    Size = new osuTK.Vector2(200, 50),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
                multiplayer = new BasicButton()
                {
                    Y = 200,
                    Text = "Multiplayer",
                    Size = new osuTK.Vector2(200, 50),
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                },
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (singleplayer.IsHovered)
            {
                this.Push(new MainScreen(false));
            }

            if (multiplayer.IsHovered)
            {
                this.Push(new ServerClientMenu());
            }

            return base.OnMouseDown(e);
        }
    }
}
