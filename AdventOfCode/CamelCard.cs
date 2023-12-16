public class CamelCard : IComparable{
    List<int> hand = new List<int>();
    int type;
    public int bid;
    int jokerCount = 0;
    public CamelCard(string cardString){
        string[] splitString = cardString.Split(" ");
        foreach (char card in splitString[0]){
            if(card == '2'){hand.Add(1);}
            else if (card == '3'){hand.Add(2);}
            else if (card == '4'){hand.Add(3);}
            else if (card == '5'){hand.Add(4);}
            else if (card == '6'){hand.Add(5);}
            else if (card == '7'){hand.Add(6);}
            else if (card == '8'){hand.Add(7);}
            else if (card == '9'){hand.Add(8);}
            else if (card == 'T'){hand.Add(9);}
            // else if (card == 'J'){hand.Add(10);} //Use this for part 1
            else if (card == 'J'){hand.Add(0); this.jokerCount++;} //Use this for part 2
            else if (card == 'Q'){hand.Add(11);}
            else if (card == 'K'){hand.Add(12);}
            else if (card == 'A'){hand.Add(13);}
        }
        this.bid = int.Parse(splitString[1]);
        this.type = CalculateType();
    }
    private int CalculateType(){
        int totalMatches = 0;
        foreach(int card in hand){
            foreach(int otherCard in hand){
                    int counter = 0;
                    if(card == otherCard){
                    counter ++;
                }
                totalMatches += counter;
           
        }
         }
         totalMatches = ApplyJoker(totalMatches,jokerCount);
         return totalMatches;
    }

    private int ApplyJoker(int totalMatches, int jokerCount)
    {
        if(jokerCount == 0||jokerCount == 5){return totalMatches;}
        if(jokerCount == 4){return 5*5;}
        if(jokerCount == 3){
            if(totalMatches == 3*3+2*1){return 4*4;}
            if(totalMatches == 3*3+2*2){return 5*5;}
            else throw new NotImplementedException("totalMatches " + totalMatches + " jokerCount " + jokerCount);
        }
        if(jokerCount == 2){
            if(totalMatches == 3*3+2*2){return 5*5;}
            if(totalMatches == 2*2+1+2*2){return 4*4 + 1;}
            if(totalMatches == 2*2+3*1){return 3*3 + 2*1;}
            else throw new NotImplementedException("totalMatches " + totalMatches + " jokerCount " + jokerCount);

        }
        if(jokerCount == 1){
            if(totalMatches == 4*4 + 1){return 5*5;}
            if(totalMatches == 3*3 + 2*1){return 4*4 +1;}
            if(totalMatches == 2*2 + 2*2 + 1){return 3*3 + 2*2;}
            if(totalMatches == 2*2 + 3*1){return 3*3 + 2*1;}
            if(totalMatches == 5){return 2*2 + 3*1;}
            else throw new NotImplementedException("totalMatches " + totalMatches + " jokerCount " + jokerCount);

        }
        else throw new NotImplementedException("totalMatches " + totalMatches + " jokerCount " + jokerCount);
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        else{
        CamelCard? otherHand = obj as CamelCard;
        if(this.type > otherHand!.type) return 1;
        if(this.type < otherHand!.type) return -1;
        if(this.type == otherHand!.type){
            for(int cardOrder = 0; cardOrder < hand.Count; cardOrder++){
                if(this.hand[cardOrder] > otherHand.hand[cardOrder]) return 1;
                if(this.hand[cardOrder] < otherHand.hand[cardOrder]) return -1;
            }
            return 0;
        }
        return 0;
    }}
}