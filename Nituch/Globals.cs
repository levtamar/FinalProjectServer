using Nituch;
using static Nituch.AutoForParsing;
using vertexP = Nituch.vertexForNituch.vertexP;
using Vertex = Nituch.vertexForNituch.Vertex;

namespace MyScraperProject
{
    public class Error
    {
        // תכונה: קוד שגיאה
        public int ErrorCode { get; set; }

        // תכונה: תיאור השגיאה
        public string Description { get; set; }

        // בונה
        public Error(int errorCode, string description)
        {
            ErrorCode = errorCode;
            Description = description;
        }
        public Error()
        {

        }
        // פעולה להצגת השגיאה כמחרוזת
        public override string ToString()
        {
            return $"Error {ErrorCode}: {Description}";
        }
    }


    public static class Globals
    {
        public static vertexP root;
        public static vertexP wrongRoot;
        public static Vertex rootLang;
        public static int counter = 0; // משתנה גלובלי
        public static int counterOfFinals = 0; // משתנה גלובלי
        internal static Vertex[] finals = new Vertex[1000];
        public static bool degel = true;
        public static int ind = 0;
        public static List <Error> errors = new List <Error>();
        public static int countParsing = 0;/// מונה לקודקודים באוטומט תחביר 1
        public static int countParsingWrong = 0;//מונה לקודקודים  אוטומט תחביר שגוי
    }

   

}
