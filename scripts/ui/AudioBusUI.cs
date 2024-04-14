using System;
using System.Collections.Generic;
using Godot;
using MasterofElements.scripts.singletons;

public partial class AudioBusUI : VBoxContainer{

    public Label audioLabel;
    private AutoLoader _autoLoader;
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        Name = "AudioSettings";
        audioLabel = new Label();
        audioLabel.Name = Name +"Title";
        audioLabel.Text = "Audio ";
        AddChild(audioLabel);
        GenerateAudioBusUIElement();
    }

    private void GenerateAudioBusUIElement(){
        AudioBusUIElement masterBusUIElement = new AudioBusUIElement("Master",_autoLoader.AudioService.GetMasterVolume());
        AddChild(masterBusUIElement);
        AudioBusUIElement musicBusUIElement = new AudioBusUIElement("Music",_autoLoader.AudioService.GetMusicVolume());
        AddChild(musicBusUIElement);
        AudioBusUIElement sfxBusUIElement = new AudioBusUIElement("SFX",_autoLoader.AudioService.GetMusicVolume());
        AddChild(sfxBusUIElement);
    }

    public partial class AudioBusUIElement : VBoxContainer{

        private AutoLoader _autoLoader;
        private const int MAXVALUE = 1;
        public Label BusLabel;
        public HSlider BusSlider;

        private PackedScene _busSliderScene;
        public AudioBusUIElement(string label, float initialDb){
            _busSliderScene = GD.Load<PackedScene>("res://scenes/ui/sprite_slider.tscn");
            BusLabel = new Label();
            BusLabel.Text = label;
            AddChild(BusLabel);
            BusSlider = _busSliderScene.Instantiate<HSlider>();
            BusSlider.MaxValue = MAXVALUE;
            BusSlider.Value = (float)Math.Pow(10, initialDb / 20f);
            BusSlider.Step = 0.01;
            AddChild(BusSlider);
        }

        public override void _Ready()
        {
            _autoLoader = new AutoLoader(this);
            BusSlider.ValueChanged += OnSliderChanged;
        }

        private void OnSliderChanged(double value)
        {
            float decibelValue = (float)Math.Log10(value) * 20;
            switch (BusLabel.Text)
            {
                case "Master":
                GD.Print(value);
                _autoLoader.AudioService.SetMasterVolume((float)value);
                GD.Print(AudioServer.GetBusVolumeDb(0));
                break;
                case "Music":
                _autoLoader.AudioService.SetMusicVolume(decibelValue);
                break;
                case "SFX":
                _autoLoader.AudioService.SetSFXVolume(decibelValue);
                break;
                default:
                break;
            }
        }
    }
}