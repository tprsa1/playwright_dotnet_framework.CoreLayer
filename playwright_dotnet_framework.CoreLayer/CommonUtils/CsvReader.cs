

public class CsvReader
{
    public static List<string> ReadCsvFile(string fileName)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), $"TestData/{fileName}.csv");
        List<string> values = new List<string>();

        try
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    values.Add(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error reading the CSV file: " + ex.Message);
        }

        return values;
    }
}
