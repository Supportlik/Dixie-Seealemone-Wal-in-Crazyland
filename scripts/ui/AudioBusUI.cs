using System;
using System.Collections.Generic;
using Godot;
using MasterofElements.scripts.singletons;

public partial class AudioBusUI : VBoxContainer{

    public Label audioLabel;

    private AutoLoader _autoLoader;
    public AudioBusUI(){
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
        private const int MAXVALUE = 100;
        public Label BusLabel;
        public HSlider BusSlider;
        public AudioBusUIElement(string label, float initialDb){
            _autoLoader = new AutoLoader(this);
            Name = "AudioBusSettings"+label;
            BusLabel = new Label();
            BusLabel.Text = label;
            AddChild(BusLabel);
            BusSlider = new HSlider();
            BusSlider.MaxValue = MAXVALUE;
            BusSlider.Value = 100f * (float)Math.Pow(10, initialDb / 20f);
            BusSlider.ValueChanged += OnSliderChanged;
            AddChild(BusSlider);
        }

        private void OnSliderChanged(double value)
        {
            float decibelValue = (float)Math.Log10(value/100) * 20;
            switch (BusLabel.Text)
            {
                case "Master":
                _autoLoader.AudioService.SetMasterVolume(decibelValue);
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