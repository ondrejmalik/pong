// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Localisation;

namespace TemplateGame.Game;

public partial class MenuButton : Button
{
    private Sprite sprite;
    private Box background;
    public SpriteText SpriteText;
    private LocalisableString text;
    private Colour4 boxColour;
    private Texture texture;

    public MenuButton()
    {
        Origin = Anchor.TopCentre;
        Anchor = Anchor.TopCentre;
        Size = new osuTK.Vector2(400, 100);
        boxColour = Colour4.MediumPurple;
    }

    [BackgroundDependencyLoader]
    private void load(TextureStore textures)
    {
        Add(
            background = new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Colour = boxColour
            });
        Add(
            SpriteText = new SpriteText()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = text,
                Font = new FontUsage(size: this.Height * 0.6f)
            });

        if (texture != null)
        {
            Add(
                sprite = new Sprite()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                    Texture = textures.Get("MenuButton.icon")
                });
        }
    }

    protected override void LoadComplete()
    {
        if (sprite != null) sprite.Texture = texture;
        base.LoadComplete();
    }

    public LocalisableString Text
    {
        get => SpriteText?.Text ?? default;
        set
        {
            text = value;
        }
    }

    public Colour4 BoxColour
    {
        get => background?.Colour ?? default;
        set
        {
            try
            {
                background.Colour = value;
            }
            catch (Exception e)
            {
                boxColour = value;
            }
        }
    }

    public Texture Texture
    {
        get => sprite.Texture;
        set
        {
            if (value != null) texture = value;
        }
    }
}
