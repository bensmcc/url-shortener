using System.Linq;

namespace UrlShortener.Random
{
    public class Random : IRandom
    {
        private const string _validCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";
        private static System.Random _randomGenerator;

        static Random()
        {
            _randomGenerator = new System.Random();
        }

        public string Generate(int length)
        {
            return new string(Enumerable.Repeat(_validCharacters, length).Select(s => s[_randomGenerator.Next(s.Length)]).ToArray());
        }
    }
}
