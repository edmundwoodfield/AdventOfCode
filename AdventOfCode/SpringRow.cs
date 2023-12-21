using System.Text;
using System.Text.RegularExpressions;

public class SpringRow{
    public SpringRow(string line){
        springs = line.Split(" ")[0];
        MatchCollection broken = Regex.Matches(line.Split(" ")[1],"\\d+");
        foreach(Match match in broken){
            brokenSprings.Add(int.Parse(match.Value));
        }
        while(springs.Length >0  && (springs[0] != '?' || springs[springs.Length-1] != '?')){
        springs = RemovePadding(springs, ref brokenSprings);}
        
        while(springs.Length >0  && (springs[0] == '#' || springs[springs.Length-1] == '#'))
        {
        springs = RemoveLeadingAndTrailingGroups(springs, ref brokenSprings);
        }

        //remove leading spaces that are too small
        //removing this reduced total by 1
        // if (brokenSprings.Count > 0 && brokenSprings[0] > 1){
        //     for(int i = 1; i < brokenSprings[0]; i++){
        //         if(springs[i] == '.'){
        //             springs = springs.Substring(i+1);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //             break;
        //         }
        //     }
        // }

        //remove trailing spaces that are too small
        // removing this increased the total
        // if(brokenSprings.Count > 0 && brokenSprings[brokenSprings.Count-1] > 1){
        //     for (int i = springs.Length-2; i > springs.Length-1-brokenSprings[brokenSprings.Count-1]; i--){
        //         if (springs[i] == '.'){
        //             springs = springs.Substring(0,i);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //             break;
        //         }
        //     }
        // }

        //remove leading groups which must all be broken
        //removing this reduced the total
        // if(brokenSprings.Count > 0 && brokenSprings[0] > 1){
        // if(brokenSprings.Count > 0 && brokenSprings[0] > 1 && springs.Length > brokenSprings[0]){
        //     if(springs[brokenSprings[0]] == '.'){
        //     for(int i = 1; i < brokenSprings[0]; i++){
        //         if(springs[i] == '#'){
        //             springs = springs.Substring(brokenSprings[0]+1);
        //             brokenSprings.RemoveAt(0);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //             break;
        //         }
        //     }}
        //     else if(springs[brokenSprings[0]] == '#' && springs[brokenSprings[0]+1] == '.'){
        //             springs = springs.Substring(brokenSprings[0]+2);
        //             brokenSprings.RemoveAt(0);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //         }
        // }
        //remove trailing groups which must all be broken
        //removing this decreased the total
        // if(brokenSprings.Count > 0 && brokenSprings[brokenSprings.Count-1] > 1){
        // if(brokenSprings.Count > 0 && brokenSprings[brokenSprings.Count-1] > 1 && springs.Length > brokenSprings[brokenSprings.Count-1]){
        //     if(springs[springs.Length-1-brokenSprings[brokenSprings.Count-1]] == '.'){
        //     for(int i = springs.Length-2; i > springs.Length-1-brokenSprings[brokenSprings.Count-1]; i--){
        //         if(springs[i] == '#'){
        //             springs = springs.Substring(0,springs.Length-1-brokenSprings[brokenSprings.Count-1]);
        //             brokenSprings.RemoveAt(brokenSprings.Count-1);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //             break;
        //         }
        //     }}
        //     else if(springs[springs.Length-1-brokenSprings[brokenSprings.Count-1]] == '#' && springs[springs.Length-1-brokenSprings[brokenSprings.Count-1]-1] == '.'){
        //             springs = springs.Substring(0,springs.Length-1-brokenSprings[brokenSprings.Count-1]-1);
        //             brokenSprings.RemoveAt(brokenSprings.Count-1);
        //             springs = RemovePadding(springs, ref brokenSprings);
        //         }
        // }
        
        // removing this reduced the number of solutions
        // if(brokenSprings.Count == 1){
        //     List<int>brokenIndices = new List<int>();
        //     for(int i = 1; i < springs.Length; i++){
        //         if(springs[i] == '#'){brokenIndices.Add(i);}
        //     }
        //     if(brokenIndices.Count > 0 && springs.Length > brokenIndices[0]+brokenSprings[0]){
        //         springs = springs.Substring(0,brokenIndices[0]+brokenSprings[0]);
        //     }
        //     if(brokenIndices.Count > 0 && springs.Length >= springs.Length-brokenIndices[brokenIndices.Count-1]+brokenSprings[0]){
        //         springs = springs.Substring(springs.Length-(springs.Length-brokenIndices[brokenIndices.Count-1])-brokenSprings[0]+1);
        //     }
        //     springs = RemovePadding(springs, ref brokenSprings);
        // }
        

        //overall strategy: for each springRow, if springs == "", there is only one solution, return 1
        //if springs != "", for each ? in springRow, try to create a new springRow from springs + brokenSprings with ? replaced by .
        //if the springRow can be created and results in springs == "", this is a single solution, add to solutions number
        //if the springRow cannot be created, discard it
        //if the springRow can be created and results in springs != "", the process needs to be repeated for each ?->. until all are either "" (add to solutions) or impossible
        //once all ?->. have been tried, return solutions number


    }

    private string RemoveLeadingAndTrailingGroups(string springs, ref List<int>brokenSprings)
    //none of this can be disabled without causing infinite loops
    {
        if (springs.Length > 0 && springs[springs.Length - 1] == '#')
        {
            for(int i = springs.Length -2 ; i > springs.Length - brokenSprings[brokenSprings.Count - 1]; i--){
                if(springs[i]=='.'){throw new Exception("incorrectly solved");}
            }
            if(springs[springs.Length - brokenSprings[brokenSprings.Count - 1]] -1 == '#'){
                throw new Exception("incorrectly solved");
            }
            springs = springs.Substring(0, springs.Length - brokenSprings[brokenSprings.Count - 1]);
            brokenSprings.RemoveAt(brokenSprings.Count - 1);
        }
        if (springs.Length > 0 && springs[0] == '#')
        {
            for(int i = 1; i < brokenSprings[0]; i++){
                if(springs[i]=='.'){throw new Exception("incorrectly solved");}
            }
            if(springs[brokenSprings[0]] == '#'){
                throw new Exception("incorrectly solved");}
            springs = springs.Substring(brokenSprings[0]+1);
            brokenSprings.RemoveAt(0);
        }
        CheckIfSolved(ref springs, ref brokenSprings);
        if(springs.Length >0  && (springs[0] != '?' || springs[springs.Length-1] != '?')){
            return RemovePadding(springs, ref brokenSprings);  
        }
        else 
        CheckIfSolved(ref springs, ref brokenSprings);
        return springs;     
    }

    private string RemovePadding(string springs, ref List<int> brokenSprings)
    {
        while(springs.Length >0  && springs[0]=='.'){
            springs = springs.Substring(1);
        }
        while(springs.Length >0  && springs[springs.Length-1]=='.'){
            springs = springs.Substring(0,springs.Length-1);
        }
        //following code does not affect the number of solutions
        for(int i = 1; i< springs.Length-2; i++){
            if(springs[i] == '.' && springs[i+1] == '.'){
                springs = springs.Substring(0,i)+springs.Substring(i+1);
            }
        }
        CheckIfSolved(ref springs, ref brokenSprings);
        if(springs.Length >0  && (springs[0] == '#' || springs[springs.Length-1] == '#')){
        return RemoveLeadingAndTrailingGroups(springs, ref brokenSprings);}
        else
        CheckIfSolved(ref springs, ref brokenSprings);
        return springs;
    }

    private void CheckIfSolved(ref string springs, ref List<int> brokenSprings)
    {
        int totalBrokenSprings;
        if (brokenSprings.Count > 1){
            totalBrokenSprings = 0;
            foreach (int brokenSpring in brokenSprings){totalBrokenSprings += brokenSpring;}
            if(springs.Length < totalBrokenSprings+1){throw new Exception("incorrectly solved");}
        }
        if (brokenSprings.Count == 1){
            int remainingBrokenSprings = brokenSprings[0];
        if(springs.Length == remainingBrokenSprings){
            if(remainingBrokenSprings > springs.Length){throw new Exception("incorrectly solved");}
            foreach(char character in springs){
                if(character == '.'){
                    throw new Exception("incorrectly solved");
                }
            }
            brokenSprings.RemoveAt(0);
            springs = "";
        }
        // removing this block reduced the total number of solutions
        // else {
        //     int knownBrokenSprings = 0;
        //     foreach(char character in springs){
        //         if(character == '#'){knownBrokenSprings++;}
        //     }
        //     if (knownBrokenSprings == remainingBrokenSprings){
        //         brokenSprings.RemoveAt(0);
        //         springs = "";
        //     }
        // }
        }
        if (brokenSprings.Count == 0){
            foreach(char character in springs){
                if(character == '#'){
                    throw new Exception("incorrectly solved");
            }
            }
            springs = "";
            }
        // the following code caused collisions between different solution paths:
        // totalBrokenSprings = 0;
        // foreach (int brokenSpring in brokenSprings){totalBrokenSprings += brokenSpring;}
        // if(totalBrokenSprings == springs.Length-(brokenSprings.Count-1)){
        //     brokenSprings = new List<int>();
        //     springs = "";
        // }
    }

    string springs;
List<int> brokenSprings = new List<int>();

public int NumberOfPossibleSolutions(){
    if(springs.Length == 0){return 1;}
    List<int> questionIndices = new List<int>();
    for(int i = 0; i < springs.Length; i++){
        if(springs[i] == '?'){questionIndices.Add(i);}
    }
    int totalSolutions = 0;
    List<SpringRow> springRows = new List<SpringRow>{this};
    while(springRows.Count > 0){
        string currentSprings = springRows[0].springs;
        StringBuilder currentBrokenSprings = new StringBuilder();
        currentBrokenSprings.Append(" ");
        foreach(int i in springRows[0].brokenSprings){currentBrokenSprings.Append(i + ",");}

        string springsA = currentSprings.Substring(0,questionIndices[0])+'.'+currentSprings.Substring(questionIndices[0]+1) + currentBrokenSprings.ToString();
        string springsB = currentSprings.Substring(0,questionIndices[0])+'#'+currentSprings.Substring(questionIndices[0]+1) + currentBrokenSprings.ToString();
        springRows.RemoveAt(0);
        try{SpringRow springRowA = new SpringRow(springsA);
        if (springRowA.springs.Length == 0)
        {totalSolutions++;}
        else{springRows.Add(springRowA);}
        }
        catch(Exception){};
        try{SpringRow springRowB = new SpringRow(springsB);
        if (springRowB.springs.Length == 0)
        {totalSolutions++;}
        else{springRows.Add(springRowB);}
        }
        catch(Exception){};
    }
    return totalSolutions;
}

}