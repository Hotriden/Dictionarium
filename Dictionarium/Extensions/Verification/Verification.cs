using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dictionarium.Extensions.Verification
{
    /// <summary>
    /// As web UI on core layer should be checks
    /// on validation date
    /// </summary>
    public static class Verification
    {
        static string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public static bool VerifyEmail(string email)
        {
            if (Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
            return false;
        }

        public static string VerificationMessage(string userName, string link)
        {
            string url = "https://haveToChangeit/";
            return $"Hello {userName}! There is just a final step to validate your account on <a href=\"{url}\">Dictionarium application</a>. Click on this " +
                $"verify <a href=\"{link}\">link</a> for finish your registration";
        }

        public static string RecoverMessage(string userName, string link)
        {
            string url = "https://haveToChangeit/";
            return $"Hello {userName}! For recover your password on <a href=\"{url}\">Dictionarium application</a>. Click on this " +
                $"<a href=\"{link}\">link</a>";
        }
    }
}
