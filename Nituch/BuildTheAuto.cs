using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vertex = Nituch.vertexForNituch.Vertex;
using StatusV = Nituch.vertexForNituch.StatusV;
using VertexFinals = Nituch.vertexForNituch.VertexFinals;
using Nituch;
using MyScraperProject;
namespace Nituch
{
    public class BuildTheAuto
    {
        Vertex v = new Vertex(StatusV.REGULAR);
        public static Dictionary<char, Func<char, Vertex, string, Vertex>> tav = new Dictionary<char, Func<char, Vertex, string, Vertex>>()
        {
            { 'א', AddEdge },
            { 'ב', AddEdge },
            { 'ג', AddEdge },
            { 'ד', AddEdge },
            { 'ה', AddEdge },
            { 'ו', AddEdge },
            { 'ז', AddEdge },
            { 'ח', AddEdge },
            { 'ט', AddEdge },
            { 'י', AddEdge },
            { 'כ', AddEdge },
            { 'ך', AddEdge }, // כ"ף סופית
            { 'ל', AddEdge },
            { 'מ', AddEdge },
            { 'ם', AddEdge }, // מ"ם סופית
            { 'נ', AddEdge },
            { 'ן', AddEdge }, // נו"ן סופית
            { 'ס', AddEdge },
            { 'ע', AddEdge },
            { 'פ', AddEdge },
            { 'ף', AddEdge }, // פ"א סופית
            { 'צ', AddEdge },
            { 'ץ', AddEdge }, // צד"י סופית
            { 'ק', AddEdge },
            { 'ר', AddEdge },
            { 'ש', AddEdge },
            { 'ת', AddEdge },
            //             סימני ניקוד – תנועות
            { 'ַ', (c, v , s) =>AddEdge( '\0' , v,s)}, // פתח
            { 'ָ', (c, v,s) =>AddEdge( '\0' , v,s)}, // קמץ
            { 'ֶ', (c, v, s) => AddEdge( '\0' , v,s)}, // סגול
            { 'ֵ', (c, v, s) => AddEdge( '\0' , v,s) }, // צירה
            { 'ִ',(c, v, s) => AddEdge1( 'י' , v,s) }, // חיריק
            { 'ֹ', (c, v, s) => AddEdge( '\0' , v,s)  }, // חולם
            { 'ֻ',(c, v, s) => AddEdge('\0' , v,s)  }, // קובוץ
            //{  'ֻ',(c, v, s) => AddEdge( 'ו' , v,s) }, //שורוק
            { 'ְ', (c, v, s) =>AddEdge( '\0' , v,s)}, // שווא
            { 'ֱ', (c, v, s) =>AddEdge( '\0' , v,s)}, // חטף סגול
            { 'ֲ', (c, v, s) =>AddEdge( '\0' , v,s)}, // חטף פתח
            { 'ֳ', (c, v, s) =>AddEdge( '\0' , v,s)}, // חטף קמץ
            { 'ּ', (c, v, s) =>AddEdge( '\0' , v,s)}, // דגש
            { 'ֿ', (c, v, s) =>AddEdge( '\0' , v,s)}, // רפה
            { '־', (c, v, s) =>AddEdge( '\0' , v,s)}, // מקף עברי
            { 'ׇ', (c, v, s) =>AddEdge( '\0' , v,s)}, // קמץ קטן
            { 'ׁ', (c, v, s) =>AddEdge( '\0' , v,s)}, // נקודה מעל (שימושי באות שין)
            { 'ׂ', (c, v, s) =>AddEdge( '\0' , v,s)}
        };

        public static void build1(string str, Vertex root)
        {
            Vertex v = root;
            //התו האחרון במילה ששם יווצר מצב מקבל 
            char lastHebrewChar = str.LastOrDefault(c => c >= 'א' && c <= 'ת');
            Globals.ind = 0;
            //מעבר על כל התוים במילה ושליחה לפונקציות מתאימות ע"פ המילון.
            while (Globals.ind < str.Length)
            {//שליפה והפעלת פונקציה מתאימה ממילון הפונקציות
                v = tav[str[Globals.ind]](str[Globals.ind], v, str);
            }
            //יצירת מצב סופי 
            int index = functionForLangAuto.HashFun(lastHebrewChar);
            if (Globals.degel)
                createF1(v, str, index);
        }

        public static void createF1(Vertex v, string str, int index)
        {
            v.Status = StatusV.ACCEPT;
            v.FinalData ??= new VertexFinals(str)
            {
                indexinFinalArr = Globals.counterOfFinals
            };
            //הוספה לרשימת המלים במצב המקבל את התוכן של המילה (המילה נשלחחת ללא ניקוד
            v.FinalData.word.Add(functionForNituch.OutRoot(functionForLangAuto.RemoveNiqqud(str)));
            int code = v.CodeV;
            if (code < Globals.finals.Length)
            {
                Globals.finals[Globals.counterOfFinals] = v.Edges[index];
                Globals.counterOfFinals++; // ספירת כמות הקודקודים הסופיים
            }
            Globals.degel = false;
        }
        public static Vertex AddEdge(char c, Vertex v, string s)
        {
            Globals.ind++;
            if (c != '\0')
            {
                int index = functionForLangAuto.HashFun(c);
                //הוספת קשת במידה ואין כזו קשת לצומת זו
                if (v.Edges[index] == null)
                {  
                    v.Edges[index] = new Vertex(StatusV.REGULAR);
                }
                // יצרת מצב סופי במידה והמחרוזת נגמרת פה
                if (Globals.ind >= s.Length)
                {
                    createF1(v.Edges[index], functionForLangAuto.TrimFinalNiqqud(s), index);
                }
                //החזרת הקודוקוד הבא שממנו ימשיכו עם התו הבא במילה
                return v.Edges[index];
            }
            return v;
        }
      //static  HashSet<string> milim = new HashSet<string> { "אִם", "עִם", "מִן", "מִמֶּנִּי", "מִמְּךָ", "מִמֵּךְ", "מִמֶּנּוּ", "מִמֶּנָּה", "מִכֶּם", "מִכֵּן", "מִכָּאן" };

        public static Vertex AddEdge1(char c, Vertex v2, string s)
        {
            Vertex temp;
            Vertex temp2;
            //במידה ומדובר על חיריק מלא
            if (Globals.ind < s.Length - 1 && s[Globals.ind + 1] == 'י')
            {//העלאת האינדקס ב1 שלא יוסיף לי 2 יודים רצופים
                Globals.ind += 1;
                // char y = s.Skip(Globals.ind + 1).FirstOrDefault(c => c >= 'א' && c <= 'ת');

            }
            //הדלקת דגל בקודקוד במישה ושגיאה להוסיף שם יוד
           // if (milim.Contains(s)) v2.degel = 1;
            //הוספת קשת עם יוד
            temp = tav['י']('י', v2, s);
            Globals.ind++;
            //מציאת האות הבאה 
            char x = s.Skip(Globals.ind - 1).FirstOrDefault(c => c >= 'א' && c <= 'ת');
            //במידה וזה סוף המילה כצו המילה אני  לדוגמה צא.
            if (x == '\0')
            {
                return temp;
            }
            //בנית מסלול עוקף
            temp2 = tav[x](x, temp, s);
            //מיזוג המסלולים
            v2.Edges[functionForLangAuto.HashFun(x)] = temp2;
            ///במידה ומצב סופי...
            if (Globals.ind >= s.Length)
            {
                createF1(temp2, s, functionForLangAuto.HashFun(x));
            }
            return temp2;
        }
    }
}
//פונקציה שבהתחלה חשבתי לבנות ככה את האוטומט וראיתי ששמ נאיבי ולא יעיל לעשות כ"כ הרבה if...
//static void AddWord(string str, Vertex root)
//{
//    Vertex v = root;
//    bool degel = false;
//    string result = "";
//    Vertex temp1 = v;
//    int b = -1;
//    Vertex x = temp1;
//    string sstr = FIXMEINCSHARP.Nikud.CheckHebrewNikud(str, out result);

//    for (int i = 0; i < sstr.Length; i++)
//    {

//        int index = HashFun(sstr[i]); // Use hash function

//        if (v.Edges[index] == null)
//        {

//            v.Edges[index] = (i == sstr.Length - 1) ? new Vertex(StatusV.ACCEPT) : new Vertex(StatusV.REGULAR); // Create vertex
//            if (v.Edges[index].Status == StatusV.ACCEPT)
//            {
//                //v.Edges[index].FinalData = new VertexFinals()
//                //{
//                //    word = str,
//                //    category = Category.NAME,
//                //    indexinFinalArr = GlobalVariables.counterOfFinals
//                //   
//                //};
//                v.Edges[index].FinalData = new VertexFinals(sstr)
//                {
//                    indexinFinalArr = GlobalVariables.counterOfFinals
//                };
//                int code = v.Edges[index].CodeV;
//                if (code < GlobalVariables.finals.Length)
//                {
//                    GlobalVariables.finals[GlobalVariables.counterOfFinals] = v.Edges[index];
//                    GlobalVariables.counterOfFinals++; // ספירת כמות הקודקודים הסופיים
//                }
//                //   degel = true;
//            }
//            if (i + 1 < sstr.Length && sstr[i] == 'י' && result[i] == '0')
//            {
//                temp1 = v;
//                b = i + 2;
//            }

//        }
//        if (b == i)
//        {
//            temp1.Edges[HashFun(sstr[i])] = v;
//            b = 0;
//        }
//        v = v.Edges[index];            // Navigate to the next vertex
//        if (i == str.Length - 1 && v.Status == StatusV.ACCEPT && !degel)
//        {
//            //Console.WriteLine(v.FinalData.indexinFinalArr);
//            //Console.WriteLine(v.FinalData.word);
//            int temp = v.FinalData.indexinFinalArr;
//            GlobalVariables.finals[temp].FinalData.word.Add(sstr);
//            //GlobalVariables.finals[GlobalVariables.counterOfFinals].FinalData.word.Add(v.Edges[index].FinalData.word.ToString()) ;
//            //   GlobalVariables.counterOfFinals++; // ספירת כמות הקודקודים הסופיים
//        }
//    }

//}