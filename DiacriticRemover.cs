using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CzechToUtf8
{
    public sealed class DiacriticRemover
    {
        private static readonly Lazy<DiacriticRemover> lazyInstance = new Lazy<DiacriticRemover>(() => new DiacriticRemover());
        public DiacriticRemover(){}
        public static DiacriticRemover Instance { get { return lazyInstance.Value; } }


        public static string RemoveDiacritics(string text)
        {
            var replacements = new Dictionary<char, char>
        {
            {'á', 'a'},
            {'č', 'c'},
            {'ď', 'd'},
            {'é', 'e'},
            {'ě', 'e'},
            {'í', 'i'},
            {'ň', 'n'},
            {'ó', 'o'},
            {'ř', 'r'},
            {'š', 's'},
            {'ť', 't'},
            {'ú', 'u'},
            {'ů', 'u'},
            {'ý', 'y'},
            {'ž', 'z'},
        };

            var stringBuilder = new StringBuilder();
            foreach (char c in text)
            {
                if (replacements.TryGetValue(char.ToLower(c), out char replacement))
                    stringBuilder.Append(char.IsUpper(c) ? char.ToUpper(replacement) : replacement);
                else
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

    }
}
