public class CamelCard : IComparable{
    List<int> hand = new List<int>();
    int type;
    public int bid;
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
            else if (card == 'J'){hand.Add(10);}
            else if (card == 'Q'){hand.Add(11);}
            else if (card == 'K'){hand.Add(12);}
            else if (card == 'A'){hand.Add(13);}
        }
        this.bid = int.Parse(splitString[1]);
        this.type = CalculateType();
    }
    private int CalculateType(){
        int handType;
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
         return totalMatches;
            // switch(totalMatches){
            //     case 25:
            //     return 7;
            //     case 17:
            //     return 6;
            //     case 13:
            //     return 5;
            //     case 11:
            //     return 4;
            //     case 9:
            //     return 3;
            //     case 7:
            //     return 2;
            //     case 5:
            //     return 1;
            //     default:
            //     return -1;
            // }
    }

    public int CompareTo(object? obj)
    {
        if (obj == null) return 1;
        CamelCard? otherHand = obj as CamelCard;
        if(this.type > otherHand.type) return 1;
        if(this.type < otherHand.type) return -1;
        if(this.type == otherHand.type){
            for(int cardOrder = 0; cardOrder < hand.Count; cardOrder++){
                if(this.hand[cardOrder] > otherHand.hand[cardOrder]) return 1;
                if(this.hand[cardOrder] < otherHand.hand[cardOrder]) return -1;
            }
            return 0;
        }
        return 0;
    }
}