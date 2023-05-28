// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.IO;
using Newtonsoft.Json;
using UdpTest.Game;

public class SettingsLoader
{
    private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + "\\pong";
    private static string file = "\\settings.txt";

    public static void SaveSettings(GameSettings settings)
    {
        string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        File.WriteAllText(path + file, json);
    }

    public static GameSettings Load()
    {
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        if (File.Exists(path + file) == false)
        {
            File.Create(path + file);
        }

        StreamReader sr = new StreamReader(path + file);
        string input = sr.ReadToEnd();
        return JsonConvert.DeserializeObject<GameSettings>(input);
    }
}
