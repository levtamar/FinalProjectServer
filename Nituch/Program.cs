using System;
using System.Net;
using HtmlAgilityPack;
using Nituch;
using MyScraperProject;
using vertexP = Nituch.vertexForNituch.vertexP;
using StatusV = Nituch.vertexForNituch.StatusV;
using Vertex = Nituch.vertexForNituch.Vertex;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nituch.vertexForNituch;
using System.Text.RegularExpressions;

namespace MyScraperProject
{
 public   class Program
    {
        static void Main(string[] args)
        {
            // string s = functionForNituch.FixWord("");
            FIRSTBUILD();
            //     Console.WriteLine(TextToText("בקבוק בקבוק שתו"));
            Console.WriteLine(TextToText("שתה אמא בקבוק אם אמא שתה בקבוק"));
            //    Console.WriteLine(TextToText("שתה בקבוק בקבוק"));
            Console.WriteLine(TextToText("אימונה"));
            // קריאה מקלט מהמשתמש
            //      Console.WriteLine("הכנס משפט:");

            //     string sub = Console.ReadLine();\
            //////מפה<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            //     string sub = "";
            //     sub = "בקבוק אמא בקבוק שתה";
            //     sub = "הלכה";
            //     sub = "חצאת מתוק אוכלים בקבוק אם חצאית מתוק אוכלים בקבוק";
            //    // sub = "שתה אימא בקבוק אים אמא שתה בקבוק";
            //     string[] wordsInSub = sub.Split(' ');
            //     WordInfo [] pointToWordsInSub = new WordInfo[wordsInSub.Length];    
            //     int ii = 0;
            //     Vertex temp = new Vertex(StatusV.REGULAR);
            //     string subStr = "";

            //     for (int i = 0; i < wordsInSub.Length; i++)
            //     {
            //         string text = "משהוכלב";
            //         string text1 = "";
            //         string w = wordsInSub[i];
            //         temp = functionForLangAuto.SearchWordAutoLang(w, rootLang, ref ii);
            //         while (text.Count() > 0 && temp == null && text.Contains(w[0]))
            //         {
            //             text = text.Replace(w[0], '\0');
            //             text1 += w[0];
            //             w = w.Substring(1);
            //             temp = functionForLangAuto.SearchWordAutoLang(w, rootLang, ref ii);
            //             //אם אתה מתחיל באחת מאויות השימוש תוריד אותה מהמחרוזת ומהמילה בעצמה ותשלח שוב את המילה

            //         }

            //         if (temp != null)
            //         {
            //             wordsInSub[i] = temp.FinalData.word[ii].Mila;
            //             pointToWordsInSub[i] = temp.FinalData.word[ii];
            //             if (text1.Count() > 0)
            //             {
            //                 //subStr = string.Concat("D");
            //                 wordsInSub[i] = text1 + temp.FinalData.word[ii].Mila;
            //             }

            //             subStr = string.Concat(subStr, temp.FinalData.word[ii].Kategoria);

            //         }
            //     }
            //     //פה לעשות החלפה מחרוזת FF או F לA
            //     subStr = Regex.Replace(subStr, "F{1,2}", "B");

            //     string d = AutoForParsing.SearchWord(subStr, Globals.root);
            //     string SortWord = "";
            ////     subStr = Regex.Replace(subStr, "BC", "BB");
            //     wordsInSub = MergeBRoles(wordsInSub,ref subStr  , pointToWordsInSub.ToList());
            //     if (d == null)
            //     {
            //         subStr = Regex.Replace(subStr, "BC", "BB");
            //         wordsInSub = MergeBRoles(wordsInSub, ref subStr , pointToWordsInSub.ToList());
            //         subStr = Regex.Replace(subStr, "B{2,}", "B");
            //         d = AutoForParsing.SearchWord(subStr, Globals.root);
            //         if(d== null) {  SortWord = AutoForParsing.SortWord(subStr);
            //         d = AutoForParsing.SearchWord(SortWord, Globals.wrongRoot);
            //         if (d != null)
            //             wordsInSub = functionForNituch.SortByFinalOrder(subStr, wordsInSub, d);
            //         else
            //             Globals.errors.Add(new Error(5, "בעיה תחבירית"));}

            //     }



            //     //תיקון דקדוקי.
            //     //חיפוש הפועל במשפט שליחתו לפונקציה שמביאה לי את המחרוזת שלו בודקת את הדרים שצריכם תיאום
            //     // ושיבוץ במשפטבין הש"ע לפועל  במידה ונדרש שינוי שיניו המחרוזת שליחה לפונקציה שבונה ציה שבונה מילה חדשה 

            //     Console.WriteLine(functionForNituch.OutRoot("הלכתי"));
            //     string[] MergeBRoles(string[] words, ref string roles, List<WordInfo> wordInfos)
            //     {
            //         var newWords = new List<string>();
            //         var newRoles = new List<char>();
            //         var newInfos = new List<WordInfo>();

            //         int i = 0;
            //         while (i < roles.Length)
            //         {
            //             if (roles[i] == 'B')
            //             {
            //                 int start = i;
            //                 List<string> mergedWords = new List<string>();
            //                 List<WordInfo> mergedInfos = new List<WordInfo>();
            //                 string temp = roles;
            //                 while (i < roles.Length &&( roles[i] == 'B' || roles[i]=='C'))
            //                 {
            //                     mergedWords.Add(words[i]);
            //                     mergedInfos.Add(wordInfos[i]);
            //                     i++;

            //                 }

            //                 newWords.Add(string.Join(" ", mergedWords));
            //                 newRoles.Add('B');

            //                 // קח את הראשון כבסיס לאובייקט המאוחד
            //                 WordInfo merged = mergedInfos[0];
            //                 if(temp.Contains("BB"))
            //                 {
            // // תמיד שנה את Mispar ל־"P"
            //                        merged.Mispar = "P";

            //                         // בדוק אם כל המינים זהים
            //                         bool allSameMin = mergedInfos.All(info => info.Min == merged.Min);
            //                         if (!allSameMin)
            //                             merged.Min = "m"; // שים "S" אם יש הבדל
            //                 }


            //                 newInfos.Add(merged);
            //             }
            //             else
            //             {
            //                 newWords.Add(words[i]);
            //                 newRoles.Add(roles[i]);
            //                 newInfos.Add(wordInfos[i]);
            //                 i++;
            //             }
            //         }

            //         roles = new string(newRoles.ToArray());
            //         wordInfos.Clear();
            //         wordInfos.AddRange(newInfos);
            //         pointToWordsInSub = wordInfos.ToArray(); // שומר את המידע המעודכן

            //         return newWords.ToArray();
            //     }

            // string[] MergeBRoles(string[] words, ref string roles, List<WordInfo> wordInfos)
            //{
            //    var newWords = new List<string>();
            //    var newRoles = new List<char>();
            //    var newInfos = new List<WordInfo>();

            //    int i = 0;
            //    while (i < roles.Length)
            //    {
            //        if (roles[i] == 'B')
            //        {
            //            int start = i;
            //            while (i < roles.Length && roles[i] == 'B') i++;

            //            newWords.Add(string.Join(" ", words[start..i]));
            //            newRoles.Add('B');

            //            // רק כאן משנים את Mispar ל-"P" כי איחדת
            //            wordInfos[start].Mispar = "P";
            //            newInfos.Add(wordInfos[start]);
            //        }
            //        else
            //        {
            //            newWords.Add(words[i]);
            //            newRoles.Add(roles[i]);
            //            newInfos.Add(wordInfos[i]);
            //            i++;
            //        }
            //    }

            //    roles = new string(newRoles.ToArray());
            //    wordInfos.Clear();
            //    wordInfos.AddRange(newInfos);
            //    pointToWordsInSub = wordInfos.ToArray(); // שומר את המידע המעודכן

            //    return newWords.ToArray();
            //}


            //static string[] MergeBRoles(string[] words, ref string roles)
            //{
            //    List<string> newWords = new List<string>();
            //    List<char> newRoles = new List<char>();

            //    int i = 0;
            //    while (i < roles.Length)
            //    {
            //        if (roles[i] == 'B')
            //        {
            //            List<string> merged = new List<string>();
            //            לאסוף רצף של B
            //            while (i < roles.Length && roles[i] == 'B')
            //            {
            //                merged.Add(words[i]);
            //                i++;
            //            }
            //            newWords.Add(string.Join(" ", merged));
            //            newRoles.Add('B');
            //        }
            //        else
            //        {
            //            newWords.Add(words[i]);
            //            newRoles.Add(roles[i]);
            //            i++;
            //        }
            //    }

            //    עדכון מחרוזת התפקידים שקיבלת ב - ref
            //   roles = new string(newRoles.ToArray());

            //    להחזיר מערך חדש של המילים אחרי האיחוד
            //        return newWords.ToArray();
            //}
            //wordsInSub = functionForNituch.grammerrError(subStr, pointToWordsInSub, wordsInSub);
            //    Console.WriteLine(functionForNituch.FixWord((VerbWordInfo) pointToWordsInSub[1], pointToWordsInSub[0]));

            //    ;
        }
        public static void  FIRSTBUILD ()
        {
            Globals.root = new vertexP(StatusV.REGULAR);
            Globals.wrongRoot = new vertexP(StatusV.REGULAR);
            vertexP wrongRoot = new vertexP(StatusV.REGULAR);
            AutoForParsing.LoadWordsFromFile(@"C:\Users\User\Desktop\OUTPUT.txt", Globals.root, Globals.wrongRoot);
            Globals.rootLang = new Vertex(StatusV.REGULAR);
            functionForLangAuto.LoadWordsFromFileAutoLang(@"C:\Users\User\Desktop\try.txt", Globals. rootLang);
            Console.WriteLine(Globals.rootLang.Edges[0].Edges[11].Edges[8].FinalData.word[0].Mila);
            Globals. rootLang.Edges[0].Edges[11].Edges[8].FinalData.word[0].Kategoria = "F";
        }
        public static string TextToText(string input)
         {
            Globals.errors = null;
            string sub = ""; 
            sub = input;
            string[] wordsInSub = sub.Split(' ');
            WordInfo[] pointToWordsInSub = new WordInfo[wordsInSub.Length];
            int ii = 0;
            Vertex temp = new Vertex(StatusV.REGULAR);
            string subStr = "";
            string text = "משהוכלב";
            string text1 = "";
            string w = "";
;            for (int i = 0; i < wordsInSub.Length; i++)
            {
                 text = "משהוכלב";
                 text1 = "";
                 w = wordsInSub[i];
                //החזרה במשתנה מסוג רף את המיקום של המילה במידה וקיים לי במצב המקבל כמה מילים.
                temp = functionForLangAuto.SearchWordAutoLang(w,Globals. rootLang, ref ii);
                while (text.Count() > 0 && temp == null && text.Contains(w[0]))
                {
                    text = text.Replace(w[0], '\0');
                    text1 += w[0];
                    w = w.Substring(1);
                    temp = functionForLangAuto.SearchWordAutoLang(w,Globals. rootLang, ref ii);
                    //אם אתה מתחיל באחת מאויות השימוש תוריד אותה מהמחרוזת ומהמילה בעצמה ותשלח שוב את המילה

                }

                if (temp != null)
                {
                    wordsInSub[i] = temp.FinalData.word[ii].Mila;
                    pointToWordsInSub[i] = temp.FinalData.word[ii];
                    if (text1.Count() > 0)
                    {
                        //subStr = string.Concat("D");
                        wordsInSub[i] = text1 + temp.FinalData.word[ii].Mila;
                    }

                    subStr = string.Concat(subStr, temp.FinalData.word[ii].Kategoria);

                }
                else
                {
                    Globals.errors.Add(new Error(0, "הכנסת מילים שלא קיימות בשפה העברית"));
                    return Globals.errors[0].ToString();    
                }
            }
            // למקרה שלפעלים מדבר כמו אכלתי שיכיל לי גם את הש"ע פה לעשות החלפה מחרוזת FF או F לA
            subStr = Regex.Replace(subStr, "F{1,2}", "B");

            string d = AutoForParsing.SearchWord(subStr, Globals.root);
            string SortWord = "";
            //     subStr = Regex.Replace(subStr, "BC", "BB");
            //wordsInSub = MergeBRoles(wordsInSub, ref subStr, pointToWordsInSub.ToList());
            if (d == null)
            {
                subStr = Regex.Replace(subStr, "BC", "BB");
                wordsInSub = MergeBRoles(wordsInSub, ref subStr, pointToWordsInSub.ToList());
                subStr = Regex.Replace(subStr, "B{2,}", "B");
                d = AutoForParsing.SearchWord(subStr, Globals.root);
                if (d == null)
                {
                    SortWord = AutoForParsing.SortWord(subStr);
                    d = AutoForParsing.SearchWord(SortWord, Globals.wrongRoot);
                    if (d != null) { 
                        wordsInSub = functionForNituch.SortByFinalOrder(subStr, wordsInSub, d);
                        ///אם יש בעיות להוריד את השורה הזו הוספתי אותה קק בשביל לבדוק משהו בטעיויות דקדוק
                    subStr = d;}
                    else
                        Globals.errors.Add(new Error(5, "בעיה תחבירית"));
                }

            }

            //תיקון דקדוקי.
            //חיפוש הפועל במשפט שליחתו לפונקציה שמביאה לי את המחרוזת שלו בודקת את הדרים שצריכם תיאום
            // ושיבוץ במשפטבין הש"ע לפועל  במידה ונדרש שינוי שיניו המחרוזת שליחה לפונקציה שבונה ציה שבונה מילה חדשה 

            string[] MergeBRoles(string[] words, ref string roles, List<WordInfo> wordInfos)
            {
                var newWords = new List<string>();
                var newRoles = new List<char>();
                var newInfos = new List<WordInfo>();

                int i = 0;
                while (i < roles.Length && i<wordInfos.Count)
                {
                    if (roles[i] == 'B'&& i< wordInfos.Count-1 && pointToWordsInSub[i+1].Kategoria != "FA")
                    {
                        int start = i;
                        List<string> mergedWords = new List<string>();
                        List<WordInfo> mergedInfos = new List<WordInfo>();
                        string temp = roles;
                        while (i < roles.Length && (roles[i] == 'B' /*|| roles[i] == 'C'*/))
                        {
                            mergedWords.Add(words[i]);
                            mergedInfos.Add(wordInfos[i]);
                            i++;

                        }

                        newWords.Add(string.Join(" ", mergedWords));
                        newRoles.Add('B');

                        // קח את הראשון כבסיס לאובייקט המאוחד
                        WordInfo merged = mergedInfos[0];
                        if (temp.Contains("BB"))
                        {
                            // תמיד שנה את Mispar ל־"P"
                            merged.Mispar = "p";

                            // בדוק אם כל המינים זהים
                            bool allSameMin = mergedInfos.All(info => info.Min == merged.Min);
                            if (!allSameMin)
                                merged.Min = "m"; // שים "S" אם יש הבדל
                        }


                        newInfos.Add(merged);
                    }
                    else
                    {
                        newWords.Add(words[i]);
                        newRoles.Add(roles[i]);
                        newInfos.Add(wordInfos[i]);
                        i++;
                    }
                }

                roles = new string(newRoles.ToArray());
                wordInfos.Clear();
                wordInfos.AddRange(newInfos);
                pointToWordsInSub = wordInfos.ToArray(); // שומר את המידע המעודכן

                return newWords.ToArray();
            }
          wordsInSub = MergeBRoles(wordsInSub, ref subStr, pointToWordsInSub.ToList());

            wordsInSub = functionForNituch.grammerrError(subStr, pointToWordsInSub, wordsInSub);
            // return string.Join("\n", Globals.errors);



            //string errorMessages = Globals.errors.Any() ?
            //string.Join("\n", Globals.errors.Select(e => e.ToString())) :
            //"תקין כל הכבוד";
            string errorMessages = Globals.errors.Any()
    ? string.Join("\n\n", Globals.errors.Select(e => e.ToString()))
    : "תקין כל הכבוד";

            string fixedSentence = string.Join(" ", wordsInSub);

            // מוסיפים שורת רווח בין ההודעות
            return errorMessages + "\n\n" + fixedSentence;

            //string fixedSentence = string.Join(" ", wordsInSub);
            //return errorMessages + "\n\n" + fixedSentence;

        }
    }
    
}

