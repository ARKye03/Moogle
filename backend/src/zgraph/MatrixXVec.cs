namespace MoogleEngine;

//Heavy matrix of the documents
//Document object that represent txt files
public class Document(string id, Dictionary<string, double> tfidf)
{
    public string id = id;
    public Dictionary<string, double> tfidf = tfidf;
}
public class DocumentMatrix
{
    public DocumentMatrix(List<Document> documents)
    {
        this.documents = documents;
        matrix = [];
        foreach (Document document in documents)
        {
            matrix.Add(document.id, document.tfidf);
        }
    }
    public Dictionary<string, Dictionary<string, double>> matrix;
    public List<Document> documents;
    public void PrintMatrix()
    {
        foreach (KeyValuePair<string, Dictionary<string, double>> document in matrix)
        {
            Console.WriteLine(document.Key);
            foreach (KeyValuePair<string, double> term in document.Value)
            {
                Console.WriteLine(term.Key + " " + term.Value);
            }
        }
    }
    //Use matrix addition, to sum up two Document Matrix
    public static DocumentMatrix AddMatrix(DocumentMatrix matrix1, DocumentMatrix matrix2)
    {
        List<Document> documents = [];
        foreach (KeyValuePair<string, Dictionary<string, double>> document in matrix1.matrix)
        {
            documents.Add(new Document(document.Key, document.Value));
        }
        foreach (KeyValuePair<string, Dictionary<string, double>> document in matrix2.matrix)
        {
            documents.Add(new Document(document.Key, document.Value));
        }
        return new DocumentMatrix(documents);
    }
    //Use matrix multiplication, to multiply two Document Matrix
    public static DocumentMatrix MultiplyMatrix(DocumentMatrix matrix1, DocumentMatrix matrix2)
    {
        List<Document> documents = [];
        foreach (KeyValuePair<string, Dictionary<string, double>> document in matrix1.matrix)
        {
            Dictionary<string, double> tfidf = [];
            foreach (KeyValuePair<string, double> term in document.Value)
            {
                tfidf.Add(term.Key, term.Value * matrix2.matrix[term.Key][document.Key]);
            }
            documents.Add(new Document(document.Key, tfidf));
        }
        return new DocumentMatrix(documents);
    }
}


