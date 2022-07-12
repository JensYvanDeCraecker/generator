var numbers = GenerateNumbers();
var seed = (DateTime.Now - new DateTime(1998, 06, 30)).Ticks;
var rnd = new Random((int)seed);
var maxRows = 9;
var maxNumbers = 8;

for (var currentRow = 1; currentRow <= maxRows; currentRow++)
{
    var row = new List<int>();
    var numbersToGenerate = maxNumbers;
    if (numbers.Count < maxNumbers)
    {
        numbersToGenerate -= numbers.Count;
        row.AddRange(numbers);
        numbers = GenerateNumbers(numbers);
    }
    for (var currentNumber = 1; currentNumber <= numbersToGenerate; currentNumber++)
    {
        var index = rnd.Next(0, numbers.Count);
        row.Add(numbers[index]);
        numbers.RemoveAt(index);
    }

    Console.WriteLine($"{currentRow}: {string.Join(" - ", row.OrderBy(v => v))}");
}

static IList<int> GenerateNumbers(IEnumerable<int>? except = null) => Enumerable.Range(1, 50).Except(except ?? Enumerable.Empty<int>()).ToList();