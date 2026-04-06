using System;

namespace PatternServiceLocator.Audio
{
    public class SoundManager : IAudioService
    {
        private float _currentVolume;

        public SoundManager() => _currentVolume = 0.2f;

        public void PlaySound(string soundName)
        {
            Console.WriteLine($"Воспроизведение звука: {soundName}");
        }

        public void SetVolume(float volume)
        {
            if (volume <= 1 && volume >= 0)
            {
                _currentVolume = volume;
                Console.WriteLine($"Громкость установлена на {volume * 100}%");
            }
        }
    }
}
