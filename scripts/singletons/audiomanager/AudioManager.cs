using Godot;
using System.Collections.Generic;

public partial class AudioManager : Node
{
	private AudioLibrary _musicLibrary;

	private AudioLibrary _sfxLibrary;

	public override void _Ready()
	{
		AudioServer.SetBusLayout(GD.Load<AudioBusLayout>("res://default_bus_layout.tres"));

		_musicLibrary = new AudioLibrary("Music", new Dictionary<string, AudioStream>());
		_musicLibrary.SetAudioStreamsFromDirectory("res://assets/audio/music/");
		_sfxLibrary = new AudioLibrary("SFX", new Dictionary<string, AudioStream>());
		_sfxLibrary.SetAudioStreamsFromDirectory("res://assets/audio/sfx/");
	}

	public async void PlaySFX(string streamName)
	{
		AudioStreamPlayer asp = new AudioStreamPlayer();

		AudioStream stream = _sfxLibrary.GetAudioStream(streamName);

		asp.Stream = stream;

		asp.Name = streamName+"_"+_sfxLibrary.GetBusName();
		asp.Bus = _sfxLibrary.GetBusName();

		AddChild(asp);
		asp.Play();

		await ToSignal(asp, "finished");
		asp.QueueFree();
	}

	public async void PlayMusic(string streamName)
	{
        GD.Print("Play music!");
		AudioStreamPlayer asp = new AudioStreamPlayer();

		AudioStream stream = _musicLibrary.GetAudioStream(streamName);

		asp.Stream = stream;

		asp.Name = streamName+"_"+_musicLibrary.GetBusName();
		asp.Bus = _musicLibrary.GetBusName();

		AddChild(asp);
		asp.Play();

		await ToSignal(asp, "finished");
		asp.QueueFree();
	}

}
