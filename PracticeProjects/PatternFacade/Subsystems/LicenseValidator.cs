using System;

namespace PatternFacade.Subsystems
{
    public class LicenseValidator
    {
        public bool ValidateLicense(out string errorMessage)
        {
            errorMessage = "";
            Console.WriteLine("[License] Проверка лицензии...");

            bool isValid = DateTime.Now.Day != 1;

            if (!isValid)
                errorMessage = "Истекла лицензия";

            return isValid;
        }
    }
}
