using System;

namespace PatternFacade.Subsystems
{
    public class NetworkService
    {
        private bool _isConnected = false;

        public bool ConnectToServer(out string errorMessage)
        {
            errorMessage = "";
            Console.WriteLine("[Network] Подключение к серверу...");

            _isConnected = true;
            return true;
        }

        public void Disconnect()
        {
            if (_isConnected)
            {
                Console.WriteLine("[Network] Отключение от сервера...");
                _isConnected = false;
            }
        }
    }
}
