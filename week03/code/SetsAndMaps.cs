using System.Text.Json;

public static class SetsAndMaps
{
    // Problem 1 - Finding Pairs
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();
        var used = new HashSet<string>();

        foreach (var word in words)
        {
            var reversed = new string(new[] { word[1], word[0] });

            if (word != reversed &&
                wordSet.Contains(reversed) &&
                !used.Contains(word) &&
                !used.Contains(reversed))
            {
                pairs.Add($"{word} & {reversed}");
                used.Add(word);
                used.Add(reversed);
            }
        }

        return pairs.ToArray();
    }

    // Problem 2 - Degree Summary
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3];

            if (degrees.ContainsKey(degree))
            {
                degrees[degree]++;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    // Problem 3 - Anagrams
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
        {
            return false;
        }

        var letters = new Dictionary<char, int>();

        foreach (var letter in word1)
        {
            if (letters.ContainsKey(letter))
            {
                letters[letter]++;
            }
            else
            {
                letters[letter] = 1;
            }
        }

        foreach (var letter in word2)
        {
            if (!letters.ContainsKey(letter))
            {
                return false;
            }

            letters[letter]--;

            if (letters[letter] < 0)
            {
                return false;
            }
        }

        return true;
    }

    // Problem 5 - Earthquake JSON Data
    public static string[] EarthquakeDailySummary()
    {
        const string uri =
            "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage =
            new HttpRequestMessage(HttpMethod.Get, uri);

        using var jsonStream =
            client.Send(getRequestMessage).Content.ReadAsStream();

        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection =
            JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var summary = new List<string>();

        if (featureCollection != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                summary.Add(
                    $"{feature.Properties.Place} - Mag {feature.Properties.Mag}"
                );
            }
        }

        return summary.ToArray();
    }
}