namespace ShortUrlWorkshop.Services;

public interface IShortCodeGenerator
{
    string Generate(int length = 7);
}
