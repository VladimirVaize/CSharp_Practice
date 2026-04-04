namespace PatternFacade.Facade
{
    public class GameResult
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public SaveData LoadedSave { get; private set; }

        public static GameResult Success(SaveData save) =>
            new GameResult { IsSuccess = true, LoadedSave = save };

        public static GameResult Failure(string error) =>
            new GameResult { IsSuccess = false, ErrorMessage = error };
    }
}
