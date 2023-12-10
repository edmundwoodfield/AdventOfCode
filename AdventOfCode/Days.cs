using System.Text.RegularExpressions;

public class Days{
    public static void Day1(){
        string path = @"..\..\..\day1input.txt";
        string[] lines;
        lines = File.ReadAllLines(path);
        Numbers.CalculateDay1Total(lines);
    }

    public static void Day2(){
        string path = @"..\..\..\day2input.txt";
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
        string path = @"..\..\..\day3input.txt";
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
        string path = @"..\..\..\day4input.txt";
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
        string path = @"..\..\..\day5input.txt";
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
    
}