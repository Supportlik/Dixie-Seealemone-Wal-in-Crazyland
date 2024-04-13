using Godot;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class AudioLibrary{

    private Dictionary<string,AudioStream> _audioStreams;

    private string _busName;

    public AudioLibrary(string busName, Dictionary<string,AudioStream> audioStreams, string busSend = "Master")
    {
        AudioServer.AddBus();
		AudioServer.SetBusName(AudioServer.BusCount-1, busName);
        AudioServer.SetBusSend(AudioServer.GetBusIndex(busSend), busSend);
        _busName = busName;
        _audioStreams = audioStreams;
    }

    public void AddAudioStream(string streamName, AudioStream audioStream){
        _audioStreams.Add(streamName,audioStream);
    }

    public void SetAudioStreams(Dictionary<string,AudioStream> audioStreams, bool replace){
        if(replace){
            _audioStreams = audioStreams;
        }
        else{
            _audioStreams = _audioStreams.Concat(audioStreams).ToDictionary(x=>x.Key,x=>x.Value);
        }
    }

    public void SetAudioStreamsFromDirectory(string path){
        using var dir = DirAccess.Open(path);
		if (dir != null)
		{
			dir.ListDirBegin();
			string fileName = dir.GetNext();
			while (fileName != "")
			{
				if (dir.CurrentIsDir())
				{
					GD.Print($"Found directory: {fileName}");
				}
				else
				{
					GD.Print($"Found file: {fileName}");
					string ext = Path.GetExtension(fileName);
					if(ext == ".mp3" || ext == ".wav"){
                        GD.Print($"AudioStream file from: {path+fileName}");
						AddAudioStream(fileName,GD.Load<AudioStream>(path+fileName));
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

    public AudioStream GetAudioStream(string streamName){
        _audioStreams.TryGetValue(streamName, out AudioStream stream);
        return stream;
    }

    public string GetBusName(){
        return _busName;
    }
}