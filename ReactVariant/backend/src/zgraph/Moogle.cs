using System.Diagnostics;

namespace MoogleEngine;

public static class Moogle
{
    private static Corpus? corpus;
    private static Searcher? searcher;
    private static Score? score;
    private static Stopwatch timer = new();
    private static int item_q;

    public static Searcher? Searcher { get => searcher; set => searcher = value; }
    public static Corpus? Corpus { get => corpus; set => corpus = value; }
    public static Score? Score { get => score; set => score = value; }
    public static Stopwatch Timer { get => timer; set => timer = value; }
    public static int Item_q { get => item_q; set => item_q = value; }


    public static void LetsGetStarted(string path) { Corpus = new Corpus(path); } //Start
    public static SearchResult Query(string query)
    {
        // Modifiqué este método para responder a la búsqueda
        Searcher = new Searcher(query, Corpus!);
        Score = new Score(Searcher, Corpus!);

        List<SearchItem> items = [];

        for (int i = Score.tupla.Length - 1; i > -1 && Score.tupla[i].Item2 != 0; i--)
        {
            items.Add(new SearchItem(Score.tupla[i].Item1[12..], Searcher.Snippets[i], Score.tupla[i].Item2));
        }

        Item_q = items.Count;
        Timer.Stop();
        return new SearchResult(items, Searcher._suggestion!);
    }
}
