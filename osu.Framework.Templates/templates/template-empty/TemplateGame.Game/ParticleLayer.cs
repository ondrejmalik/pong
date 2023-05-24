using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace TemplateGame.Game
{
    public partial class ParticleLayer : CompositeDrawable
    {
        private Container Box;
        public float particleFrequencyCount = 0;

        public ParticleLayer()
        {
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            RelativeSizeAxes = Axes.Both;
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            InternalChild = Box = new Container
            {
                RelativeSizeAxes = Axes.Both,
                Children = new Drawable[]
                {
                }
            };
        }

        public void AddParticle(Vector2 NewPosition)
        {
            if (particleFrequencyCount > 15)
            {
                Box.Add(new Particle()
                {
                    Position = NewPosition
                });
                particleFrequencyCount = 0;
            }
            else
            {
                //particleFrequencyCount++;
            }
        }
    }
}
