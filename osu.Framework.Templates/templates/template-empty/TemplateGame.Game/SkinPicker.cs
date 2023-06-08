// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Logging;
using osuTK;
using osuTK.Graphics;

namespace TemplateGame.Game;

public partial class SkinPicker : CompositeDrawable
{
    public MenuButton select1;
    public MenuButton select2;
    public MenuButton select3;
    public MenuButton select4;
    int selectedSkin = 0;

    public int SelectedSkin => selectedSkin;

    public SkinPicker()
    {
        AutoSizeAxes = Axes.Both;
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
                select1 = new MenuButton()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(100, 100),
                    Position = new Vector2(-300, 0),
                    BoxColour = Color4.Red,
                },
                select2 = new MenuButton()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(100, 100),
                    Position = new Vector2(-100, 0),

                    BoxColour = Color4.Blue,
                },
                select3 = new MenuButton()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(100, 100),
                    Position = new Vector2(100, 0),

                    BoxColour = Color4.Purple,
                },
                select4 = new MenuButton()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(100, 100),
                    Position = new Vector2(300, 0),
                    BoxColour = Color4.FloralWhite,
                },
            }
        };
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        hoverCheck();
        return base.OnMouseDown(e);
    }

    protected override bool OnTouchDown(TouchDownEvent e)
    {
        hoverCheck();

        return base.OnTouchDown(e);
    }

    private void hoverCheck()
    {
        if (select1.IsHovered)
        {
            selectedSkin = 0;
        }

        if (select2.IsHovered)
        {
            selectedSkin = 1;
        }

        if (select3.IsHovered)
        {
            selectedSkin = 2;
        }

        if (select4.IsHovered)
        {
            selectedSkin = 3;
        }

        Logger.Log($"Selected skin: {selectedSkin}");
    }
}
