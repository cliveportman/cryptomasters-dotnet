namespace Cryptopals.Challenges
{
    // Challenge 3: Provided with a hex encoded string that has been XOR'd against a single character, find the key and decrypt the message
    public class Challenge3
    {
        private const string Encoded = "1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736";
        public static string Run()
        {
            var result = Decrypt.SingleCharacterXor(Encoded);
            return result.Character + ": " + result.Score + " " + result.DecryptedText;
            
        }
    }
}