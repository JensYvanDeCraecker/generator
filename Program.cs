var numbers = new List<int>();
var stars = new List<int>();
var currentLotteryDate = GetCurrentLotteryDate();
var seed = (currentLotteryDate - new DateTime(1998, 06, 30)).Ticks;
var rnd = new Random((int)seed);
var maxRows = 10;
var maxNumbers = 50;
var maxStars = 12;
var maxNumbersPerRow = 5;
var maxStarsPerRow = 2;

Console.WriteLine($"Your lucky numbers for {currentLotteryDate}");

for (var currentRow = 1; currentRow <= maxRows; currentRow++)
{
    var rowNumbers = GenerateRow(numbers, maxNumbersPerRow, maxNumbers, rnd).OrderBy(v => v);
    var rowStars = GenerateRow(stars, maxStarsPerRow, maxStars, rnd).OrderBy(v => v);
    Console.WriteLine($"{currentRow}: {string.Join(" - ", rowNumbers.Select(v => v.ToString()).Concat(rowStars.Select(v => "*" + v)))}");
}

static DateTime GetCurrentLotteryDate()
{
    var lotteryTime = new TimeOnly(21, 30);
    var lotteryDays = new HashSet<DayOfWeek> { DayOfWeek.Tuesday, DayOfWeek.Friday };
    var current = DateTime.Now;

    while (!lotteryDays.Contains(current.DayOfWeek))
    {
        current = current.AddDays(1);
    }

    return DateOnly.FromDateTime(current).ToDateTime(lotteryTime);
}

static IEnumerable<int> GenerateRow(List<int> numbers, int maxRowNumbers, int maxNumbers, Random rnd)
{
    var row = new List<int>();
    var numbersToGenerate = maxRowNumbers;
    if (numbers.Count < maxRowNumbers)
    {
        numbersToGenerate -= numbers.Count;
        row.AddRange(numbers);
        numbers.Clear();
        numbers.AddRange(GenerateNumbers(maxNumbers, numbers));
    }
    for (var currentNumber = 1; currentNumber <= numbersToGenerate; currentNumber++)
    {
        var index = rnd.Next(0, numbers.Count);
        row.Add(numbers[index]);
        numbers.RemoveAt(index);
    }
    return row;
}

static IEnumerable<int> GenerateNumbers(int maxNumbers, IEnumerable<int> except) => Enumerable.Range(1, maxNumbers).Except(except);