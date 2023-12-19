namespace MoogleEngine;

public class SearchResult(List<SearchItem> items, string suggestion = "")
{
    public List<SearchItem> items = items ?? throw new ArgumentNullException("items");

    public SearchResult() : this([])
    {

    }
    public string Suggestion { get; private set; } = suggestion;

    public IEnumerable<SearchItem> Items()
    {
        return items;
    }

    public int Count { get { return items.Count; } }
}
