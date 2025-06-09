using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyScraperProject;
namespace Nituch.vertexForNituch
{

  
        public enum StatusV { REGULAR, ACCEPT }
    public enum Category { VERB, NOUN, QUESTIONWORD, TIMEWORD, NAME };




    public class WordInfo
    {
        public string Mila { get; set; }
        public string Kategoria { get; set; }
        public string Mispar { get; set; }
        public string Min { get; set; }
        public string Root { get; set; }

        public override string ToString()
        {
            return $"מילה: {Mila}, קטגוריה: {Kategoria}, מספר: {Mispar}, מין: {Min}, שורש: {Root}";
        }
    }

    public class VerbWordInfo : WordInfo
    {
        public string Zman { get; set; }
        public string Guf { get; set; }
        public string Binyan { get; set; }

        public override string ToString()
        {
            return base.ToString() + $", זמן: {Zman}, גוף: {Guf}, בניין: {Binyan}";
        }
    }




    //הגדרת קודקוד אוטמט תחביר
    public class vertexP
        {
            public vertexP[] Edges = new vertexP[5]; // מערך של 4 הפניות ל־vertex
            public int code;                 // קוד קודקוד
            public StatusV status;
            public string str;
            public vertexP(StatusV status)
            {
           //     this.status = status;
            this.code = Globals.countParsing++;
             }
            public vertexP(/*StatusV status*/  string str)
            {
                this.status = StatusV.ACCEPT;
                this.Edges[4] = Globals.root;
                this.str = str;
            this.code = Globals.countParsing++;

        }
        public vertexP(/*StatusV status*/  string str , bool d)
        {
            this.status = StatusV.ACCEPT;
            this.Edges[4] = Globals.wrongRoot;
            this.str = str;
            this.code = Globals.countParsingWrong++;
        }
    }
    //הגדרת אוטומט  שפה
    public class Vertex
    {
        public int CodeV;
        public StatusV Status;
        public Vertex[] Edges = new Vertex[16];
        public VertexFinals FinalData;
        public int degel;
        // Assuming 27 possible vertices

        public Vertex(StatusV status)
        {
            CodeV = Globals.counter;
            Status = status;
            Globals.counter = Globals.counter + 1;
            FinalData = null;
        }
    }
    public class VertexFinals
    {
        public List<WordInfo> word = new List<WordInfo>();
        public Category category;
        public int indexinFinalArr = 0;
        public int mone = 0;
        public VertexFinals(string str)
        {
            //if(mone == 0)
            //{
            //    word = new List< string>(); 

            //}
            //word.Add(str);
            //mone++;


        }
    }
}
