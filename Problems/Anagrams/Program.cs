using Microsoft.VisualBasic;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Anagrams
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] strs = ["eat", "tea", "tan", "ate", "nat", "bat"];
            var output = Solution2.GroupAnagrams(strs);
            foreach(var list in output)
            {
                Console.Write("{");
                foreach (var word in list)
                {
                    Console.Write($",{word}");
                }
                Console.WriteLine("}");
            }
            
        }
    }
    /*
     *  Given an array of strings strs, group the anagrams => (words come from the same characters)
        together.You can return the answer in any order.

        Example 1:

        Input: strs = ["eat", "tea", "tan", "ate", "nat", "bat"]

        Output: [["bat"],["nat", "tan"],["ate", "eat", "tea"]]
    */
    #region Solution1
    public static class Solution
    {
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            if (strs.Length == 0) return [[""]];
            /*Dictionary<string, IList<string>> anagrams = new Dictionary<string, IList<string>>();*/
            IList<IList<string>> anagrams = new List<IList<string>>();
            for (int i = 0; i < strs.Length; i++)
            {
                string word = strs[i];
                bool flag = false;
                if (anagrams.Count == 0)
                {
                    anagrams.Add([word]);
                    continue;
                }
                Dictionary<char, int> wordChars = new Dictionary<char, int>();
                foreach (char c in word)
                {
                    if (wordChars.ContainsKey(c))
                    {
                        wordChars[c]++;
                    }
                    else
                    {
                        wordChars[c] = 1;
                    }
                }
                foreach (IList<string> key in anagrams)
                {
                    if (!(word.Length == key[0].Length)) continue;
                    
                    if (wordChars.CompareStrings(key[0]))
                    {
                        key.Add(word);
                        flag = true;
                    }
                }
                if (!flag) {
                    anagrams.Add([word]);
                }
            }
           return anagrams;
        }
        public static bool CompareStrings(this Dictionary<char, int> wordChars, string key)
        {
            
            foreach (char ch in key)
            {
                if (!wordChars.ContainsKey(ch)) return false;
                if(-- wordChars[ch] < 0) return false;
            }
            return true;
        }
    }
    #endregion
    public static class Solution2
    {
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            
            Dictionary<string,IList<string>> anagrams = new Dictionary<string,IList<string>>();
            List<string> keys = new();
            IList<IList<string>> result = new List<IList<string>>();
            foreach (string str in strs)
            {
                char[] word = str.ToCharArray();
                Array.Sort(word);
                string key = new string(word);
                if (anagrams.ContainsKey(key))
                {
                    anagrams[key].Add(str);
                }
                else
                {
                    anagrams[key] = [$"{str}"];
                    keys.Add(key);
                }

            }
            foreach (string key in keys) {
                result.Add(anagrams[key]);
            }
            return result;
        }
    }
}
