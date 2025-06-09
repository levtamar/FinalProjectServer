using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using HtmlAgilityPack;
using Nituch;
using MyScraperProject;
using vertexP = Nituch.vertexForNituch.vertexP;
using StatusV = Nituch.vertexForNituch.StatusV;
using WordInfo = Nituch.vertexForNituch.WordInfo;
using Nituch.vertexForNituch;




namespace Nituch
{
    public class functionForNituch
    {

        static Dictionary<string, char> posToChar = new Dictionary<string, char>
                {
                    { "פועל ", 'A' },
                    { "שם עצם ", 'B' },
                    { "שם עצם", 'B' },
                    { "כינוי גוף", 'B' },
                    { "שם תואר ", 'C' },
                     { "שם תואר", 'C' },
                    { " שם תואר", 'C' },
                      {"תואר פועל" , 'C'},
                    { "מילת יחס", 'D' },
                    { "מילית קישור", 'E' }
                };
        static Dictionary<string, string> HebrewToEnum = new Dictionary<string, string>
    {
        { "עבר","PERF" },
        { "הווה / בינוני", "AP" },
        { "עתיד", "IMPF" },
            {"ציווי"  , "IMP" }
    };
        static Dictionary<string, string> hebrewToEnglish = new Dictionary<string, string>
        {
            { "פָּעַל", "G" },
            { "נִפְעַל", "N" },
            { "פִּעֵל", "D" },
            { "הִתְפַּעֵל", "DT" },
            { "הופעל", "HP" }, // לא מנוקד, נשאר כפי שהוא
            { "פועל", "DP" } ,
             { "הִפְעִיל", "H" } // לא מנוקד, נשאר כפי שהוא
        };

        //public static string BuildWordId wordInfo)
        //{
        //    // מוודא שאין ערכים חסרים
        //    string zman = wordInfo.Zman ?? "UNKNOWN";
        //    string guf = wordInfo.Guf ?? "X";
        //    string min = wordInfo.Min ?? "x";
        //    string mispar = wordInfo.Mispar ?? "s"; // נניח ש"s" כברירת מחדל

        //    return $"{zman}-{guf}{min}{mispar}";
        //}
        public static string BuildWordId(VerbWordInfo wordInfo)
        {
            // מוודא שאין ערכים חסרים
            string zman = wordInfo.Zman ?? "UNKNOWN";
            string guf = wordInfo.Guf ?? "X";
            string min = wordInfo.Min ?? "x";
            string mispar = wordInfo.Mispar ?? "s"; // נניח ש"s" כברירת מחדל

            return $"{zman}-{guf}{min}{mispar}";
        }
        public static string FixWord(VerbWordInfo wordInfo1 , WordInfo noun1)
        {
          
            if (!(noun1.Mispar == wordInfo1.Mispar && wordInfo1.Min == noun1.Min))
            {
                VerbWordInfo wordInfo = new VerbWordInfo
                {
                    Mila = wordInfo1.Mila,
                    Kategoria = wordInfo1.Kategoria,
                    Mispar = wordInfo1.Mispar,
                    Min = wordInfo1.Min,
                    Root = wordInfo1.Root,
                    Zman = wordInfo1.Zman,
                    Guf = wordInfo1.Guf,
                    Binyan = wordInfo1.Binyan
                };

                WordInfo noun = new WordInfo
                {
                    Mila = noun1.Mila,
                    Kategoria = noun1.Kategoria,
                    Mispar = noun1.Mispar,
                    Min = noun1.Min,
                    Root = noun1.Root
                };
                wordInfo.Min = noun.Min; 
                wordInfo.Mispar = noun.Mispar;  
          
            string strId = $"{wordInfo.Zman}-{wordInfo.Guf}{wordInfo.Min}{wordInfo.Mispar}";

            if ((wordInfo.Zman == "PERF" || wordInfo.Zman == "IMPF") && wordInfo.Guf == "1") strId = $"{wordInfo.Zman}-{wordInfo.Guf}{wordInfo.Mispar}";
            if (strId.Contains("AP")) strId = new string(strId.Where(c => !char.IsDigit(c)).ToArray());
            if (wordInfo.Zman == "PERF" && wordInfo.Guf == "3" && wordInfo.Mispar == "p") 
                    strId = "PERF-3p";
            
            //    string url = "https://www.pealim.com/constructor/?root=%D7%94%D7%9C%D7%9A&binyan=G";
            //     string url = "https://www.pealim.com/constructor/?root=" + Uri.EscapeDataString(wordInfo.Root) + "&binyan=" + wordInfo.Binyan;
            string url = " https://www.pealim.com/constructor/?root=" + wordInfo.Root + "&binyan=" + hebrewToEnglish[wordInfo.Binyan] + "&gg-prefer-chataf=default&gd-prefer-chataf=default&gd-tashlum=default";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamRead = new StreamReader(response.GetResponseStream());
            string html = streamRead.ReadToEnd();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            // var node = doc.GetElementbyId("IMP-2fp");
            var node = doc.GetElementbyId(strId);
            string s = functionForLangAuto.RemoveNiqqud(node.InnerText.Trim());
            Globals.errors.Add(new Error(6, "📚 נמצאה טעות דקדוקית במשפט כתבת " + wordInfo1.Mila + "במקום " + s));
            Console.WriteLine(s);
            if (node != null)
            {
                return functionForLangAuto.RemoveNiqqud(node.InnerText.Trim()); // או InnerHtml אם אתה רוצה את הקוד HTML הפנימי
            }
            else
            {
                return "תגית עם id=2155 לא נמצאה.";
            }
            }
            else
            {
                return wordInfo1.Mila;
            }
        }
        //public static string FixWord(VerbWordInfo wordInfo)
        //{
        //    string strId = $"{wordInfo.Zman}-{wordInfo.Guf}{wordInfo.Min}{wordInfo.Mispar}";

        //    if ((wordInfo.Zman == "PERF" || wordInfo.Zman == "IMPF") && wordInfo.Guf == "1") strId = $"{wordInfo.Zman}-{wordInfo.Guf}{wordInfo.Mispar}";
        //    if (strId.Contains("AP")) strId = new string(strId.Where(c => !char.IsDigit(c)).ToArray());
        //    if (wordInfo.Zman == "PERF" && wordInfo.Guf == "3" && wordInfo.Mispar == "p") strId = "PERF-3p";


        //    //    string url = "https://www.pealim.com/constructor/?root=%D7%94%D7%9C%D7%9A&binyan=G";
        //    //     string url = "https://www.pealim.com/constructor/?root=" + Uri.EscapeDataString(wordInfo.Root) + "&binyan=" + wordInfo.Binyan;
        //    string url = " https://www.pealim.com/constructor/?root=" + wordInfo.Root + "&binyan=" + "G" + "&gg-prefer-chataf=default&gd-prefer-chataf=default&gd-tashlum=default";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    StreamReader streamRead = new StreamReader(response.GetResponseStream());
        //    string html = streamRead.ReadToEnd();
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(html);
        //   // var node = doc.GetElementbyId("IMP-2fp");
        //   var node = doc.GetElementbyId(strId);

        //    string s = node.InnerText.Trim();
        //    Console.WriteLine(s);
        //    if (node != null)
        //    {
        //        return node.InnerText.Trim(); // או InnerHtml אם אתה רוצה את הקוד HTML הפנימי
        //    }
        //    else
        //    {
        //        return "תגית עם id=2155 לא נמצאה.";
        //    }
        //}
        public static WordInfo OutRoot(string word)
        {
            string url = "https://www.pealim.com/he/search/?from-nav=1&q=";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + word);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader streamRead = new StreamReader(response.GetResponseStream());
            string html = streamRead.ReadToEnd();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            string category;
            string time = "";
            string y = "";
            string rootText = "";
            string binyan = "";
            string min = "";
            string guf = "";

            // חלק דיבור
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='verb-search-binyan']");
            HtmlNodeCollection zman = doc.DocumentNode.SelectNodes("//div[@class='vf-search-tpgn']");
            var rawText = "";
            // קטגוריה
            try
            {
                 rawText = nodes[0].InnerText.Trim();
                var tag = rawText.Contains('–') ? rawText[10..rawText.IndexOf('–')] : rawText[10..];
                category = posToChar[tag].ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בעת חילוץ קטגוריה: " + ex.Message);
                category = "ERROR";
            }
            if (category != "C")
            {
                // זמן, מין, גוף
                HtmlNode z1 = zman[0];
                string tagZman1 = z1.InnerText.Trim();

                min = tagZman1.Contains("נקבה") ? "f" :
                      tagZman1.Contains("זכר") ? "m" :
                      tagZman1.EndsWith("ת:") ? "f" : "m";
                 if (tagZman1.Contains("מדבר")) category = "FA";

                if (rawText.Contains("נקבה")) min ="f";
                if (category == "A" || category == "FA") // תנאי לפועל
                {
                    // זמן
                    time = tagZman1[0..tagZman1.IndexOf(',')];
                    // גוף ומין
                    string min11 = tagZman1.Substring(tagZman1.IndexOf(',') + 1);
                    //guf = min1.Contains("נסת" || "שלישי") ? "3" : (min1.Contains("מדבר") ? "1" : "2");
                     guf = (min11.Contains("נסת") || min11.Contains("שלישי")) ? "3" :
             (min11.Contains("מדבר") ? "1" : "2");

                    // בנין
                    HtmlNode binyanDiv = doc.DocumentNode.SelectSingleNode("//div[@class='verb-search-binyan']//b");
                    binyan = binyanDiv != null ? binyanDiv.InnerText.Trim() : "NOT FOUND";

                    // שורש
                    HtmlNode rootNode = doc.DocumentNode.SelectSingleNode("//div[@class='verb-search-root']/a");
                    rootText = rootNode?.InnerText.Trim() ?? "NOT FOUND";

                   
                }
                // יחיד/רבים
                y = (tagZman1.Contains("ים")) ? "p" : "s";
            }

            WordInfo wordInfo;

            if (category == "A" || category == "FA") // יצירת אובייקט פועל
            {
                var verbWordInfo = new VerbWordInfo
                {
                    Mila = word,
                    Mispar = y,
                    Kategoria = category,
                    Min = min,
                    Root = functionForLangAuto.RemoveNiqqud(rootText),
                    Zman = HebrewToEnum.TryGetValue(time, out var code) ? code : "NO FOUND",
                    Guf = guf,
                    //Binyan = functionForLangAuto.RemoveNiqqud(binyan)
                    Binyan = binyan,
                };
                wordInfo = verbWordInfo;
            }
            else // מילה רגילה
            {
                wordInfo = new WordInfo
                {
                    Mila = word,
                    Mispar = y,
                    Kategoria = category,
                    Min = min,
                    Root = functionForLangAuto.RemoveNiqqud(rootText)
                };
            }

     
            Console.WriteLine(wordInfo.ToString());

            return wordInfo;
        }

        //public static WordInfo OutRoot(string word)
        //{
        //    string url = "https://www.pealim.com/he/search/?from-nav=1&q=";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + word);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //    StreamReader streamRead = new StreamReader(response.GetResponseStream());
        //    string html = streamRead.ReadToEnd();
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(html);
        //    string tag1 = "not found";
        //    string tag2 = "not found";
        //    string tagZman1 = "not found";
        //    string tagZman2 = "not found";
        //    HtmlNode node1;
        //    HtmlNode node2;
        //    HtmlNode z1;
        //    HtmlNode z2;
        //    HtmlNode tryyy;
        //    string category;
        //    bool degel;
        //    string time = "";
        //    string y = "";
        //    string v;
        //    string rootText = "";
        //    string binyan = "";
        //    string min = "";
        //    string min1 = "";
        //    string guf = "";
        //    WordInfo wordInfo = new WordInfo();
        //    //חלק דיבור
        //    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='verb-search-binyan']");
        //    HtmlNodeCollection zman = doc.DocumentNode.SelectNodes("//div[@class='vf-search-tpgn']");
        //    HtmlNodeCollection spans = doc.DocumentNode.SelectNodes("//div[@class='vf-search-hebrew']//span");
        //    //קטגוריה
        //    try
        //    {
        //        var rawText = nodes[0].InnerText.Trim();
        //        var tag = rawText.Contains('–') ? rawText[10..rawText.IndexOf('–')] : rawText[10..];
        //        category = posToChar[tag].ToString();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("שגיאה בעת חילוץ קטגוריה: " + ex.Message);
        //        category = "ERROR";
        //    }
        //    if (category != "C")
        //    {
        //        //גוף
        //        z1 = zman[0];
        //        tagZman1 = z1.InnerText.Trim();
        //        min = tagZman1.Contains("נקבה") ? "f" :
        //      tagZman1.Contains("זכר") ? "m" :
        //      tagZman1.EndsWith("ת:") ? "f" : "m";
        //        if (category == "A")
        //        {


        //            //זמן
        //            time = tagZman1[0..tagZman1.IndexOf(',')];
        //            //מין וגוף
        //            min1 = tagZman1.Substring(tagZman1.IndexOf(',') + 1);
        //            guf = min1.Contains("נסת") ? "3" : (min1.Contains("מדבר") ? "1" : "2");
        //            string[] s = tag1.Split(' ');
        //            //בנין
        //            HtmlNode binyanDiv = doc.DocumentNode.SelectSingleNode("//div[@class='verb-search-binyan']//b");
        //            binyan = binyanDiv != null ? binyanDiv.InnerText.Trim() : "NOT FOUND";
        //            //שורש
        //            HtmlNode rootNode = doc.DocumentNode.SelectSingleNode("//div[@class='verb-search-root']/a");
        //            rootText = rootNode?.InnerText.Trim() ?? "NOT FOUND";


        //        }
        //        if (tagZman1.Contains("מדבר")) category = "FA";
        //        try
        //        {
        //            z1 = zman[0];
        //            tagZman1 = z1.InnerText.Trim();
        //            y = (tagZman1.Contains("ים")) ? "s" : "p";
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("שגיאה בעת חילוץ יחיד רבים: " + ex.Message);
        //            category = "ERROR";
        //        }
        //        //tryyy = spans[0];
        //        //v = tryyy.InnerText.Trim();
        //        //if (v.Contains('+'))
        //        //{
        //        //    category = "E" + posToChar[tag1.Contains('–') ? tag1[10..tag1.IndexOf('–')] : tag1[10..]];
        //        //}
        //        //יחיד רבים
        //    }
        //    wordInfo.VerbDetails = new vertexForNituch.VerbInfo();
        //    wordInfo.Mispar = y;
        //    wordInfo.Mila = word;
        //    wordInfo.VerbDetails.Zman = HebrewToEnum.TryGetValue(time, out var code) ? code : "NO FOUND";
        //    wordInfo.Kategoria = category;
        //    wordInfo.Min = min;
        //    wordInfo.VerbDetails.Guf = guf;
        //    wordInfo.Root = functionForLangAuto.RemoveNiqqud(rootText);
        //    wordInfo.VerbDetails.Binyan = functionForLangAuto.RemoveNiqqud(binyan);
        //    //  wordInfo.Guf = (min.Contains("נסת"));
        //    wordInfo.Min = min;
        //    Console.WriteLine(BuildWordId(wordInfo));
        //    Console.WriteLine(wordInfo.ToString());
        //    return wordInfo;
        //}



        //    wordInfo.Mispar = y;
        //    wordInfo.Mila = word;
        //    wordInfo.Zman = HebrewToEnum.TryGetValue(time, out var code) ? code : "NO FOUND";
        //    wordInfo.Kategoria = category;
        //    wordInfo.Min = min;
        //    wordInfo.Guf = guf;
        //wordInfo.root = functionForLangAuto.RemoveNiqqud(rootText);
        //wordInfo.binyan = functionForLangAuto.RemoveNiqqud(binyan);   


        //public static string OutRoot(string word)
        //{
        //    // כתובת דף האינטרנט
        //    string url = "https://www.pealim.com/he/search/?from-nav=1&q=";

        //    // שליחת בקשה עם מילת החיפוש
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + word);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    // קבלת התגובה (HTML)
        //    StreamReader streamRead = new StreamReader(response.GetResponseStream());

        //    // קריאת התגובה למחרוזת
        //    string html = streamRead.ReadToEnd();

        //    // טעינת העמוד שהתקבל ל-HTML
        //    HtmlDocument doc = new HtmlDocument();
        //    doc.LoadHtml(html);

        //    string tag = "not found";
        //    HtmlNode node;
        //    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='verb-search-root']");
        //    Console.WriteLine(nodes);

        //    // אם השורש לא נמצא, חיפוש חלופי
        //    if (nodes == null)
        //    {
        //        nodes = doc.DocumentNode.SelectNodes("//*[@class='menukad']");
        //    }

        //    // אם מצאנו תוצאה
        //    if (nodes != null)
        //    {
        //        node = nodes[0];
        //        tag = node.InnerHtml;

        //        // אם התוצאה היא "נעתי", מחזירים את מילת החיפוש המקורית
        //        if (tag == "נעתי")
        //        {
        //            tag = word;
        //        }
        //    }
        //    else
        //    {
        //        // אם לא נמצא כלום, מחזירים את מילת החיפוש המקורית
        //        tag = word;
        //    }

        //    return tag;
        //}
        //public static string countSort(string str)
        //{
        //    int[] arr = new int[4];
        //    List<char>[] arrStr = new List<char>[4];
        //    for (int i = 0; i < str.Length; i++)
        //    {
        //        arr[str[i] - 'A'] += 1;
        //        arrStr[str[i]] = & str [i];
        //    }

        //}

        //public static string SortByKeys(string keys, string[] parts)
        //{
        //    // שלב 1: ניצור מערך של 26 רשימות (לכל אות מ-A עד Z)
        //    List<string>[] buckets = new List<string>[4];
        //    for (int i = 0; i < buckets.Length; i++)
        //        buckets[i] = new List<string>();

        //    // שלב 2: נמלא כל רשימה לפי האות המתאימה
        //    for (int i = 0; i < keys.Length; i++)
        //    {
        //        int index = keys[i] - 'A'; // ממיר את האות לאינדקס (A=0, B=1, ...)
        //        buckets[index].Add(parts[i]); // שומר את המילה במקום המתאים
        //    }

        //    // שלב 3: נאסוף את כל הרשימות לפי הסדר ונחזיר מחרוזת אחת
        //    List<string> result = new List<string>();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        result.AddRange(buckets[i]);
        //    }

        //    return string.Join(" ", result); // מחזיר מחרוזת אחת עם כל המילים
        //}

        public static string[] SortByFinalOrder(string keys, string[] parts, string finalOrder)
        {
            //הגדרת מילון של תורים מסוג מחרוזת
            Dictionary<char, Queue<string>> buckets = new Dictionary<char, Queue<string>>();
            char key;
            //הוספת שגיאה לרשימת השגיאות
            Globals.errors.Add(new Error(6, "בעיה בסדר המילים במשפט"));
            //מעבר על כל המילים 
            for (int i = 0; i < keys.Length; i++)
            {
               key = keys[i];
                //אם במילון עדיין אין תור מהסוג הזה -  אתחול התור
                if (!buckets.ContainsKey(key))
                    buckets[key] = new Queue<string>();
                //הכנסה לתור את המילה
                buckets[key].Enqueue(parts[i]);
            }
            List<string> result = new List<string>();
            //מעבר על כל התווים במחרוזת הסדר הסופי
            foreach (char c in finalOrder)
            {//אם קיימת תור לא ריק מהסוג של התו הוספתו למחרוזת התוצאה ומחיקתו מהתור
                if (buckets.ContainsKey(c) && buckets[c].Count > 0)
                    result.Add(buckets[c].Dequeue());

            }
            //הכנסה ל PARTS את המבנה הנכון
            for (int i = 0; i < result.Count && i < parts.Length; i++)
            {
                parts[i] = result[i];
            }
            return parts;
        }
        
        //public static void AnalyzeWord(string word)
        //{
        //    string url = $"https://www.pealim.com/he/search/?q={word}";
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //    {
        //        string html = reader.ReadToEnd();
        //        HtmlDocument doc = new HtmlDocument();
        //        doc.LoadHtml(html);

        //        // סוג המילה (לדוגמה: verb / noun)
        //        //     var typeNode = doc.DocumentNode.SelectSingleNode("//div[@class='verb-search-label']");
        //           HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//div[@class='verb-search-binyan']");
        //        string wordType = nodes?.InnerText.Trim() ?? "לא נמצא סוג";

        //        Console.WriteLine($"סוג המילה: {wordType}");

        //        // שליפה של הטיות מהטבלאות
        //        var tables = doc.DocumentNode.SelectNodes("//table[contains(@class, 'conjugations-table')]");
        //        if (tables != null)
        //        {
        //            foreach (var table in tables)
        //            {
        //                // כותרת הטבלה – הזמן (עבר, הווה, עתיד)
        //                var tenseHeader = table.SelectSingleNode(".//thead//th");
        //                string tense = tenseHeader?.InnerText.Trim() ?? "לא ידוע";

        //                Console.WriteLine($"\nזמן: {tense}");

        //                // שליפה של הטיות
        //                var rows = table.SelectNodes(".//tbody/tr");
        //                if (rows != null)
        //                {
        //                    foreach (var row in rows)
        //                    {
        //                        var cells = row.SelectNodes(".//td");
        //                        if (cells != null && cells.Count >= 3)
        //                        {
        //                            string pronoun = cells[0].InnerText.Trim();  // גוף
        //                            string form = cells[1].InnerText.Trim();     // הטיה
        //                            string translit = cells[2].InnerText.Trim(); // תרגום

        //                            // ניחוש של מין ומספר לפי גוף
        //                            string gender = (pronoun.Contains("היא") || pronoun.Contains("את")) ? "נקבה" :
        //                                            (pronoun.Contains("הוא") || pronoun.Contains("אתה")) ? "זכר" : "מעורב";
        //                            string number = (pronoun.Contains("הם") || pronoun.Contains("הן") || pronoun.Contains("אנחנו")) ? "רבים" : "יחיד";

        //                            Console.WriteLine($"גוף: {pronoun}, צורת פועל: {form}, מין: {gender}, מספר: {number}");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("לא נמצאו טבלאות פועל – אולי זו לא מילה פועלית?");
        //        }
        //    }
        //}
        public static string[] grammerrError (string str  , WordInfo [] pointers  , string [] words)
        {
           
            for (int i = 0; i < str.Length-1; i++)
            {
                //if ( str[i] == 'B' && str [i+1]=='A')
                //{
                //    words[i + 1] = FixWord((VerbWordInfo)pointers[i+1], pointers[i ]);

                //}
                ////else
                ////if ( str[i] =='A' && str[i+1]=='B')
                ////    words[i + 1] = FixWord((VerbWordInfo)pointers[i], pointers[i+1]);
                if (str[i] == 'B' && str[i + 1] == 'A')
                {

                    if (pointers[i + 1] is VerbWordInfo verb)
                    {
                        var res = FixWord(verb, pointers[i]);
                        if (res != null)
                            words[i + 1] = res;
                        else if (pointers[i] is VerbWordInfo reverseVerb)
                            words[i] = FixWord(reverseVerb, null);
                    }
                    //else if (pointers[i] is VerbWordInfo reverseVerb)
                    //{
                    //    words[i] = FixWord(reverseVerb, null);
                    //}
                }



            }
            return words;
        }
    }

}
