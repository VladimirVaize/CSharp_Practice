namespace PatternServiceLocator.Audio
{
    public interface IAudioService
    {
        void PlaySound(string soundName);
        void SetVolume(float volume);
    }
}
