namespace Cryptopals.Challenges
{
    // Challenge 5: Provided with a text file, find out which line has been encrypted with a single character XOR
    public class Challenge5
    {
        private const string Stanza = "Burning 'em, if you ain't quick and nimble\nI go crazy when I hear a cymbal";
        private const string Key = "ICE";

        public static string Run()
        {
            var result = Encrypt.RepeatingKey(Stanza, Key);

            return result;
        }
    }
}