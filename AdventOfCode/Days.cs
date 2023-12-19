using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class Days{
    public static void Day1(){
        string path = @"..\..\..\inputs\day1input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        Numbers.CalculateDay1Total(lines);
    }

    public static void Day2(){
        string path = @"..\..\..\inputs\day2input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        double total = 0;
        double powerTotal = 0;
        foreach(string line in lines){
            CubeBag cubeBag = new CubeBag(line);
            powerTotal += cubeBag.powerOfColours;
            if(cubeBag.maxObservedRed <= 12 && cubeBag.maxObservedGreen <=13 && cubeBag.maxObservedBlue <= 14){
                total += cubeBag.gameId;
            }
        }
        Console.WriteLine("The total is " + total);
        Console.WriteLine("The power total is " + powerTotal);
    }

    public static void Day3(){
        string path = @"..\..\..\inputs\day3input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);    
        List<List<int>> indexList = Numbers.GetIndexLists(lines);
        int counter = 0;
        int total = 0;
        foreach(string line in lines){
            List<int> validInts = Numbers.FilterToNumbersWithValidIndices(line,indexList[counter]);
            counter ++;
            foreach(int validInt in validInts){
                total += validInt;
            }
        }
        Console.WriteLine("The total is " + total);
        List<List<int>> gearList = Numbers.GetGearLinesIndexList(lines);
        counter = 0;
        foreach(List<int> gearLine in gearList){
            counter ++;
        }
        List<List<int>> finalGearList = Numbers.FilterToValidGearLinesIndexList(lines);
        int gearTotal = 0;
        foreach(List<int> gear in finalGearList){
            gearTotal += (gear[0]*gear[1]);
        }
        Console.WriteLine("The total of all the gear powers is " + gearTotal);
    }
    public static void Day4()
    {
        string path = @"..\..\..\inputs\day4input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        double totalScratchcardScore = 0;
        foreach (string line in lines)
        {
            Scratchcard scratchcard = new Scratchcard(line);
            if (scratchcard.numberOfMatches != 0)
            {
                totalScratchcardScore += Math.Pow(2, scratchcard.numberOfMatches - 1);
            }
        }
        Console.WriteLine("The total for the scratchcards is " + totalScratchcardScore);

        Dictionary<int, Scratchcard> scratchcards = new Dictionary<int, Scratchcard>();
        foreach (string line in lines)
        {
            Scratchcard scratchcard = new Scratchcard(line);
            scratchcards.Add(scratchcard.cardNumber, scratchcard);
        }
        int totalNumberOfCards = CalculateTotalNumberOfCards(scratchcards);
        Console.WriteLine("The total number of scratchcards is " + totalNumberOfCards);
    }

    private static int CalculateTotalNumberOfCards(Dictionary<int, Scratchcard> scratchcards)
    {
        int totalNumberOfCards = 0;
        for (int i = 1; i <= scratchcards.Count; i++)
        {
            int counter = 1;
            while (scratchcards[i].numberOfMatches > 0)
            {
                scratchcards[(i + counter)].quantity += scratchcards[i].quantity;
                scratchcards[i].numberOfMatches--;
                counter++;
            }
        }
        for (int i = 1; i <= scratchcards.Count; i++)
        {
            totalNumberOfCards += scratchcards[i].quantity;
        }

        return totalNumberOfCards;
    }

    public static void Day5()
    {
        string path = @"..\..\..\inputs\day5input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        string seeds = lines[0];
        List<double>[] listList = GenerateListList();
        List<List<double>>[] dataListList = GenerateDataListList();
        Day5PopulateData(lines, ref dataListList);
        Day5GenerateSeedsPart1(seeds, listList[0]);
        double lowestLocation = Day5Part1Calculation(listList, dataListList);
        Console.WriteLine("The lowest location is " + lowestLocation);
        List<List<double>> part2Seeds = Day5GenerateSeedRanges(seeds);
        double? part2LowestLocation = null;
        while (part2Seeds.Count > 0) {
            List<double>[] Part2ListList = GenerateListList();
            Part2ListList[0] = part2Seeds[0];
            bool rangeCheck = false;
            double workingLowestLocation = Day5Part2Calculation(Part2ListList,dataListList, ref rangeCheck);
            if(rangeCheck){
                if(part2LowestLocation == null || workingLowestLocation < part2LowestLocation){
                    part2LowestLocation = workingLowestLocation;
                };
            }
            else{
                double diff = part2Seeds[0][1]-part2Seeds[0][0];
                if (diff == 1){
                part2Seeds.Add(new List<double>{part2Seeds[0][0],part2Seeds[0][0]});
                part2Seeds.Add(new List<double>{part2Seeds[0][1],part2Seeds[0][1]});
                }
                else
                {if (diff % 2 == 1){diff ++;}
                double halfDiff = diff/2;
                part2Seeds.Add(new List<double>{part2Seeds[0][0],part2Seeds[0][0]+halfDiff});
                part2Seeds.Add(new List<double>{part2Seeds[0][0]+halfDiff+1,part2Seeds[0][1]});}
            }
            part2Seeds.RemoveAt(0);
        }
        Console.WriteLine("the lowest location for part 2 is " + part2LowestLocation);
    }

    private static List<List<double>>[] GenerateDataListList()
    {
        List<List<double>> seedToSoilData = new List<List<double>>();
        List<List<double>> soilToFertilizerData = new List<List<double>>();
        List<List<double>> fertilizerToWaterData = new List<List<double>>();
        List<List<double>> waterToLightData = new List<List<double>>();
        List<List<double>> lightToTemperatureData = new List<List<double>>();
        List<List<double>> temperatureToHumidityData = new List<List<double>>();
        List<List<double>> humidityToLocationData = new List<List<double>>();
        return new List<List<double>>[] { seedToSoilData, soilToFertilizerData, fertilizerToWaterData, waterToLightData, lightToTemperatureData, temperatureToHumidityData, humidityToLocationData };

    }

    private static List<double>[] GenerateListList()
    {
        List<double> seedList = new List<double>();
        List<double> soilList = new List<double>();
        List<double> fertilizerList = new List<double>();
        List<double> waterList = new List<double>();
        List<double> lightList = new List<double>();
        List<double> temperatureList = new List<double>();
        List<double> humidityList = new List<double>();
        List<double> locationList = new List<double>();
        return new List<double>[] { seedList, soilList, fertilizerList, waterList, lightList, temperatureList, humidityList, locationList };

    }

    private static double Day5Part1Calculation(List<double>[] listList, List<List<double>>[] dataListList)
    {
    bool dummyBool = false;
    return Day5Part2Calculation(listList,dataListList, ref dummyBool);
    }
    private static double Day5Part2Calculation(List<double>[] listList, List<List<double>>[] dataListList, ref bool singlePath)
    {
        double lowestLocation = 0;
        if(TraverseLists(listList, dataListList)){
            singlePath = true;
        }
        else{
            singlePath = false;
        }
        lowestLocation = listList[7][0];
        foreach (double location in listList[7])
        {
            if (location < lowestLocation)
            {
                lowestLocation = location;
            }
        }
        return lowestLocation;
    }
    

    private static void Day5SortLists(List<List<double>>[]dataList)
    {
        foreach(List<List<double>>unsortedList in dataList){
            unsortedList.Sort((a,b) => a[0].CompareTo(b[0]));
            bool rangesComplete = false;
            while (!rangesComplete){
                double i = 0;
                for(int j = 0; j < unsortedList.Count; j++){
                    if(i < unsortedList[j][0]){
                        unsortedList.Add(new List<double>{i,i,unsortedList[j][0]-i});
                    }
                    i = unsortedList[j][0]+unsortedList[j][2]+1;
                }
                rangesComplete = true;
                unsortedList.Sort((a,b) => a[0].CompareTo(b[0]));
            }
            }
    }


    private static void Day5GenerateSeedsPart1(string seeds, List<double> seedList)
    {
        MatchCollection matches = Regex.Matches(seeds, "\\d+");
        foreach (Match match in matches)
        {
            seedList.Add(double.Parse(match.Value));
        }
    }

    private static List<List<double>> Day5GenerateSeedRanges(string seeds){
        MatchCollection matches = Regex.Matches(seeds, "\\d+");
        List<List<double>> ranges = new List<List<double>>();
        List<double> doubles = new List<double>();
        foreach (Match match in matches)
        {
            doubles.Add(double.Parse(match.Value));
        }
        for(int i = 0; i < doubles.Count; i+=2){
            double bottom = doubles[i];
            double top = doubles[i]+doubles[i+1];
            ranges.Add(new List<double>{bottom,top});
        }
        return ranges;
    }

    private static bool TraverseLists(List<double>[] listList, List<List<double>>[] dataListList)
    {
        List<int> pathAndLocation = new List<int>();
        bool singlePath = false;
        for (int i = 0; i < dataListList.Length; i++){
            foreach (double source in listList[i]){
                foreach (List<double> mapLine in dataListList[i]){
                    if (source >= mapLine[1] && source <= mapLine[1] + mapLine[2]){
                        double dest = source - mapLine[1] + mapLine[0];
                        listList[i + 1].Add(dest);
                    }
                }
            }
        
        }
        if(listList[0][0] == listList[0][1] || listList[7][0]-listList[0][0] == listList[7][1]-listList[0][1]){
            singlePath = true;
        }
        return singlePath;
    }

    private static void Day5PopulateData(string[] lines, ref List<List<double>>[] dataListList)
    {
        int seedToSoilIndex = 0;
        int soilToFertilizerIndex = 0;
        int fertilizerToWaterIndex = 0;
        int waterToLightIndex = 0;
        int lightToTemperatureIndex = 0;
        int temperatureToHumidityIndex = 0;
        int humidityToLocationIndex = 0;
        int endIndex = 0;
        List<int> indices = new List<int>{seedToSoilIndex,soilToFertilizerIndex,fertilizerToWaterIndex,waterToLightIndex,lightToTemperatureIndex,temperatureToHumidityIndex,humidityToLocationIndex,endIndex};
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Contains("seed-to-soil map:"))
            {
                indices[0] = i;
            }
            if (lines[i].Contains("soil-to-fertilizer map:"))
            {
                indices[1] = i;
            }
            if (lines[i].Contains("fertilizer-to-water map:"))
            {
                indices[2] = i;
            }
            if (lines[i].Contains("water-to-light map:"))
            {
                indices[3] = i;
            }
            if (lines[i].Contains("light-to-temperature map:"))
            {
                indices[4] = i;
            }
            if (lines[i].Contains("temperature-to-humidity map:"))
            {
                indices[5] = i;
            }
            if (lines[i].Contains("humidity-to-location map:"))
            {
                indices[6] = i;
            }
            indices[7] = lines.Length+1;
        }
        for( int j = 0; j < indices.Count - 1; j++){
            for(int i = indices[j]; i < indices[j+1] -1; i++){
                List<double> mapLine = new List<double>();
                MatchCollection matches = Regex.Matches(lines[i],"\\d+");
                foreach(Match match in matches){
                    mapLine.Add(double.Parse(match.Value));
                }
                if(mapLine.Count != 0){
                    dataListList[j].Add(mapLine);}
            }
        }
        Day5SortLists(dataListList);
    }
    
    public static void Day6(){
        string path = @"..\..\..\inputs\day6input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        List<List<double>> races = GenerateRaces(lines,1);
        List<int> numberOfPossibleVictories = CalculateNumberOfPossibleVictories(races);
        double productOfPossibleVictories = 1;
        for(int i = 0; i < numberOfPossibleVictories.Count; i ++){
            productOfPossibleVictories *= numberOfPossibleVictories[i];
        }
        Console.WriteLine("the product of all the possible victories is " + productOfPossibleVictories);
        List<List<double>> racesPart2 = GenerateRaces(lines,2);
        List<double> possibleVictories = new List<double>();
        foreach (List<double> race in racesPart2) {possibleVictories.Add(BulkCalculateNumberOfPossibleVictories(race));}
        Console.WriteLine("the number of possibile victories is " + possibleVictories[0]);

    }

    private static double BulkCalculateNumberOfPossibleVictories(List<double> race)
    {
        double time = race[0];
        double distance = race[1];
        double bottomPossibility = 1;
        double topPossibility = time - 1;
        bool complete = false;
        double lowestKnown = time;
        double highestFailure = 0;
        do
        {

            if ((time - bottomPossibility - 1) * (bottomPossibility - 1) <= distance && (time - bottomPossibility) * bottomPossibility > distance)
            {
                complete = true;
                break;
            }
            if ((time - bottomPossibility) * bottomPossibility > distance)
            {
                lowestKnown = bottomPossibility;
                bottomPossibility = lowestKnown - Math.Round((lowestKnown - highestFailure) / 2);
            }
            if ((time - bottomPossibility) * bottomPossibility <= distance)
            {
                highestFailure = bottomPossibility;
                bottomPossibility = highestFailure + Math.Round((lowestKnown - highestFailure) / 2);
            }
        }
        while (!complete);
        complete = false;
        double highestKnown = bottomPossibility;
        double lowestFailure = time;
        do
        {

            if ((time - (topPossibility + 1)) * (topPossibility + 1) <= distance && (time - topPossibility) * topPossibility > distance)
            {
                complete = true;
                break;
            }
            if ((time - topPossibility) * topPossibility > distance)
            {
                highestKnown = topPossibility;
                topPossibility = highestKnown + Math.Round((lowestFailure - highestKnown) / 2);
            }
            if ((time - topPossibility) * topPossibility <= distance)
            {
                lowestFailure = topPossibility;
                topPossibility = lowestFailure - Math.Round((lowestFailure - highestKnown) / 2);
            }
        }
        while (!complete);
        return topPossibility-bottomPossibility+1;
    }

    private static List<List<double>> GenerateRaces(string[] lines, int part){
        List<List<double>> races = new List<List<double>>();
        MatchCollection? times = null;
        MatchCollection? distances = null;
        if(part != 2){
            times = Regex.Matches(lines[0],"\\d+");
            distances = Regex.Matches(lines[1],"\\d+");}
        if(part == 2){
            times = Regex.Matches(lines[0].Replace(" ",""),"\\d+");
            distances = Regex.Matches(lines[1].Replace(" ",""),"\\d+");
        }
        for (int i = 0; i < times!.Count; i++){
        races.Add(new List<double>{double.Parse(times[i].Value),double.Parse(distances![i].Value)});}
        return races;
    }
    private static List<int> CalculateNumberOfPossibleVictories(List<List<double>> races){
        List<int> numberOfPossibleVictories = new List<int>();
        foreach(List<double> race in races){
            int counter = 0;
            double time = race[0];
            double distance = race[1];
            for(int buttonTime = 1; buttonTime < distance; buttonTime++){
                if((time-buttonTime)*buttonTime > distance){
                    counter++;
                }
            }
            numberOfPossibleVictories.Add(counter);
        }
        return numberOfPossibleVictories;
    }
    public static void Day7(){
        string path = @"..\..\..\inputs\day7input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        List<CamelCard> hands = new List<CamelCard>();
        foreach(string line in lines){
            hands.Add(new CamelCard(line));
        }
        hands.Sort();
        double totalWinnings = 0;
        for(int i = 0; i < hands.Count; i++){
            totalWinnings += hands[i].bid*(i+1);
        }
        Console.WriteLine(value: "The total winnings is " + totalWinnings);

    }
    public static void Day8()
    {
        string path = @"..\..\..\inputs\day8input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        string direction = lines[0];
        Dictionary<string, List<string>> nodeMap = GenerateNodeMap(lines);
        Console.WriteLine("The number of steps taken for part 1 is " + CalculateStepsDay8Part1(direction, nodeMap, "AAA", "ZZZ"));
        List<string> startingNodes = new List<string>();
        foreach (string location in nodeMap.Keys)
        {
            if (location[2] == 'A')
            {
                startingNodes.Add(location);
            }
        }
        Console.WriteLine("The number of steps taken for part 2 is " + CalculateStepsDay8Part2(direction, nodeMap, startingNodes));
    }

    private static double CalculateLowestCommonMultiple(List<int> numbersToFactorise)
    {
        List<Dictionary<int, int>> primeFactorsList = GeneratePrimeFactorsList(numbersToFactorise);
        Dictionary<int, int> factors = new Dictionary<int, int>();
        for (int i = 0; i < primeFactorsList.Count; i++)
        {
            foreach (int factor in primeFactorsList[i].Keys)
            {
                if (!factors.ContainsKey(factor))
                {
                    factors.Add(factor, primeFactorsList[i].GetValueOrDefault(factor));
                }
                else if (factors.GetValueOrDefault(factor) < primeFactorsList[i].GetValueOrDefault(factor))
                {
                    factors.Add(factor, primeFactorsList[i].GetValueOrDefault(factor));
                }

            }
        }
        double total = 1;
        foreach (int key in factors.Keys)
        {
            total *= key * factors.GetValueOrDefault(key);
        }

        return total;
    }

    private static List<Dictionary<int, int>> GeneratePrimeFactorsList(List<int> numbersToFactorise)
    {
        List<Dictionary<int, int>> primeFactorsList = new List<Dictionary<int, int>>();
        foreach (int numberToFactorise in numbersToFactorise)
        {
            int workingInt = numberToFactorise;
            Dictionary<int, int> primeFactors = new Dictionary<int, int>();
            for (int i = 2; i < workingInt / 2; i++)
            {
                while (workingInt % i == 0)
                {
                    primeFactors.Add(i, primeFactors.GetValueOrDefault(i, 0) + 1);
                    primeFactors.Add(workingInt / i, primeFactors.GetValueOrDefault(workingInt / i, 0) + 1);
                    workingInt = workingInt / i;
                }
            }
            primeFactorsList.Add(primeFactors);
        }

        return primeFactorsList;
    }

    private static double CalculateStepsDay8Part2(string direction, Dictionary<string, List<string>> nodeMap, List<string> startingNodes){
        int counter = 0;
        int fullCounters = 0;
        List<int> stepsToZ = new List<int>();
        foreach(string startNode in startingNodes){
            counter = 0;
            fullCounters = 0;
            string location = startNode;
            do{
            if (direction[counter] == 'L')
        {
            location = nodeMap.GetValueOrDefault(location, new List<string>())[0];
        }
        else { location = nodeMap.GetValueOrDefault(location, new List<string>())[1]; }
        counter++;
        if (counter >= direction.Length)
        {
            fullCounters++;
            counter = 0;
        }}
        while(location[2] != 'Z');        
        stepsToZ.Add(fullCounters * direction.Length + counter);
        }
        return CalculateLowestCommonMultiple(stepsToZ);
    }

    private static int CalculateStepsDay8Part1(string direction, Dictionary<string, List<string>> nodeMap, string startLocation, string endLocation)
    {
        int counter = 0;
        int fullCounters = 0;
        string location = startLocation;
        do
        {
            if (direction[counter] == 'L')
            {
                location = nodeMap.GetValueOrDefault(location, new List<string>())[0];
            }
            else { location = nodeMap.GetValueOrDefault(location, new List<string>())[1]; }
            counter++;
            if (counter >= direction.Length)
            {
                fullCounters++;
                counter = 0;
            }
        }
        while (location != endLocation);
        return fullCounters * direction.Length + counter;
    }

    private static Dictionary<string, List<string>> GenerateNodeMap(string[] lines)
    {
        Dictionary<string, List<string>> nodeMap = new Dictionary<string, List<string>>();
        for (int i = 2; i < lines.Length; i++)
        {
            string key = lines[i].Split("=")[0].Trim();
            string left = lines[i].Split("=")[1].Substring(2, 3);
            string right = lines[i].Split("=")[1].Substring(7, 3);
            nodeMap.Add(key, new List<string> { left, right });
        }

        return nodeMap;
    }

    public static void Day9(){
        string path = @"..\..\..\inputs\day9input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        List<List<long>> longsList = new List<List<long>>();
        foreach(string line in lines){
            List<long> longs = new List<long>();
            MatchCollection matches = Regex.Matches(line, "-?\\d+");
            foreach (Match match in matches){
                longs.Add(long.Parse(match.Value));
                }
            longsList.Add(longs);
            }
            List<long> finalLongs = new List<long>();
            List<long> previousLongs = new List<long>();
            foreach (List<long> longLine in longsList){
                List<List<long>> allStages = new List<List<long>>();
                List<long> newList = longLine;
                while(!DifferencesAllZero(newList)) {
                    allStages.Add(newList);
                    newList = CalculateDifferences(newList);
                }
                finalLongs.Add(NextLong(allStages));
                previousLongs.Add(PreviousLong(allStages));
            }
            long total = 0;
            foreach(long finalLong in finalLongs){total += finalLong;}
            Console.WriteLine("the sum of all the next values is " + total);
            total = 0;
            foreach(long previousLong in previousLongs){total += previousLong;}
            Console.WriteLine("the sum of all the previous values is " + total);
        }

    private static List<long> CalculateDifferences (List<long> longs){
        List<long> differences = new List<long>();
        for (int i = 0; i < longs.Count - 1; i++){
            differences.Add(longs[i+1]-longs[i]);
        }
        return differences;
    }

    private static bool DifferencesAllZero(List<long> longs){
        foreach(long comparedLong in longs){
            if(comparedLong != 0){
                return false;
            }
        }
        return true;
    }

    private static long NextLong(List<List<long>> differenceLists){
        long workingLong = 0;
        for(int i = differenceLists.Count - 1; i >= 0; i--){
            workingLong += differenceLists[i][differenceLists[i].Count - 1];
        }
        return workingLong;
    }

    private static long PreviousLong(List<List<long>> differenceLists){
        long workingLong = 0;
        for(int i = differenceLists.Count - 1; i >= 0; i--){
            workingLong = differenceLists[i][0] - workingLong;
        }
        return workingLong;
    }

    public static void Day10(){
        string path = @"..\..\..\inputs\day10input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        List<int> startingLocation = new List<int>();
        for(int i = 0; i < lines.Length; i++){
            for(int j = 0; j < lines[i].Length; j++){
                if(lines[i][j] == 'S'){
                    startingLocation.Add(i);
                    startingLocation.Add(j);
                    break;
                }
            }
        }
        List<int> currentLocation = startingLocation;
        List<int> ignorePoints = new List<int>();
        List<List<int>> route = new List<List<int>>{currentLocation};
        MakeMove(lines, ref currentLocation, ref ignorePoints, route);
        while(!(currentLocation[0] == startingLocation[0] && currentLocation[1] == startingLocation[1])){
        MakeMove(lines, ref currentLocation, ref ignorePoints, route);}
        Console.WriteLine("total number of steps back to start is " + route.Count);
        Console.WriteLine("the number of steps to the furthest point is " + route.Count/2);
        lines = ReplaceStartWithShape(lines,route);
        long inside = CountInsideCoords(lines,route);
        Console.WriteLine("the number of squares inside the pipes is " + inside);

    }

    private static long CountInsideCoords(string[] lines, List<List<int>> route)
    {
        List<char> verticals = new List<char>{'|','J','7','F','L'};
        long total = 0;
        for(int i = 0; i < lines.Length; i++){
            bool inside = false;
            char switchChar = '-';
            for(int j = 0; j < lines[i].Length; j++){
                char examinedChar = lines[i][j];
                if(route.Any(p => p[0] == i && p[1] == j)){
                    if(verticals.Contains(examinedChar) && (!(switchChar == 'F' && examinedChar == '7')) && (!(switchChar == 'L' && examinedChar == '7'))){
                    switchChar = examinedChar;
                    if(examinedChar != 'F'){
                    inside = !inside;}}
                    else if (verticals.Contains(examinedChar)){switchChar = examinedChar;}}

                
                if(inside && !route.Any(p => p[0] == i && p[1] == j)){
                    total++;};
            }

        }
        return total;
    }
    

    private static string[] ReplaceStartWithShape(string[] lines, List<List<int>> route){
        string[] output = lines;
        List<int> s = route[0];
        List<int> first = route[1];
        List<int> last = route[route.Count-2];
        char shape;
        if(first[1] == last[1]){shape = '|';}
        else if(first[0] == last[0]){shape = '-';}
        else if ((first[0]-last[0] == 1 && first[1]-last[1]== 1 && first[0]==s[0])||(last[0]-first[0] == 1 && last[1]-first[1] == 1 && last[0]==s[0])){shape = 'L';}
        else if ((first[0]-last[0] == 1 && last[1]-first[1]== 1 && first[0]==s[0])||(last[0]-first[0] == 1 && first[1]-last[1] == 1 && last[0]==s[0])){shape = 'J';}
        else if ((first[0]-last[0] == 1 && first[1]-last[1]== 1 && first[1]==s[1])||(last[0]-first[0] == 1 && last[1]-first[1] == 1 && last[1]==s[1])){shape = '7';}
        else if ((first[0]-last[0] == 1 && last[1]-first[1]== 1 && first[1]==s[1])||(last[0]-first[0] == 1 && first[1]-last[1] == 1 && last[1]==s[1])){shape = 'F';}
        else throw new Exception("failed to replace S");
        string newLine = output[s[0]].Substring(0,s[1])+shape+output[s[0]].Substring(s[1]+1,output[s[0]].Length-s[1]-1);
        output[s[0]]=newLine;
        return output;

    }

    private static void MakeMove(string[] lines, ref List<int> currentLocation, ref List<int> ignorePoints, List<List<int>> route)
    {
        List<int> nextLocation = IdentifyNextMove(lines, currentLocation, ref ignorePoints);
        currentLocation = nextLocation;
        route.Add(currentLocation);        
    }


    private static List<List<int>> GenerateGrid(List<int> startingLocation)
    {
        return new List<List<int>>{
        new List<int>{startingLocation[0]-1,startingLocation[1]},
        new List<int>{startingLocation[0],startingLocation[1]+1},
        new List<int>{startingLocation[0]+1,startingLocation[1]},
        new List<int>{startingLocation[0],startingLocation[1]-1},
        };
    }
    private static readonly List<char> north = new List<char>{'|','7','F','S'};
    private static readonly List<char> east = new List<char>{'-','J','7','S'};
    private static readonly List<char> south = new List<char>{'|','L','J','S'};
    private static readonly List<char> west = new List<char>{'-','L','F','S'};
    private static List<int> IdentifyNextMove(string[] lines, List<int> startingLocation, ref List<int> ignorePoints)
    {
        char shape = lines[startingLocation[0]][startingLocation[1]];
        switch(shape){
            case '|': {ignorePoints.Add(1);ignorePoints.Add(3);break;}
            case '-': {ignorePoints.Add(0);ignorePoints.Add(2);break;}
            case 'L': {ignorePoints.Add(2);ignorePoints.Add(3);break;}
            case 'J': {ignorePoints.Add(1);ignorePoints.Add(2);break;}
            case '7': {ignorePoints.Add(0);ignorePoints.Add(1);break;}
            case 'F': {ignorePoints.Add(0);ignorePoints.Add(3);break;}
            default: break;
        }
        List<List<int>> compass = GenerateGrid(startingLocation);
        for(int i = 0; i < 4; i++){
            List<char>point = new List<char>();
            if(i == 0){point = north;}
            else if (i == 1){point = east;}
            else if (i == 2){point = south;}
            else if (i == 3){point = west;}
        if (!ignorePoints.Contains(i)){
            int ignorePoint;
        try{if (point.Contains(lines[compass[i][0]][compass[i][1]])){
            ignorePoint = i+2;
            if(ignorePoint > 3){
                ignorePoint -= 4;
            }
            ignorePoints = new List<int>{ignorePoint};
            return compass[i];
        }}
        catch{
        }}
        }
        throw new Exception("failed to identify valid move");
    }
    public static void Day11()
    {
        string path = @"..\..\..\inputs\day11input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        List<string> rows = ExpandSpace(lines);
        Dictionary<int, List<int>> starMap = GenerateStarMap(rows);
        long totalDistances = 0;
        foreach(int star in starMap.Keys){
            long distances = 0;
            foreach(int otherStar in starMap.Keys){
                distances += Math.Abs(starMap.GetValueOrDefault(star,new List<int>{0,0})[0]-starMap.GetValueOrDefault(otherStar,new List<int>{0,0})[0]);
                distances += Math.Abs(starMap.GetValueOrDefault(star,new List<int>{0,0})[1]-starMap.GetValueOrDefault(otherStar,new List<int>{0,0})[1]);
            }
            totalDistances += distances;
        }
        Console.WriteLine("Total distances between all stars is " + totalDistances/2);
    }

    private static Dictionary<int, List<int>> GenerateStarMap(List<string> rows)
    {
        Dictionary<int, List<int>> starMap = new Dictionary<int, List<int>>();
        int counter = 0;
        for (int i = 0; i < rows.Count; i++)
        {
            for (int j = 0; j < rows[0].Length; j++)
            {
                if (rows[i][j] == '#')
                {
                    starMap.Add(counter, new List<int> { i, j });
                    counter++;
                }
            }
        }
        return starMap;
    }

    private static List<string> ExpandSpace(string[] lines)
    {
        List<string> rows = new List<string>();
        for (int i = 0; i < lines.Length; i++)
        {
            bool empty = true;
            foreach (char character in lines[i])
            {
                if (character != '.')
                {
                    empty = false;
                }
            }
            if (empty) { rows.Add(lines[i]); rows.Add(lines[i]); }
            else { rows.Add(lines[i]); }
        }
        List<int> emptyIndices = new List<int>();
        for (int i = 0; i < lines[0].Length; i++)
        {
            bool empty = true;
            foreach (string line in lines)
            {
                if (line[i] != '.')
                {
                    empty = false;
                }
            }
            if (empty) { emptyIndices.Add(i); }
        }
        int counter = 0;
        foreach (int index in emptyIndices)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                rows[i] = rows[i].Substring(0, index+counter) + '.' + rows[i].Substring(index+counter);
            }
            counter++;
        }
        return rows;
    }
}