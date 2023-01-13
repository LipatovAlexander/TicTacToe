namespace Application.Common.Interfaces;

public interface IRandomizer
{
    TEnum EnumValue<TEnum>() where TEnum : struct, Enum;
}