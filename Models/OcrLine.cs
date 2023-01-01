namespace Models;

public class OcrLine
{
    public string locale { get; set; }
    public string description { get; set; }
    public OcrBoundingPoly boundingPoly { get; set; }
    public int CalculatedX = 0;
    public int CalculatedY = 0;
    public int calculatedLine = 0;
}
