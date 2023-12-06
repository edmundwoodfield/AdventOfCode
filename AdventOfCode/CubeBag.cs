public class CubeBag{
    public int gameId;
    public double maxObservedRed;
    public double maxObservedGreen;
    public double maxObservedBlue;
    public double powerOfColours;
    string[] gameArray;

public CubeBag(string gameLine){
    string[] splitGameLine = gameLine.Split(":");
    this.gameId = int.Parse(splitGameLine[0].Substring(5));
    this.gameArray = splitGameLine[1].Split(";");
    PopulateMaxObservedValues();
    this.powerOfColours = this.maxObservedRed * this.maxObservedGreen * this.maxObservedBlue;
}
public void PopulateMaxObservedValues(){
    foreach(string hand in gameArray){
        string[] colourPairs = hand.Split(",");
        foreach(string colourPair in colourPairs){
            double? red = GetNumberForColour(colourPair,"red");
            double? green = GetNumberForColour(colourPair,"green");
            double? blue = GetNumberForColour(colourPair,"blue");
            if (red > this.maxObservedRed){
                this.maxObservedRed = (double)red;
            }
            if (green > this.maxObservedGreen){
                this.maxObservedGreen = (double)green;
            }
            if (blue > this.maxObservedBlue){
                this.maxObservedBlue = (double)blue;
            }
        }
    }
}
public  double? GetNumberForColour(string colourPair, string colourName){
        if(colourPair.Contains(colourName)){
        string strippedNumber = "";
        foreach(char character in colourPair){
            if(char.IsDigit(character)){
                strippedNumber = string.Concat(strippedNumber,character);
            }
        }
        return double.Parse(strippedNumber);}
        else return null;
    }
}