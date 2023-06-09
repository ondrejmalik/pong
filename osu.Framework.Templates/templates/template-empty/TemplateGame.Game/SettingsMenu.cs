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
        private SkinPicker paddleSkinPicker;
        private SkinPicker ballSkinPicker;

        public SettingsMenu()
        {
            slider = new BindableNumberWithCurrent<double>(10);
            slider.MinValue = 0;
            slider.MaxValue = 100;
            slider.Precision = 1;
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
                        Colour = new Color4((byte)3, (byte)127, (byte)196, byte.MaxValue),
                        RelativeSizeAxes = Axes.Both,
                    },
                    backButton = new MenuButton()
                    {
                        Anchor = Anchor.TopLeft,
                        Origin = Anchor.TopLeft,
                        Size = new Vector2(50, 50),
                    },
                    particlesCheckbox = new BasicCheckbox()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(0, 50),
                        LabelText = "Enable Particles",
                        Current = new BindableBool(GameSettings.EnableParticles),
                    },
                    soundVolumeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 100),
                        Font = new FontUsage(size: 80),
                        Text = "Volume "
                    },
                    soundVolume = new BasicSliderBar<double>()
                    {
                        Current = slider,
                        Colour = Color4.DarkRed,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 100),
                        Size = new Vector2(300, 80),
                    },
                    ballSpeedText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 200),
                        Font = new FontUsage(size: 80),
                        Text = "Ball Speed "
                    },
                    ballSpeed = new BasicSliderBar<double>()
                    {
                        Current = slider2,
                        Colour = Color4.Purple,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 200),
                        Size = new Vector2(300, 80),
                    },
                    scoreLimitText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 300),
                        Font = new FontUsage(size: 80),
                        Text = "ScoreLimit "
                    },
                    scoreLimit = new BasicSliderBar<double>()
                    {
                        Current = slider3,
                        Colour = Color4.Green,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 300),
                        Size = new Vector2(300, 80),
                    },
                    paddleSizeText = new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(textOffset, 400),
                        Font = new FontUsage(size: 80),
                        Text = "Paddle Size "
                    },
                    paddleSize = new BasicSliderBar<double>()
                    {
                        Current = slider4,
                        Colour = Color4.Fuchsia,
                        Origin = Anchor.TopCentre,
                        Anchor = Anchor.TopCentre,
                        Position = new Vector2(0, 400),
                        Size = new Vector2(300, 80),
                    },
                    new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Y = 500,
                        Font = new FontUsage(size: 80),
                        Text = "Paddle Skin"
                    },
                    paddleSkinPicker = new SkinPicker()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(0, 600),
                    },
                    new SpriteText()
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Y = 700,
                        Font = new FontUsage(size: 80),
                        Text = "Ball Skin"
                    },
                    ballSkinPicker = new SkinPicker("mic0", "mic1", "mic2", "mic3")
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        Position = new Vector2(0, 800),
                    },

                    submitButton = new MenuButton()
                    {
                        Text = "Submit",
                        Y = 950,
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
            GameSettings.EnableParticles = particlesCheckbox.Current.Value;
            GameSettings.PaddleColour = paddleSkinPicker.SelectedSkin;
            GameSettings.BallColour = ballSkinPicker.SelectedSkin;
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
            soundVolumeText.Text = "Sound Volume " + soundVolume.Current.Value.ToString("0.") + "%";
            ballSpeedText.Text = "Ball Speed " + ballSpeed.Current.Value.ToString("0.0");
            scoreLimitText.Text = "ScoreLimit " + scoreLimit.Current.Value.ToString("0");
            paddleSizeText.Text = "Paddle Size " + paddleSize.Current.Value.ToString("0.0");
        }
    }
}
