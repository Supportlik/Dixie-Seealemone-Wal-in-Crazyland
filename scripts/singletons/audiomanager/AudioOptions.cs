namespace MasterofElements.scripts.singletons.audiomanager;

public class AudioOptions
{
    public bool MasterMute { get; set; }
    public float MasterVolume { get; set; }

    public bool MusicMute { get; set; }
    public float MusicVolume { get; set; }

    public bool SfxMute { get; set; }
    public float SfxVolume { get; set; }

    public AudioOptions(bool masterMute, float masterVolume, bool musicMute, float musicVolume, bool sfxMute,
        float sfxVolume)
    {
        MasterMute = masterMute;
        MasterVolume = masterVolume;
        MusicMute = musicMute;
        MusicVolume = musicVolume;
        SfxMute = sfxMute;
        SfxVolume = sfxVolume;
    }
}