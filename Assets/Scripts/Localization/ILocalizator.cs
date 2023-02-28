namespace Clicker.Localization
{
    public interface ILocalizator<out T>
    {
        T GetTranslation(string key);
    }
}