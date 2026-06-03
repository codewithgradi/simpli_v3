public class QueryParemeters
{
  private int _pageSize = 10;
  private int _maxSize = 20;
  public int Page { get; set; }
  public int Size
  {
    get
    {
      return _pageSize;
    }
    set
    {
      _pageSize = Math.Min(value, _maxSize);
    }
  }
}