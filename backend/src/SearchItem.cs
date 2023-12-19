namespace MoogleEngine;

public class SearchItem(string title, string snippet, double score)
{

    public string Title { get; private set; } = title;

    public string Snippet { get; private set; } = snippet;

    public double Score { get; private set; } = score;
}
