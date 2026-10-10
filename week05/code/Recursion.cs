using System.Collections;

public static class Recursion
{
    // Problem 1 - Recursive Squares Sum
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }

    // Problem 2 - Permutations Choose
    public static void PermutationsChoose(
        List<string> results,
        string letters,
        int size,
        string word = "")
    {
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        foreach (char letter in letters)
        {
            string character = letter.ToString();

            if (!word.Contains(character))
            {
                PermutationsChoose(
                    results,
                    letters,
                    size,
                    word + character);
            }
        }
    }

    // Problem 3 - Climbing Stairs
    public static decimal CountWaysToClimb(
        int s,
        Dictionary<int, decimal>? remember = null)
    {
        if (s == 0)
            return 0;

        if (s == 1)
            return 1;

        if (s == 2)
            return 2;

        if (s == 3)
            return 4;

        remember ??= new Dictionary<int, decimal>();

        if (remember.TryGetValue(s, out decimal saved))
            return saved;

        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        remember[s] = ways;

        return ways;
    }

    // Problem 4 - Wildcard Binary Patterns
    public static void WildcardBinary(
        string pattern,
        List<string> results)
    {
        int index = pattern.IndexOf('*');

        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        string withZero =
            pattern[..index] + "0" + pattern[(index + 1)..];

        string withOne =
            pattern[..index] + "1" + pattern[(index + 1)..];

        WildcardBinary(withZero, results);
        WildcardBinary(withOne, results);
    }

    // Problem 5 - Maze
    public static void SolveMaze(
        List<string> results,
        Maze maze,
        int x = 0,
        int y = 0,
        List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        if (!maze.IsValidMove(currPath, x, y))
        {
            return;
        }

        currPath.Add((x, y));

        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        SolveMaze(results, maze, x + 1, y, currPath);
        SolveMaze(results, maze, x - 1, y, currPath);
        SolveMaze(results, maze, x, y + 1, currPath);
        SolveMaze(results, maze, x, y - 1, currPath);

        currPath.RemoveAt(currPath.Count - 1);
    }
}