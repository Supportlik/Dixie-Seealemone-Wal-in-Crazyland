using System;
using System.Collections.Generic;
using Godot;
using MasterofElements.scripts.singletons;

public partial class AudioBusUI : VBoxContainer{
    private AutoLoader _autoLoader;

    private Texture2D _masterLabelSprite = GD.Load<Texture2D>("res://assets/sprites/ui/Master_Labelx200.png");
	private Texture2D _musicLabelSprite = GD.Load<Texture2D>("res://assets/sprites/ui/Music_Labelx200.png");
	private Texture2D _sfxLabelSprite = GD.Load<Texture2D>("res://assets/sprites/ui/SFX_Labelx200.png");
    public override void _Ready()
    {
        _autoLoader = new AutoLoader(this);
        GenerateAudioBusUIElement();
    }

    private void GenerateAudioBusUIElement(){
        AudioBusUIElement masterBusUIElement = new AudioBusUIElement("Master",_masterLabelSprite,_autoLoader.AudioService.GetMasterVolume());
        AddChild(masterBusUIElement);
        AudioBusUIElement musicBusUIElement = new AudioBusUIElement("Music",_musicLabelSprite,_autoLoader.AudioService.GetMusicVolume());
        AddChild(musicBusUIElement);
        AudioBusUIElement sfxBusUIElement = new AudioBusUIElement("SFX",_sfxLabelSprite,_autoLoader.AudioService.GetSFXVolume());
        AddChild(sfxBusUIElement);
    }

    public partial class AudioBusUIElement : VBoxContainer{

        private AutoLoader _autoLoader;

        private string _name;
        private const int MAXVALUE = 1;
        public TextureRect BusLabel;
        public HSlider BusSlider;

        public VBoxContainer Container;

        private PackedScene _busSliderScene;
        private PackedScene _conatainerScene;
        public AudioBusUIElement(string name, Texture2D label, float sliderValue){
            _name = name;
            _conatainerScene = GD.Load<PackedScene>("res://scenes/ui/audio_ui_container.tscn");
            Container = _conatainerScene.Instantiate<VBoxContainer>();
            AddChild(Container);
            InitSlider(sliderValue);
            InitLabel(label);
            Control control = new Control();
            control.AddChild(BusLabel);
            Container.AddChild(control);
            Container.AddChild(BusSlider);
        }

        public override void _Ready()
        {
            _autoLoader = new AutoLoader(this);
            BusSlider.ValueChanged += OnSliderChanged;
        }

        public void InitSlider(float sliderValue){
            _busSliderScene = GD.Load<PackedScene>("res://scenes/ui/sprite_slider.tscn");
            BusSlider = _busSliderScene.Instantiate<HSlider>();
            BusSlider.MaxValue = MAXVALUE;
            BusSlider.MinValue = 0;
            BusSlider.Step = 0.01;
            BusSlider.Value = sliderValue;
        }

        private void InitLabel(Texture2D label){
            BusLabel = new TextureRect();
            BusLabel.Texture = label;
        }
        

        private void OnSliderChanged(double value)
        {
            switch (_name)
            {
                case "Master":
                // GD.Print(value);
                _autoLoader.AudioService.SetMasterVolume((float)value);
                break;
                case "Music":
                _autoLoader.AudioService.SetMusicVolume((float)value);
                break;
                case "SFX":
                _autoLoader.AudioService.SetSFXVolume((float)value);
                break;
                default:
                break;
            }
        }
    }
}