using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex = Nituch.vertexForNituch.Vertex;
using StatusV = Nituch.vertexForNituch.StatusV;
using MyScraperProject;

namespace Nituch
{
    public class functionForLangAuto
    {
        private static readonly Dictionary<char, int> _letterHash = new()
        {
            { 'א', 0 },
            { 'ה', 0 },
            { 'ע', 0 },
            { 'ב', 1 },
            { 'ג', 2 },
            { 'ד', 3 },
            { 'ו', 4 },
            { 'ז', 5 },
            { 'ח', 6 },
            { 'כ', 6 },
            { 'ך', 6 },
            { 'ק', 6 },
            { 'ט', 7 },
            { 'ת', 7 },
            { 'י', 8 },
            { 'ל', 9 },
            { 'מ', 10 },
            { 'ם', 10 },
            { 'נ', 11 },
            { 'ן', 11 },
            { 'ס', 12 },
            { 'ש', 12 },
            { 'פ', 13 },
            { 'ף', 13 },
            { 'צ', 14 },
            { 'ץ', 14 },
            { 'ר', 15 }

        };

        public static int HashFun(char c) => _letterHash[c];
       

        public static Vertex SearchWordAutoLang(string str, Vertex root, ref int ind)
        {
            
            int sum = 0;
            Vertex v = root;
            ind = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int index = HashFun(str[i]); // Use hash function 
                if (v.Edges[index] == null)
                {
                    return null; // Word does not exist
                }
                v = v.Edges[index];
                sum += v.degel;// Navigate to the next vertex
            }
            //במצב מקבל יכולות לחכות  לי כמה מילים
            if (v.Status == StatusV.ACCEPT)
            {
                for (int i = 0; i < v.FinalData.word.Count; i++)
                {
                    if (v.FinalData.word[i].Mila.Equals(str)) ind = i;
                }
                if (ind == 0) ErrorReporting(v.FinalData.word[0].Mila, str, sum);
                return v;
            }
            return null;
            // If reached the end vertex, the word exists
        }
        static void FreeMemory(Vertex v)
        {
            if (v == null) return;
            for (int i = 0; i < 16; i++)
            {
                FreeMemory(v.Edges[i]); // Free each edge
            }
        }
        public static void LoadWordsFromFileAutoLang(string filePath, Vertex root)
        {
            try
            {
                string[] words = File.ReadAllLines(filePath); // Read all lines from the file
                string b = "";
                foreach (string word in words)
                {
                    //if (!string.IsNullOrWhiteSpace(word)) // בדוק שאינה ריקה
                    //{ 
                    //    FIXMEINCSHARP.BuildTheAuto.build1(word, root); //
                    //}
                    b = TrimFinalNiqqud(word);
                    BuildTheAuto.build1(b, root);

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        }
        public static string TrimFinalNiqqud(string word)
        {
            if (string.IsNullOrEmpty(word)) return word;

            char last = word[word.Length - 1];

            // אם התו האחרון הוא **לא אות עברית** (כלומר ניקוד), נסיר אותו
            if (last < 'א' || last > 'ת')
                return word.Substring(0, word.Length - 1);

            return word;
        }
        //public static string RemoveNiqqud(string input)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    foreach (char c in input)
        //    {
        //        // תחום תווי ניקוד בעברית: U+0591 עד U+05C7
        //        if (c < '\u0591' || c > '\u05C7')
        //        {
        //            sb.Append(c);
        //        }
        //    }

        //    return sb.ToString();
        //}
        //public static string RemoveNiqqud(string input)
        //{
        //    StringBuilder sb = new StringBuilder();

        //    foreach (char c in input)
        //    {
        //        // דילוג על ניקוד, מקף ורווח
        //        if ((c < '\u0591' || c > '\u05C7') && c != '-' && c != ' ')
        //        {
        //            sb.Append(c);
        //        }
        //    }

        //    return sb.ToString();
        //}

        public static string RemoveNiqqud(string input)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in input)
            {
                // טווח האותיות העבריות: מ-א׳ (U+05D0) עד ת׳ (U+05EA)
                if (c >= '\u05D0' && c <= '\u05EA')
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        //        public static void ErrorReporting(string str, string strUser, int sum)
        //        {
        //            Error er = new Error();
        //            int i = 0; int j = 0;
        //            string chars = "אהעחטכתקסש";


        //            while (i < str.Length && j < strUser.Length)
        //            {
        //                if (str[i] != strUser[j])
        //                {
        //                    if (str[i] == 'י')
        //                    {
        //                        er.ErrorCode = 1;
        //                        i++;
        //                        Globals.errors.Add(new Error(1,""));
        //                        continue;
        //                    }
        //                    else
        //                    {
        //                        if (strUser[j] == 'י')
        //                        {
        //                            er.ErrorCode = (sum > 0) ? 3 : 2;
        //                            Globals.errors.Add(new Error(er.ErrorCode, ""));
        //                            j++;

        //                            continue;
        //                        }
        //                    }
        //                    if(chars.Contains( strUser[j]))
        //                    {
        //                        er.ErrorCode =4;
        //                        er.Description = strUser[j] + ":" + str[i]  ;
        //                        Globals.errors.Add(new Error(4 , strUser[j] + ":" + str[i]));

        //                    }

        //                }
        //                 i++;
        //                j++;
        //}


        //        }
        public static void ErrorReporting(string str, string strUser, int sum)
        {
            int i = 0, j = 0;
            string chars = "אהעחטכתקסש";
            HashSet<string> milim = new HashSet<string> { "אים", "עים", "מין", "מימני", "מימך", "מימך", "מימנו", "ממנה", "מיכם", "מיכן", "מיכאן" };

            while (i < str.Length && j < strUser.Length)
            {
                if (str[i] != strUser[j])
                {
                    if (str[i] == 'י')
                    {
                        Globals.errors.Add(new Error(1, "✏️ החסרת את האות י במיקום נדרש."));
                        i++;
                        continue;
                    }

                    if (strUser[j] == 'י')
                    {
                        Globals.errors.Add(new Error(
                        milim.Contains(strUser) ? 3 : 2,
                        milim.Contains(strUser) ? "⚠️ הוספת האות י במיקום זה אינה נכונה." : "✅ הוספת האות י תקינה ואינה שגיאה."));

                        j++;
                        continue;
                    }

                    if (chars.Contains(strUser[j]))
                    {
                        string desc = $"{strUser[j]}:{str[i]}";
                        Globals.errors.Add(new Error(4, desc));
                    }
                }

                i++;
                j++;
            }
        }

    }
}
