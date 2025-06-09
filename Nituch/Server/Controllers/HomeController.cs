using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Nituch; // או namespace מלא שבו נמצאת המחלקה



[ApiController]
[Route("[controller]")]
public class TextController : ControllerBase
{
    [HttpPost("/api/GetText")]
    public string ProcessText(string text)
    {
        string answer = "juhgf";
     answer= MyScraperProject.Program.TextToText(text);

        string newanswer = JsonConvert.SerializeObject(answer);
        return newanswer;
    }
   
}


