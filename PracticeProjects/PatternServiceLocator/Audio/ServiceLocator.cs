using System;

namespace PatternServiceLocator.Audio
{
    public static class ServiceLocator
    {
        private static IAudioService _audioService;
        private static bool _isRegistered = false;

        public static void RegisterAudioService(IAudioService service)
        {
            _audioService = service ?? throw new ArgumentNullException(nameof(service));
            _isRegistered = true;
        }

        public static IAudioService GetAudioService()
        {
            if (!_isRegistered)
                throw new InvalidOperationException("Audio service not registered. Call RegisterAudioService first.");

            return _audioService;
        }
    }
}
