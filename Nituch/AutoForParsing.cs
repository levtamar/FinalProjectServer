using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nituch;
using MyScraperProject;
using Nituch.vertexForNituch;



namespace Nituch
{
    public class AutoForParsing
    {
        // public enum StatusV { REGULAR, ACCEPT }
        //public class vertexP
        //{
        //    public vertexP[] Edges = new vertexP[5]; // מערך של 4 הפניות ל־vertex
        //    public int code;                 // קוד קודקוד
        //    public StatusV status;
        //    public string str;
        //    public vertexP(StatusV status)
        //    {
        //        this.status = status;
        //    }
        //    public vertexP(/*StatusV status*/  string str)
        //    {
        //       this.status = StatusV.ACCEPT; 
        //        this.Edges[4] = Globals.root;
        //        this.str = str;
        //    }
        //}
        // public static bool SearchWord(string str, vertexP root )
        // {
        //     vertexP v = root;
        //     for (int i = 0; i < str.Length; i++)
        //     {
        //         int index = str[i] - 'A';
        //         if (v.Edges[index] == null)
        //         {
        //             return false; // Word does not exist
        //         }
        //         v = v.Edges[index]; // Navigate to the next vertex
        //     }
        //     return (v.status == StatusV.ACCEPT);
        //}


        //public static string SortWord(string s)
        //{
        //    int[] arr = new int[5];
        //    string newStr = "";

        //    // ספירת התווים
        //    for (int i = 0; i < s.Length; i++)
        //    {
        //        arr[s[i] - 'A']++;
        //    }

        //    // בניית המחרוזת הממוינת
        //    for (int i = 0; i <5; i++)
        //    {
        //        for (int j = 0; j < arr[i]; j++)
        //        {
        //            newStr += (char)('A' + i);
        //        }
        //    }

        //    return newStr;
        //}
        public static string SortWord(string s)
        {
            StringBuilder result = new StringBuilder();
            int[] counts = new int[5]; // אינדקסים 0 = A, 1 = B, ..., 4 = E

            foreach (char c in s)
            {
                if (c == 'E')
                {
                    // מיון החלק הקודם והוספה
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < counts[i]; j++)
                        {
                            result.Append((char)('A' + i));
                        }
                    }
                    counts = new int[5]; // איפוס הספירה
                    result.Append('E'); // השארת ה-E
                }
                else
                {
                    counts[c - 'A']++;
                }
            }

            // מיון והוספה של החלק האחרון
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    result.Append((char)('A' + i));
                }
            }

            return result.ToString();
        }

        public static string SearchWord(string str, vertexP root)

        {
            vertexP v = root;
            string vvvv = "";
            int index;
            int counter = 1;
            int[] arr = new int[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                 index = str[i] - 'A';
                //if (index == 4)
                //{
                //    vvvv += v.str + "E";
                //}
                //if (v.Edges[index] == null)
                //{
                //    return null; // Word does not exist
                //}
                arr[i] = counter++;
                //if ((i > 0 && str[i] == 'B' && str[i - 1] == 'B') ||
                //    (i < str.Length - 1 && str[i] == 'C' && str[i - 1] == 'B'))
               
                //   arr[i] = arr[i-1];
                vvvv += (index == 4) ? v.str + "E" : "";
                if (v.Edges[index] == null) return null;
                v = v.Edges[index];
            }
           if (v.status == StatusV.ACCEPT)
                return vvvv + v.str;
            return null;
        }
        public static string SearchWordInWrongs(string str, vertexP root , string[] wordsInSub)
        {
            vertexP v = root;
            string vvvv = "";
            for (int i = 0; i < str.Length; i++)
            {
                int index = str[i] - 'A';
                if(index ==4)
                {
                    vvvv += v.str + "E";
                }
                if (v.Edges[index] == null)
                {
                    return null; // Word does not exist
                }
                v = v.Edges[index]; // Navigate to the next vertex
            }
            if (v.status == StatusV.ACCEPT)

                return vvvv+v.str;
            return null;
        }
        //public static bool SearchWordInWrongRoot(string str, vertexP root , out str)
        //{
        //    vertexP v = root;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        int index = str[i] - 'A';
        //        if (v.Edges[index] == null)
        //        {
        //            return false; // Word does not exi st
        //        }
        //        v = v.Edges[index];
        //        if(v.status == StatusV.ACCEPT)
        //        {
        //            str = v.str;
        //        }
        //        // Navigate to the next vertex
        //    }
        //   if(v.status != StatusV.ACCEPT)
        //    {

        //    }
        //}
        //public  static void LoadWordsFromFile(string filePath  , vertexP root)
        //{
        //    try
        //    {
        //        string[] words = File.ReadAllLines(filePath);
        //        foreach (string word in words)
        //        {
        //            //Console.WriteLine(word);   
        //            AddWord(word, root);
        //        }
        //    }
        //    catch (IOException ex)
        //    {
        //        Console.WriteLine("Error reading file: " + ex.Message);
        //    }
        //}
        public static void LoadWordsFromFile(string filePath, vertexP root , vertexP wrongRoot)
        {
            try
            {
                string[] words = File.ReadAllLines(filePath);
                for (int i = 0; i < words.Length; i+=2)
                {
                    //הוספת מילה לאוטמט הזיהוי
                    AddWord(words[i], root , words[i] , i);
                    //הובפת מילה לאוטמט התיקון  - שמירת במצב הסופי את המילה הנכונה 
                    AddWordWrongs(words[i+1], wrongRoot, words[i] , i+1);
                }
              
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
            }
        } 
        //public static void AddWord(string str, vertexP root, string wordToInsert , int ind)
        //{
        //    vertexP v = root;
        //    vertexP prev = null;
        //    for (int i = 0; i < str.Length; i++)
        //    {

        //        int index = str[i] - 'A';

        //        //    v.Edges[index] = (str[i] == 'B') ? v : (str[i] == 'C' ? prev : null);
        //        if (v.Edges[index] == null)
        //        {
        //            //if (str[i] == 'B')
        //            //{
        //            //    v.Edges[index] = v;    

        //            //}
        //            //if (str[i] == 'C')
        //            //{
        //            //    // קשת חזרה לקודקוד הקודם
        //            //    v.Edges[index] = prev;
        //            //}

        //            // v.Edges[index] = (str[i] == 'B') ? v : (str[i] == 'C' ? prev : null);
        //            if (str[i] == 'B')
        //            {
        //                v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert) : new vertexP(StatusV.REGULAR);
        //               // v.Edges[index] = v;
        //               v.Edges[index].Edges[1] = v.Edges[index];
        //                //if (i == str.Length - 1)
        //                //{
        //                //    v.status = StatusV.ACCEPT;
        //                //    v.str = wordToInsert;
        //                //}

        //            }
        //            else
        //            {
        //                if (str[i] == 'C')
        //                {
        //                    // קשת חזרה לקודקוד הקודם
        //                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert) : new vertexP(StatusV.REGULAR);
        //                    v.Edges[index].Edges[1] = prev;

        //                }
        //                else

        //                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert) : new vertexP(StatusV.REGULAR);
        //            }
        //        }
        //        prev = v;
        //        v = v.Edges[index];
        //    }
        //    }

        public static void AddWordWrongs(string str, vertexP root, string wordToInsert, int ind)
        {
            vertexP v = root;
            vertexP prev = null;
            for (int i = 0; i < str.Length; i++)
            {
                int index = str[i] - 'A';

                if (v.Edges[index] == null)
                {
                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert , true) : new vertexP(StatusV.REGULAR);
                }
                v = v.Edges[index];
            }

        }
        public static void AddWord(string str, vertexP root, string wordToInsert, int ind)
        {
            vertexP v = root;
            vertexP prev = null;
            int index;
            for (int i = 0; i < str.Length; i++)
            {
                 index = str[i] - 'A';
                if (v.Edges[index] == null)
                {
                    //יצירת קשת חדשה במידה וזה מצב סופי להכניס את המילה למצב המקבל.
                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert) : new vertexP(StatusV.REGULAR);
                    //רקרוסיה  
                    switch (str[i])
                    {//במקרה שזה ש"ע יצירת קשת סיבובית לאותו קודקוד
                        case 'B':  v.Edges[index].Edges[1] = v.Edges[index]; break;
                      //במקרה שזה תיאור יצרת קשת חוזרת לש"ע שאותו הוא מתאר
                        case 'C':   v.Edges[index].Edges[1] = prev; break;  
                    }
                }
                prev = v;
                v = v.Edges[index];
            }
        }




        //     פונקציה הוספת מילה לאוטמטים תוך מתן אפשרות ב2 האוטומטים
        //        public static void AddWordWrongs111(string str, vertexP root, string wordToInsert, int ind)

        //{
        //    vertexP v = root;
        //    vertexP prev = null;
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        int index = str[i] - 'A';
        //        if (v.Edges[index] == null)
        //        {
        //            //if (str[i] == 'B')
        //            //{
        //            //    v.Edges[index] = v;    

        //            //}
        //            //if (str[i] == 'C')
        //            //{
        //            //    // קשת חזרה לקודקוד הקודם
        //            //    v.Edges[index] = prev;
        //            //}

        //            // v.Edges[index] = (str[i] == 'B') ? v : (str[i] == 'C' ? prev : null);
        //            if (str[i] == 'B')
        //            {
        //                v.Edges[index] = v;
        //                if (i == str.Length - 1)
        //                {
        //                    v.status = StatusV.ACCEPT;
        //                    v.str = wordToInsert;
        //                }

        //            }
        //            else
        //            {
        //                if (str[i] == 'C')
        //                {
        //                    // קשת חזרה לקודקוד הקודם
        //                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert) : new vertexP(StatusV.REGULAR);
        //                    v.Edges[index].Edges[1] = prev;

        //                }
        //                else

        //                    v.Edges[index] = (i == str.Length - 1) ? new vertexP(wordToInsert, true) : new vertexP(StatusV.REGULAR);
        //            }
        //        }
        //        prev = v;
        //        v = v.Edges[index];
        //    }

    }
}



