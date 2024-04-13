using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

public class AudioLibrary
{
    private Dictionary<string, AudioStream> _audioStreams;

    private readonly string _busName;

    public AudioLibrary(string busName, Dictionary<string, AudioStream> audioStreams, string busSend = "Master")
    {
        AudioServer.AddBus();
        AudioServer.SetBusName(AudioServer.BusCount - 1, busName);
        AudioServer.SetBusSend(AudioServer.GetBusIndex(busSend), busSend);
        _busName = busName;
        _audioStreams = audioStreams;
    }

    private void AddAudioStream(string streamName, AudioStream audioStream)
    {
        _audioStreams.Add(streamName, audioStream);
    }

    public void SetAudioStreamsFromDirectory(string path)
    {
        using var dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            var fileName = dir.GetNext();
            while (fileName != "")
            {
                if (dir.CurrentIsDir())
                {
                    GD.Print($"Found directory: {fileName}");
                }
                else
                {
                    // GD.Print($"Found file: {fileName}");
                    var ext = Path.GetExtension(fileName);
                    if (ext is ".mp3" or ".wav")
                    {
                        // GD.Print($"AudioStream file from: {path + fileName}");
                        AddAudioStream(fileName, GD.Load<AudioStream>(path + fileName));
                    }
                }

                fileName = dir.GetNext();
            }
        }
        else
        {
            GD.Print("An error occurred when trying to access the path.");
        }
    }

    public AudioStream GetAudioStream(string streamName)
    {
        var isStreamPresent = _audioStreams.TryGetValue(streamName, out AudioStream stream);

        if (!isStreamPresent)
        {
            GD.PrintErr($"The stream {streamName} was not found in the library.");
        }

        return stream;
    }

    public string GetBusName()
    {
        return _busName;
    }
}