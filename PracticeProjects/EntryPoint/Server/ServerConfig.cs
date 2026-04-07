namespace EntryPoint.Server
{
    public class ServerConfig
    {
        public string Mode { get; set; } = "dev";
        public int Port { get; set; }
        public bool Debug { get; set; }
        public string ConfigPath { get; set; }

        public int MaxPlayers { get; set; } = 100;
        public string ServerName { get; set; } = "Default Server";
        public float TickRate { get; set; } = 20.0f;
    }
}
