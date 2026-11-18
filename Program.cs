using System.Diagnostics;
using System.IO.Pipelines;

void PrintFrequencies(Dictionary<string, int> frequencies)
{
    if (frequencies.Count == 0)
    {
        Console.WriteLine("Нет слов для анализа");
        return;
    }

    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        Console.WriteLine($"{entry.Key}: {entry.Value}");
    }
}

void AddWord(Dictionary<string, int> frequencies, string word)
{
    if (frequencies.TryGetValue(word, out int count))
    {
        frequencies[word] = count + 1;
    }
    else
    {
        frequencies.Add(word, 1);
    }
}

int SumFrequencies(Dictionary<string, int> frequencies)
{
    int sum = 0;
    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        sum += entry.Value;
    }
    return sum;
}

// Шаг 8

// 1
HashSet<string> excludedWords = new HashSet<string> { "и", "в", "на" };

// 2
Console.Write("Введите слово для добавления в исключения: ");
string text1 = Console.ReadLine().Trim().ToLowerInvariant();
if (excludedWords.Add(text1))
{
    Console.WriteLine("Исключение добавлено");
}
else
{
    Console.WriteLine("Такого исключения нет");
}

// Шаг 3
Console.Write("Введите слово для удаления из исключения: ");
string textDelete = Console.ReadLine().Trim().ToLowerInvariant();
if (excludedWords.Remove(textDelete))
{
    Console.WriteLine("Исключение удалено");
}
else
{
    Console.WriteLine("Такого исключения нет");
}
Console.WriteLine($"Количество исключений: {excludedWords.Count}");

// Шаг 4
Console.Write("Введите текст: ");
string text = Console.ReadLine() ?? "";
string[] words = text.ToLowerInvariant().Split(' ', StringSplitOptions.RemoveEmptyEntries);

// Шаг 5
Dictionary<string, int> frequencies = new Dictionary<string, int>();
foreach (string word in words)
{
    if (!excludedWords.Contains(word))
    {
        AddWord(frequencies, word);
    }
}

// Шаг 6
Console.WriteLine();
Console.WriteLine(">>> Анализ слов <<<");
Console.WriteLine();

Console.WriteLine($"Исходная строка: [{text}]");
Console.WriteLine($"Всего слов: {words.Length}");
Console.WriteLine($"Пропущено слов: {words.Length - SumFrequencies(frequencies)}");
Console.WriteLine($"Учтено слов: {SumFrequencies(frequencies)}");
Console.WriteLine($"Разных учтенных слов: {frequencies.Count}");

Console.WriteLine();
Console.WriteLine("Частоты слов: ");
PrintFrequencies(frequencies);

Console.Write("Слово для поиска: ");
string slovo = Console.ReadLine().Trim().ToLowerInvariant();
if (frequencies.TryGetValue(slovo, out int found))
{
    Console.WriteLine($"Количество вхождений: {found}");
}
else
{
    Console.WriteLine("Слово не найдено");
}

// Самостоятельное задание № 1
int CountFrequentWords(Dictionary<string, int> frequencies, int limit)
{
    int count = 0;
    foreach (KeyValuePair<string, int> entry in frequencies)
    {
        if (entry.Value >= limit)
        {
            count++;
        }
    }
    return count;
}
Console.WriteLine();
Console.Write("Введите частоту (от 1 до 100): ");
string input = Console.ReadLine();

if (int.TryParse(input, out int limit) && limit >= 1 && limit <= 100)
{
    int count = CountFrequentWords(frequencies, limit);
    Console.WriteLine($"Количество слов с частотой >= {limit}: {count}");
}
else
{
    Console.WriteLine("Нужно ввести целое число от 1 до 100.");
}