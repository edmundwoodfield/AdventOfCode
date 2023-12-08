using System.Text.RegularExpressions;

public class Scratchcard{
    public Scratchcard(string scratchcardInput){
        string[] cardNumberSplit = scratchcardInput.Split(":");
        this.cardNumber = int.Parse(Regex.Match(cardNumberSplit[0],"\\d+").Value);
        string[] numberSplit = cardNumberSplit[1].Split("|");
        MatchCollection winning = Regex.Matches(numberSplit[0],"\\d+");
        foreach(Match match in winning){
            this.winningNumbers.Add(int.Parse(match.Value));
        }
        MatchCollection own = Regex.Matches(numberSplit[1],"\\d+");
        foreach(Match match in own){
            this.ownNumbers.Add(int.Parse(match.Value));
        }
        this.numberOfMatches = GenerateMatches(this.winningNumbers,this.ownNumbers);
    }
    public int cardNumber;
    public List<int> winningNumbers = new List<int>();
    public List<int> ownNumbers = new List<int>();
    public int numberOfMatches;
    
    private int GenerateMatches(List<int> winning, List<int> own){
        int matchCount = 0;
        foreach(int win in winning){
            if(own.Contains(win)){
                matchCount++;
            }
        }
        return matchCount;
    }
}