using System;
using System.Media;
using NAudio;
using NAudio.CoreAudioApi;
using NAudio.Wave;

public class PlayToDevice
{
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: PlayToDevice <sound_file_path>");
            return;
        }

        string filePath = args[0];

        var enumerator = new MMDeviceEnumerator();
        var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Communications);

        using(var audioFile = new AudioFileReader(filePath))
        using(var outputDevice = new WasapiOut(device, AudioClientShareMode.Shared, true, 0))
        {
            outputDevice.Init(audioFile);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(500);
            }
        }
    }
}
