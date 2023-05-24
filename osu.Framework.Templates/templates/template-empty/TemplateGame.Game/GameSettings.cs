namespace a;

public class GameSettings
{
    public static int SoundVolume { get; set; }
    public static float BallSpeed { get; set; }
    public static float PaddleSize { get; set; }
    public static bool EnableParticles { get; set; }
    public static int ScoreLimit { get; set; }
    public static int BackgroundColour { get; set; }
    public static int BallColour { get; set; }
    public static int PaddleColour { get; set; }

    public static string ToString()
    {
        return $"{GameSettings.SoundVolume},{GameSettings.BallSpeed},{GameSettings.PaddleSize},{GameSettings.EnableParticles},{GameSettings.ScoreLimit},{GameSettings.BackgroundColour},{GameSettings.BallColour},{GameSettings.PaddleColour}";
    }
}
