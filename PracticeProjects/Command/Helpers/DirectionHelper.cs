namespace Command.Helpers
{
    public static class DirectionHelper
    {
        public static string GetDirectionName(int dx, int dy)
        {
            if (dy >= 1)
            {
                if (dx >= 1)
                    return "северо-восток";
                if (dx <= -1)
                    return "северо-запад";
                return "север";
            }
            if (dy <= -1)
            {
                if (dx >= 1)
                    return "юго-восток";
                if (dx <= -1)
                    return "юго-запад";
                return "юг";
            }
            if (dx >= 1 && dy == 0) return "восток";
            if (dx <= -1 && dy == 0) return "запад";
            return "неизвестно";
        }
    }
}
