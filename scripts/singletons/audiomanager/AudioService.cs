using System;
using System.Collections.Generic;
using Godot;
using MasterofElements.scripts.singletons.fileaccess;

namespace MasterofElements.scripts.singletons.audiomanager;

public partial class AudioService : Node
{
    private AutoLoader _autoLoader;
    private AudioOptions _audioOptions;

    private AudioLibrary _musicLibrary;

    private AudioLibrary _sfxLibrary;

    private AudioBus _audioBus;

    private const string MasterBus = "Master";
    private const string MusicBus = "Music";
    private const string SfxBus = "SFX";


    public override void _Ready()
    {
        GD.Print("Hi");
        _autoLoader = new AutoLoader(this);

        _setUpAudio();
    }

    private void _setUpAudio()
    {
        _audioOptions = _getAudioOptionsFromFile();

        AudioServer.SetBusLayout(GD.Load<AudioBusLayout>("res://default_bus_layout.tres"));
        _musicLibrary = new AudioLibrary(MusicBus, new Dictionary<string, AudioStream>());
        _musicLibrary.SetAudioStreamsFromDirectory("res://assets/audio/music/");
        _sfxLibrary = new AudioLibrary(SfxBus, new Dictionary<string, AudioStream>());
        _sfxLibrary.SetAudioStreamsFromDirectory("res://assets/audio/sfx/");
        _audioBus = new AudioBus();

        SetMasterMuted(_audioOptions.MasterMute);
        SetMusicMuted(_audioOptions.MusicMute);
        SetSFXMuted(_audioOptions.SfxMute);

        SetMasterVolume(_audioOptions.MasterVolume);
        SetMusicVolume(_audioOptions.MusicVolume);
        SetSFXVolume(_audioOptions.SfxVolume);
    }

    private AudioOptions _getAudioOptionsFromFile()
    {
        AudioOptions audioOptions = _autoLoader.FileAccessService.ReadObject<AudioOptions>(UserFiles.AudioOptionsFile);
        if (audioOptions == null)
        {
            audioOptions = new AudioOptions(false, 0.5f, false, 0.5f, false, 0.5f);
            _audioOptions = audioOptions;
            _persistAudioOptions();
        }

        return audioOptions;
    }


    public void SetMusicVolume(float volume)
    {
        var newVolume = Math.Clamp(volume, 0, 1);
        _audioBus.SetBusVolume(MusicBus, newVolume);
        _audioOptions.MusicVolume = newVolume;
        _autoLoader.FileAccessService.WriteObject(UserFiles.AudioOptionsFile, _audioOptions);
    }

    public float GetMusicVolume()
    {
        return _audioOptions.MusicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        var newVolume = Math.Clamp(volume, 0, 1);
        _audioBus.SetBusVolume(SfxBus, newVolume);
        _audioOptions.SfxVolume = newVolume;
        _autoLoader.FileAccessService.WriteObject(UserFiles.AudioOptionsFile, _audioOptions);
    }

    public float GetSFXVolume()
    {
        return _audioOptions.SfxVolume;
    }

    public void SetMasterVolume(float volume)
    {
        var newVolume = Math.Clamp(volume, 0, 1);
        _audioBus.SetBusVolume(SfxBus, newVolume);
        _audioOptions.MasterVolume = newVolume;
        _autoLoader.FileAccessService.WriteObject(UserFiles.AudioOptionsFile, _audioOptions);
    }

    public float GetMasterVolume()
    {
        return _audioOptions.MasterVolume;
    }

    public bool IsMasterMuted()
    {
        return _audioOptions.MasterMute;
    }

    public bool IsMusicMuted()
    {
        return _audioOptions.MusicMute;
    }

    public bool IsSFXMuted()
    {
        return _audioOptions.SfxMute;
    }

    public void SetMasterMuted(bool mute)
    {
        _audioBus.SetBusMute(MasterBus, mute);
        _audioOptions.MasterMute = mute;
    }

    public void SetMusicMuted(bool mute)
    {
        _audioBus.SetBusMute(MusicBus, mute);
        _audioOptions.MusicMute = mute;
    }

    public void SetSFXMuted(bool mute)
    {
        _audioBus.SetBusMute(SfxBus, mute);
        _audioOptions.SfxMute = mute;
    }

    private void _persistAudioOptions()
    {
        _autoLoader.FileAccessService.WriteObject(UserFiles.AudioOptionsFile, _audioOptions);
    }

    public AudioStreamPlayer PlaySfx(string streamName, Node root, string playKey = null)
    {
        return _playAudio(streamName, _sfxLibrary, root, playKey);
    }

    public AudioStreamPlayer PlayMusic(string streamName, Node root, string playKey = null)
    {
        return _playAudio(streamName, _musicLibrary, root, playKey);
    }

    public void StopPlayBackAndQueueFree(Node root, string playKey)
    {
        if (playKey == null)
            return;

        var audioStreamPlayer = root.GetNodeOrNull<AudioStreamPlayer>(playKey);
        if (audioStreamPlayer == null)
            return;

        audioStreamPlayer.Stop();
        audioStreamPlayer.EmitSignal("finished");
        audioStreamPlayer.QueueFree();
    }


    private AudioStreamPlayer _playAudio(string streamName, AudioLibrary audioLibrary, Node root, string playKey)
    {
        if (playKey == null)
        {
            playKey = $"{audioLibrary.GetBusName()}_{streamName}_{Guid.NewGuid().ToString()}";
        }
        else
        {
            var audioStreamPlayer = root.GetNodeOrNull<AudioStreamPlayer>(playKey);
            if (audioStreamPlayer != null)
            {
                return audioStreamPlayer;
            }
        }

        AudioStreamPlayer asp = new AudioStreamPlayer();
        AudioStream stream = audioLibrary.GetAudioStream(streamName);

        asp.Stream = stream;

        asp.Name = playKey;
        asp.Bus = audioLibrary.GetBusName();

        root.AddChild(asp);

        asp.Play();
        asp.Connect("finished", Callable.From(() => asp.QueueFree()));
        return asp;
    }
}