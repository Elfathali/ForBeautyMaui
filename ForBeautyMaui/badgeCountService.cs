public static class BadgeCounterService
{
    private static readonly Dictionary<int, int> _counts = new();
    public static event EventHandler<(int index, int count)>? CountChanged;

    public static void SetCount(int index, int count)
    {
        _counts[index] = count;
        CountChanged?.Invoke(null, (index, count));
    }

    public static int GetCount(int index)
    {
        return _counts.TryGetValue(index, out var count) ? count : 0;
    }
}
