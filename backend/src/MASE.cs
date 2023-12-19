//MASE -Moogle Advance Search Engine
//Here I process the searcher \^-^/
//                             |
//                            /\
using System.Text.RegularExpressions;

namespace MoogleEngine
{
    public class Searcher
    {
        //Splitted Query
        public string[] UsrInp;
        //Words values for Splitted Query
        public double[] WVal;
        //Evidently
        public Corpus corpus;
        //Snippets
        public string[] Snippets;
        //repetitions of a word
        public Dictionary<string, int> Frqhzy = [];
        //Max word frqhzy
        public int MaxWordAppereance = 0;
        //Module of the WVal vector -> Value |WVal|
        public double Module = 0;
        //Lists that vary depending on the optional operators placed in the query
        public List<string> LetMeIn;
        public List<string> LetMeOut;
        public List<(string, string)> Closeness;

        //Thread that saves the suggestion
        public string? _suggestion;

        public Searcher(string Query, Corpus corpus)
        {
            UsrInp = ProcessQuery(Query);
            LetMeIn = [];
            LetMeOut = [];
            Closeness = [];
            GetInfo(UsrInp);
            this.corpus = corpus;
            GSuggest(Frqhzy, corpus);
            Snippets = new string[corpus.Docs.Count];
            WVal = new double[UsrInp.Length];
            Save_W_Value();
            Mod();
            FillSuggest();
        }
        //Split query by the pieces
        static string[] ProcessQuery(string UI)
        {
            string[] PUI = UI.Split(new char[] { ' ', ',', '.', ';', '?', '¿', '¡', ':', '"' }, StringSplitOptions.RemoveEmptyEntries);
            return PUI;
        }
        //Splitted Query weight values TF *IDF
        public void Save_W_Value()
        {
            int count = 0;
            foreach (var par in Frqhzy)
            {
                WVal[count] = corpus.GeneralFiler.TryGetValue(par.Key, out double value)
                ? Frqhzy[par.Key] / (double)MaxWordAppereance * value
                : 0;
                count++;
            }
        }
        //Vector of weights of the searcher/query
        public void Mod()
        {
            for (int i = 0; i < WVal.Length; i++)
            {
                Module += Math.Pow(WVal[i], 2);
            }
            Module = Math.Sqrt(Module);
        }
        //I take over the data from the searcher/query
        public void GetInfo(string[] UsrInp)
        {
            int count = 0;
            for (int i = 0; i < UsrInp.Length; i++)
            {
                int count1 = 0;

                if (UsrInp[i][0] == '!')
                {
                    UsrInp[i] = UsrInp[i][1..];
                    LetMeOut.Add(UsrInp[i]);
                }
                if (UsrInp[i][0] == '^')
                {
                    UsrInp[i] = UsrInp[i][1..];
                    LetMeIn.Add(UsrInp[i]);
                }
                while (UsrInp[i][0] == '*')
                {
                    UsrInp[i] = UsrInp[i][1..];
                    count1++;

                }
                if (UsrInp[i] == "~")
                {
                    Closeness.Add((UsrInp[i - 1], UsrInp[i + 1]));
                    continue;
                }
                //Up to this point, what the method does is add the words with operators to their corresponding lists.
                //-------------------------------------------------------------------------------------------------------\\
                //Each word in the searcher goes to the dictionary Frqhzy(frqhzy) with its number of repetitions

                Frqhzy.TryAdd(UsrInp[i], 0);
                Frqhzy[UsrInp[i]] += count1 + 1;
                if (MaxWordAppereance < Frqhzy[UsrInp[i]])
                {
                    MaxWordAppereance = Frqhzy[UsrInp[i]];
                }
            }
            UsrInp = new string[Frqhzy.Count];
            foreach (var par in Frqhzy)
            {
                UsrInp[count] = par.Key;
                count++;
            }
        }
        public void FillSnippet((string, double)[] tupla)
        {
            for (int i = tupla.Length - 1; i > -1 && tupla[i].Item2 != 0; i--)
            {
                string Relevant = ""; //this variable will contain the most important term present in document i, which will be present in the snippet
                for (int j = 0; j < Frqhzy.Count; j++)
                {
                    if (!corpus.Docs[tupla[i].Item1].pesos.ContainsKey(Frqhzy.ElementAt(j).Key)) continue;
                    Relevant = Frqhzy.ElementAt(j).Key;
                }
                for (int j = 0; j < Frqhzy.Count; j++)
                {
                    if (!corpus.Docs[tupla[i].Item1].pesos.ContainsKey(Frqhzy.ElementAt(j).Key)) continue;
                    if (corpus.Docs[tupla[i].Item1].pesos[Frqhzy.ElementAt(j).Key] > corpus.Docs[tupla[i].Item1].pesos[Relevant])
                    {
                        Relevant = Frqhzy.ElementAt(j).Key;
                    }
                }
                string txt = File.ReadAllText(tupla[i].Item1).ToLower();
                string[] palabras = txt.Split(new char[] { ' ' });
                if (Relevant == "") return;
                //Once the most important term has been determined, a snippet is created where it is found
                Snippets[i] = RetSnippet(txt, Relevant);
            }
        }
        public string RetSnippet(string txt, string Relevant)
        {
            while (true)
            {
                //This condition is to select a snippet that contains the most important word
                if ((!Char.IsPunctuation(txt[txt.IndexOf(Relevant) - 1]) && !(txt[txt.IndexOf(Relevant) - 1] == ' ')) || (!Char.IsPunctuation(txt[txt.IndexOf(Relevant) + Relevant.Length]) && !(txt[txt.IndexOf(Relevant) + Relevant.Length] == ' ')))
                {
                    txt = txt[(txt.IndexOf(Relevant) + Relevant.Length)..];
                    continue;
                }
                if (txt.IndexOf(Relevant) < 50)
                {
                    return txt[..100];
                }
                if (txt.IndexOf(Relevant) > txt.Length - 50)
                {
                    return txt[^100..];
                }
                return txt.Substring(txt.IndexOf(Relevant) - 50, 100);
            }
        }
        public void GSuggest(Dictionary<string, int> Frequency, Corpus corpus)
        {
            for (int i = 0; i < UsrInp.Length; i++)
            {
                if (Suggestion(UsrInp[i], corpus) == "") continue;

                else
                {
                    if (Frequency.TryGetValue(UsrInp[i], out int value))
                    {
                        Frequency.Add(Suggestion(UsrInp[i], corpus), value);  /*------*/
                        Frequency.Remove(UsrInp[i]);
                    }
                    UsrInp[i] = Suggestion(UsrInp[i], corpus);
                }
            }
        }
        //To throw the house out the window, there is no more, word by word to suggest the best.
        private static string Suggestion(string word, Corpus corpus)
        {
            string suggestion = "";

            if (!corpus.GeneralFiler.ContainsKey(word))
            {
                for (int i = 1; i < word.Length / 3 + 1; i++)
                {
                    foreach (var pair in corpus.GeneralFiler)
                    {
                        if (LevenstheinDistance(word, pair.Key) == i) { suggestion = Compare(suggestion, pair.Key, word, corpus); }
                    }
                    if (suggestion != "") return suggestion;
                }
            }
            return suggestion;
        }
        //Between two words returns the most relevant one
        private static string Compare(string suggestion, string newword, string word, Corpus corpus)
        {
            if (suggestion == "") suggestion = newword;

            if (Score.TotalWeight(suggestion, corpus) < Score.TotalWeight(newword, corpus)) suggestion = newword;

            return suggestion;
        }
        public void FillSuggest()
        {
            for (int i = 0; i < UsrInp.Length; i++)
            {
                _suggestion = _suggestion + UsrInp[i] + " ";
            }
        }
        //Method that calculates the levenshtein distance between two words
        static int LevenstheinDistance(string s1, string s2)
        {
            int n = s1.Length;
            int m = s2.Length;
            int[,] d = new int[n + 1, m + 1];

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            for (int i = 0; i <= n; d[i, 0] = i++) { }
            for (int j = 0; j <= m; d[0, j] = j++) { }

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }
    }
    //This class will store the scores of each document, to later be displayed
    public class Score
    {
        //Searcher
        public Searcher searcher;
        //Corpus
        public Corpus Corpus;
        //Array size == number of documents and their scores
        public (string, double)[] tupla;

        //Granting scores to the respective documents
        public Score(Searcher searcher, Corpus corpus)
        {
            this.searcher = searcher;
            Corpus = corpus;
            tupla = new (string, double)[corpus.Docs.Count];
            FillScores();
        }
        //Score\Value resulting from the product of the searcher vector and the documents vector
        //Tuples from highest to lowest
        public void FillScores()
        {
            for (int i = 0; i < Corpus.Docs.Count; i++)
            {
                tupla![i].Item2 = VecMultiply(i);
                tupla[i].Item1 = Corpus.Docs.ElementAt(i).Key;
                ModScore(i);
            }

            BubbleSort(tupla);
            if (tupla[^1].Item2 != 0)
            {
                searcher.FillSnippet(tupla);
            }

        }
        public double VecMultiply(int i)
        {
            if (!ValidateDoc(i) || searcher.Module == 0) return 0;
            double suma = 0;
            for (int j = 0; j < searcher.Frqhzy.Count; j++)
            {
                if (!Corpus.Docs.ElementAt(i).Value.pesos.ContainsKey(searcher.Frqhzy.ElementAt(j).Key)) continue;
                suma += Corpus.Docs.ElementAt(i).Value.pesos[searcher.Frqhzy.ElementAt(j).Key] * searcher.WVal[j];
            }
            suma /= searcher.Module * Corpus.Docs.ElementAt(i).Value.Module;
            return suma;
        }
        //Method that organizes elements from smallest to largest
        public static void BubbleSort((string, double)[] tupla)
        {
            for (int i = 0; i < tupla.Length; i++)
                for (int j = 0; j < tupla.Length - 1; j++)
                    if (tupla[j].Item2 > tupla[j + 1].Item2)
                        Swap(tupla, j, j + 1);
        }
        //Exchange of 2 elements of an array, classic Swap
        private static void Swap((string, double)[] tupla, int a, int b)
        {
            (tupla[b], tupla[a]) = (tupla[a], tupla[b]);
        }

        //Returns the sum of the weights of a word in each of the documents
        public static double TotalWeight(string word, Corpus corpus)
        {
            double totalweight = 0;

            for (int i = 0; i < corpus.Docs.Count; i++) { totalweight += corpus.Docs.ElementAt(i).Value.pesos.TryGetValue(word, out double value) ? value : 0; }

            return totalweight;
        }
        public bool ValidateDoc(int i)
        {
            for (int j = 0; j < searcher.LetMeOut.Count; j++)
            {
                if (Corpus.Docs.ElementAt(i).Value.pesos.ContainsKey(searcher.LetMeOut[j])) { return false; }
            }
            for (int j = 0; j < searcher.LetMeIn.Count; j++)
            {
                if (!Corpus.Docs.ElementAt(i).Value.pesos.ContainsKey(searcher.LetMeIn[j])) { return false; }
            }
            return true;
        }

        //If there is a pair of words in the Closeness list, modify the score of the documents depending on the closeness between the terms that belong to said list
        public void ModScore(int i)
        {
            if (searcher.Closeness.Count == 0) return;
            for (int j = 0; j < searcher.Closeness.Count; j++)
            {
                if (!Corpus.Docs.ElementAt(i).Value.pesos.ContainsKey(searcher.Closeness[j].Item1) || !Corpus.Docs.ElementAt(i).Value.pesos.ContainsKey(searcher.Closeness[j].Item2))
                {
                    tupla[i].Item2 = tupla[i].Item2 / Corpus.Docs.ElementAt(i).Value.pesos.Count;
                    return;
                }
            }
            //If any of the 2 words that have a Closeness operator between them is not found in a certain document, the score of this document will be divided by the length of this document

            //If both appear, the score will be divided by the smallest distance between them
            List<int> a = [];
            List<int> b = [];
            for (int j = 0; j < searcher.Closeness.Count; j++)
            {
                a = Corpus.Docs.ElementAt(i).Value.Vocabulary[searcher.Closeness[j].Item1];
                b = Corpus.Docs.ElementAt(i).Value.Vocabulary[searcher.Closeness[j].Item2];
                int menordist = LowestDistance(a, b);
                tupla[i].Item2 = tupla[i].Item2 / menordist;
            }
        }
        private static int LowestDistance(List<int> a, List<int> b)
        {
            int i = 0; int j = 0;
            int min = Math.Abs(a[0] - b[0]);
            while (i + j < a.Count + b.Count - 2)
            {
                if (j == b.Count - 1 || (i < a.Count - 1 && a[i] < b[j])) i++;
                else j++;
                min = Math.Min(min, Math.Abs(a[i] - b[j]));
            }
            return min;
        }
    }
}