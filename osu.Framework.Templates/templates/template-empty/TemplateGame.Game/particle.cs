using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;

namespace TemplateGame.Game
{
    public partial class Particle : CompositeDrawable
    {
        private Sprite sprite;

        public Particle()
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;
            Anchor = Anchor.Centre;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            InternalChild = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Children = new Drawable[]
                {
                    sprite = new Sprite
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Texture = textures.Get("particle"),
                    },
                }
            };
        }

        protected override void LoadComplete()
        {
            this.LifetimeEnd = Time.Current + 200;
            base.LoadComplete();
        }
    }
}
