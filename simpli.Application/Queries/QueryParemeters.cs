public class QueryParameters
{
  private int _size = 10;
  const int _maxSize = 20;
  public int Page { get; set; }
  public int Size
  {
    get
    {
      return _size;
    }
    set
    {
      _size = Math.Min(value, _maxSize);
    }
  }
  public string SortOrder { get; set; } = "asc";
  public string SortBy { get; set; } = string.Empty;
}