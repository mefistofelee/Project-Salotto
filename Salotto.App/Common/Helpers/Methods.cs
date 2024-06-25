using System.Collections.Generic;

namespace Salotto.App.Common.Helpers
{
    public class Methods
    {
        public static List<string> GetGender()
        {
            return new List<string>()
            {
                "Male", "Female", "Prefer not to say"
            };
        }

        public static List<string> GetDocumentType()
        {
            return new List<string>() { "Identity Card Number (for EU/EEA/Swiss citizens)", "Passport" };
        }
        public static List<string> GetDocumentStatus()
        {
            return new List<string>() { "Confirmed", "Voided" };
        }
    }
}
