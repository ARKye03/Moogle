namespace MoogleEngine;

public class SearchResult(List<SearchItem> items, string suggestion = "")
{
    public List<SearchItem> Items { get; private set; } = items ?? throw new ArgumentNullException(nameof(items));
    public string Suggestion { get; private set; } = suggestion;
    public int Count { get { return Items.Count; } }

    public SearchResult() : this([])
    {
    }
}
