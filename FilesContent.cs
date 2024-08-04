using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace create_feature
{
    internal class FilesContent
    {
        public static string Request(string FeatureName)
        {
            StringBuilder request = new StringBuilder();
            request.Append($"using namespace {MakePlural(FeatureName)}");
            return request.ToString();
        }

        static string MakePlural(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            if (word.EndsWith("y") && word.Length > 1 && IsConsonant(word[word.Length - 2]))
            {
                return word.Substring(0, word.Length - 1) + "ies";
            }
            else if (word.EndsWith("s") || word.EndsWith("x") || word.EndsWith("z") ||
                     word.EndsWith("ch") || word.EndsWith("sh"))
            {
                return word + "es";
            }
            else
            {
                return word + "s";
            }
        }
        static bool IsConsonant(char c)
        {
            return !"aeiou".Contains(char.ToLower(c));
        }
    }
}
