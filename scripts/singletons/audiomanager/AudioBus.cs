using System.Collections.Generic;
using Godot;

public static class AudioBus{

    private static Dictionary<string,int> _audioBuses;
    static AudioBus(){
        _audioBuses = new Dictionary<string, int>();
        SetAudioBuses();
    }
    public static void SetAudioBuses(){
        int busCount = AudioServer.BusCount;
        for (int i = 0; i < busCount; i++)
        {
            string busName = AudioServer.GetBusName(i);
            _audioBuses.TryAdd(busName,i);
        }
    }
    public static Dictionary<string,int> GetAudioBuses(){
        return _audioBuses;
    }
    public static void SetBusVolume(string busName, float volumeDb){
        AudioServer.SetBusVolumeDb(_audioBuses[busName], volumeDb);
    }

    public static void SetBusMute(string busName, bool enable){
        AudioServer.SetBusMute(_audioBuses[busName], enable);
    }
}