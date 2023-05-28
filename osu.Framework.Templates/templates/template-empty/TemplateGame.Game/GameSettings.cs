using System;

namespace UdpTest.Game;

public class GameSettings
{
    public static double SoundVolume { get; set; }
    public static float BallSpeed { get; set; }
    public static float PaddleSize { get; set; }
    public static bool EnableParticles { get; set; }
    public static int ScoreLimit { get; set; }
    public static int BackgroundColour { get; set; }
    public static int BallColour { get; set; }
    public static int PaddleColour { get; set; }

    public static string ToString()
    {
        return
            $"{GameSettings.SoundVolume};{GameSettings.BallSpeed};{GameSettings.PaddleSize};{GameSettings.EnableParticles};{GameSettings.ScoreLimit};{GameSettings.BackgroundColour};{GameSettings.BallColour};{GameSettings.PaddleColour}";
    }

    public static void SetDefaluts()
    {
        SoundVolume = 1;
        BallSpeed = 1f;
        PaddleSize = 7f;
        EnableParticles = true;
        ScoreLimit = 10;
        BackgroundColour = 0;
        BallColour = 0;
        PaddleColour = 0;
    }

    public static void SetSettings(string[] settings)
    {
        if (settings == null || settings.Length != 8)
        {
            SetDefaluts();
        }

        SoundVolume = Convert.ToDouble(settings[0]);
        BallSpeed = Convert.ToSingle(settings[1]);
        PaddleSize = Convert.ToSingle(settings[2]);
        EnableParticles = Convert.ToBoolean(settings[3]);
        ScoreLimit = Convert.ToInt32(settings[4]);
        BackgroundColour = Convert.ToInt32(settings[5]);
        BallColour = Convert.ToInt32(settings[6]);
        PaddleColour = Convert.ToInt32(settings[7]);
    }
}
