using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;
using UdpTest.Game;

namespace TemplateGame.Game
{
    public partial class SettingsMenu : CompositeDrawable
    {
        private MenuButton submitButton;
        private BasicCheckbox particlesCheckbox;
        private Container box;
        private BindableNumberWithCurrent<double> slider;
        private BindableNumberWithCurrent<double> slider2;
        private BindableNumberWithCurrent<double> slider3;
        private BindableNumberWithCurrent<double> slider4;
        private BasicSliderBar<double> soundVolume;
        private BasicSliderBar<double> ballSpeed;
        private BasicSliderBar<double> scoreLimit;
        private BasicSliderBar<double> paddleSize;
        private SpriteText soundVolumeText;
        private SpriteText ballSpeedText;
        private SpriteText scoreLimitText;
        private SpriteText paddleSizeText;
        private SpriteText EnableParticlesText;
        private SpriteText BackgroundColourText;
        private SpriteText BallColourText;
        private SpriteText PaddleColourText;
        private int textOffset = -500;
        private MenuButton backButton;
        public bool Exit = false;
        public SettingsMenu()
        {
            slider = new BindableNumberWithCurrent<double>(0.5);
            slider.MinValue = 0;
            slider.MaxValue = 1;
            slider.Precision = 0.1;
            slider.Value = GameSettings.SoundVolume;
            slider2 = new BindableNumberWithCurrent<double>(0.5);
            slider2.MinValue = 0.1;
            slider2.MaxValue = 5;
            slider2.Precision = 0.1;
            slider2.Value = GameSettings.BallSpeed;
            slider3 = new BindableNumberWithCurrent<double>(0.5);
            slider3.MinValue = 1;
            slider3.MaxValue = 30;
            slider3.Precision = 1;
            slider3.Value = GameSettings.ScoreLimit;
            slider4 = new BindableNumberWithCurrent<double>(0.5);
            slider4.MinValue = 1;
            slider4.MaxValue = 20;
            slider4.Precision = 0.5;
            slider4.Value = GameSettings.PaddleSize;

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
                    backButton = new MenuButton()
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Size = new Vector2(50, 50),
                    },
                    soundVolumeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 200),
                        Font = new FontUsage(size: 80),
                        Text = "Volume "
                    },
                    soundVolume = new BasicSliderBar<double>()
                    {
                        Current = slider,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 200),
                        Size = new Vector2(300, 80),
                    },
                    ballSpeedText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 300),
                        Font = new FontUsage(size: 80),
                        Text = "Ball Speed "
                    },
                    ballSpeed = new BasicSliderBar<double>()
                    {
                        Current = slider2,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 300),
                        Size = new Vector2(300, 80),
                    },
                    scoreLimitText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 400),
                        Font = new FontUsage(size: 80),
                        Text = "ScoreLimit "
                    },
                    scoreLimit = new BasicSliderBar<double>()
                    {
                        Current = slider3,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 400),
                        Size = new Vector2(300, 80),
                    },
                    paddleSizeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 500),
                        Font = new FontUsage(size: 80),
                        Text = "Paddle Size "
                    },
                    paddleSize = new BasicSliderBar<double>()
                    {
                        Current = slider4,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 500),
                        Size = new Vector2(300, 80),
                    },
                    particlesCheckbox = new BasicCheckbox()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 600),
                        LabelText = "Enable Particles",
                    },
                    submitButton = new MenuButton()
                    {
                        Text = "Submit",
                        Y = 700,
                    },
                }
            };
        }

        protected override bool OnMouseDown(MouseDownEvent e)
        {
            if (submitButton.IsHovered)
            {
                submit();
            }

            if (backButton.IsHovered)
            {
                back();
            }

            return base.OnMouseDown(e);
        }

        protected override bool OnTouchDown(TouchDownEvent e)
        {
            if (submitButton.IsHovered)
            {
                submit();
            }

            if (backButton.IsHovered)
            {
                back();
            }

            return base.OnTouchDown(e);
        }

        private void submit()
        {
            GameSettings.SoundVolume = soundVolume.Current.Value;
            GameSettings.BallSpeed = (float)ballSpeed.Current.Value;
            GameSettings.ScoreLimit = (int)scoreLimit.Current.Value;
            GameSettings.PaddleSize = (float)paddleSize.Current.Value;
            Logger.Log(GameSettings.ToString());
            //SettingsLoader.SaveSettings(GameSettings);
        }

        private void back()
        {
            Exit = true;
        }

        protected override void Update()
        {
            updateText();
        }

        private void updateText()
        {
            soundVolumeText.Text = "Sound Volume " + soundVolume.Current.Value.ToString("0.0");
            ballSpeedText.Text = "Ball Speed " + ballSpeed.Current.Value.ToString("0.0");
            scoreLimitText.Text = "ScoreLimit " + scoreLimit.Current.Value.ToString("0");
            paddleSizeText.Text = "Paddle Size " + paddleSize.Current.Value.ToString("0.0");
        }
    }
}
