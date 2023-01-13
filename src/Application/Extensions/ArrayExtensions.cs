namespace Application.Extensions;

public static class ArrayExtensions
{
    public static T[][] ToJagged<T>(this T[,] array)
    {
        return array.Cast<T>()
            .Select((x, i) => new
                { x, index = i / array.GetLength(1) })
            .GroupBy(x => x.index)
            .Select(x => x.Select(s => s.x).ToArray())
            .ToArray();
    }
}