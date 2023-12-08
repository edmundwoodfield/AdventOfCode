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
        List<double> seedList = new List<double>();
        List<double> soilList = new List<double>();
        List<double> fertilizerList = new List<double>();
        List<double> waterList = new List<double>();
        List<double> lightList = new List<double>();
        List<double> temperatureList = new List<double>();
        List<double> humidityList = new List<double>();
        List<double> locationList = new List<double>();
        List<double>[] listList = new List<double>[] { seedList, soilList, fertilizerList, waterList, lightList, temperatureList, humidityList, locationList };

        MatchCollection winning = Regex.Matches(seeds, "\\d+");
        foreach (Match match in winning)
        {
            seedList.Add(double.Parse(match.Value));
        }
        List<List<double>> seedToSoilData = new List<List<double>>();
        List<List<double>> soilToFertilizerData = new List<List<double>>();
        List<List<double>> fertilizerToWaterData = new List<List<double>>();
        List<List<double>> waterToLightData = new List<List<double>>();
        List<List<double>> lightToTemperatureData = new List<List<double>>();
        List<List<double>> temperatureToHumidityData = new List<List<double>>();
        List<List<double>> humidityToLocationData = new List<List<double>>();
        List<List<double>>[] dataListList = new List<List<double>>[] { seedToSoilData, soilToFertilizerData, fertilizerToWaterData, waterToLightData, lightToTemperatureData, temperatureToHumidityData, humidityToLocationData };

        PopulateData(lines, ref seedToSoilData, ref soilToFertilizerData, ref fertilizerToWaterData, ref waterToLightData, ref lightToTemperatureData, ref temperatureToHumidityData, ref humidityToLocationData);
        TraverseLists(ref listList, dataListList);
        double lowestLocation = locationList[0];
        foreach(double location in locationList){
            if(location< lowestLocation){
                lowestLocation = location;
            }
        }
        Console.WriteLine("The lowest location is " + lowestLocation);
    }

    private static void TraverseLists(ref List<double>[] listList, List<List<double>>[] dataListList)
    {
        for (int i = 0; i < 7; i++)
            foreach (double source in listList[i])
            {
                bool mapped = false;
                foreach (List<double> mapLine in dataListList[i])
                {
                    if (source >= mapLine[1] && source < mapLine[1] + mapLine[2])
                    {
                        double dest = source - mapLine[1] + mapLine[0];
                        listList[i + 1].Add(dest);
                        mapped = true;
                    }
                }
                if (!mapped)
                {
                    listList[i + 1].Add(source);
                }
            }
    }

    private static void PopulateData(string[] lines, ref List<List<double>> seedToSoil, ref List<List<double>> soilToFertilizer,ref List<List<double>> fertilizerToWater,ref List<List<double>> waterToLight,ref List<List<double>> lightToTemperature,ref List<List<double>> temperatureToHumidity,ref List<List<double>> humidityToLocation)
    {
        int seedToSoilIndex = 0;
        int soilToFertilizerIndex = 0;
        int fertilizerToWaterIndex = 0;
        int waterToLightIndex = 0;
        int lightToTemperatureIndex = 0;
        int temperatureToHumidityIndex = 0;
        int humidityToLocationIndex = 0;
        for (int i = 1; i < lines.Length; i++)
        {
            if (lines[i].Contains("seed-to-soil map:"))
            {
                seedToSoilIndex = i;
            }
            if (lines[i].Contains("soil-to-fertilizer map:"))
            {
                soilToFertilizerIndex = i;
            }
            if (lines[i].Contains("fertilizer-to-water map:"))
            {
                fertilizerToWaterIndex = i;
            }
            if (lines[i].Contains("water-to-light map:"))
            {
                waterToLightIndex = i;
            }
            if (lines[i].Contains("light-to-temperature map:"))
            {
                lightToTemperatureIndex = i;
            }
            if (lines[i].Contains("temperature-to-humidity map:"))
            {
                temperatureToHumidityIndex = i;
            }
            if (lines[i].Contains("humidity-to-location map:"))
            {
                humidityToLocationIndex = i;
            }
        }
        for(int i = seedToSoilIndex + 1; i < soilToFertilizerIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            seedToSoil.Add(mapLine);
        }
        for(int i = soilToFertilizerIndex + 1; i < fertilizerToWaterIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            soilToFertilizer.Add(mapLine);
        }
        for(int i = fertilizerToWaterIndex + 1; i < waterToLightIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            fertilizerToWater.Add(mapLine);
        }
        for(int i = waterToLightIndex + 1; i < lightToTemperatureIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            waterToLight.Add(mapLine);
        }
        for(int i = lightToTemperatureIndex + 1; i < temperatureToHumidityIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            lightToTemperature.Add(mapLine);
        }
        for(int i = temperatureToHumidityIndex + 1; i < humidityToLocationIndex -1; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            temperatureToHumidity.Add(mapLine);
        }
        for(int i = humidityToLocationIndex + 1; i < lines.Length; i++){
            List<double> mapLine = new List<double>();
            MatchCollection matches = Regex.Matches(lines[i],"\\d+");
            foreach(Match match in matches){
                mapLine.Add(double.Parse(match.Value));
            }
            humidityToLocation.Add(mapLine);
        }
    }
    
}