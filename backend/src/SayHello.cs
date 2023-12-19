using MoogleEngine;
namespace GraphEngine;

public class Hello
{
  /// <summary>
  /// Represents the result of a search operation.
  /// </summary>
  public static SearchResult Main(string sourceCode)
  {
    // Source code does not end with ';'
    if (sourceCode is null)
    {
      return null!;
    }
    return RunQuery(sourceCode);

  }
  private static SearchResult RunQuery(string sourceCode) => Moogle.Query(sourceCode);
}
