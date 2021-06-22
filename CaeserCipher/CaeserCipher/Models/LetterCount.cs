
namespace CaeserCipher.Models
{
    public class LetterCount
    {
        public char Letter;
        public int Count;

        public LetterCount(char letter, int count)
        {
            this.Letter = letter;
            this.Count = count;
        }
    }
}
