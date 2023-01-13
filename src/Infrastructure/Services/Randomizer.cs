using Application.Common.Interfaces;

namespace Infrastructure.Services;

public sealed class Randomizer : IRandomizer
{
    private readonly Random _random = Random.Shared;

    public TEnum EnumValue<TEnum>() where TEnum : struct, Enum
    {
        var values = Enum.GetValues<TEnum>();
        return ArrayValue(values);
    }

    private T ArrayValue<T>(IReadOnlyList<T> array)
    {
        var count = array.Count;
        return array[_random.Next(count)];
    }
}