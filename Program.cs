using Newtonsoft.Json;
using System.Text;

var responseText = File.ReadAllText("Response.json");
List<Models.OcrLine> ocrResponse = JsonConvert.DeserializeObject<List<Models.OcrLine>>(responseText);

Models.OcrLine CalculateAvgAxises(Models.OcrLine i)
{
    i.CalculatedX = i.boundingPoly.vertices.Sum(i => i.x) / 4;
    i.CalculatedY = i.boundingPoly.vertices.Sum(i => i.y) / 4;
    return i;
}

var orderedResponse = ocrResponse.Skip(1).Select(i => CalculateAvgAxises(i)).OrderBy(i => i.CalculatedY).ThenBy(i => i.CalculatedX).ToList();

var currentLine = 1;
var PreviousY = -15;
StringBuilder sr = new StringBuilder();

sr.Append("line\ttext");
foreach (var item in orderedResponse)
{
    if ((item.CalculatedY - PreviousY) > 15)
    {
        sr.AppendLine();

        sr.Append(currentLine + "\t");
        currentLine++;
    }
    sr.Append(item.description + " ");

    PreviousY = item.CalculatedY;
    item.calculatedLine = currentLine;
}

Console.WriteLine(sr);


Console.ReadLine();