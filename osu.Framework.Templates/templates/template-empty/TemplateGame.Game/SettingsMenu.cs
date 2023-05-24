using a;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace TemplateGame.Game
{
    public partial class SettingsMenu : CompositeDrawable
    {
        private MenuButton submit;
        private BasicCheckbox particlesCheckbox;
        FillFlowContainer flow;
        public static GameSettings gameSettings;
        private Container box;

        public SettingsMenu()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            //gameSettings = SettingsLoader.Load();
            InternalChild = box = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                    new Box()
                    {
                        Colour = Color4.Goldenrod,
                        RelativeSizeAxes = Axes.Both,
                    },
                    submit = new MenuButton()
                    {
                        Text = "Submit",
                        Y = 700,
                    },
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (submit.IsHovered)
            {
            }

            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            if (submit.IsHovered)
            {
            }

            return base.OnTouchDown(e);
        }
    }
}
