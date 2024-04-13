using System;
using System.Collections.Generic;
using Godot;

public class AudioBus
{
    private static Dictionary<string, int> _audioBuses;

    public AudioBus()
    {
        _audioBuses = new Dictionary<string, int>();
        SetAudioBuses();
    }

    public void SetAudioBuses()
    {
        var busCount = AudioServer.BusCount;
        for (var i = 0; i < busCount; i++)
        {
            var busName = AudioServer.GetBusName(i);
            _audioBuses.TryAdd(busName, i);
        }
    }

    public static Dictionary<string, int> GetAudioBuses()
    {
        return _audioBuses;
    }

    public void SetBusVolume(string busName, float volume)
    {
        var newVolume = Math.Clamp(volume, 0, 1);
        var decibelValue = (float)Math.Log10(newVolume) * 20;
        AudioServer.SetBusVolumeDb(_audioBuses[busName], decibelValue);
    }

    public void SetBusMute(string busName, bool enable)
    {
        AudioServer.SetBusMute(_audioBuses[busName], enable);
    }
}