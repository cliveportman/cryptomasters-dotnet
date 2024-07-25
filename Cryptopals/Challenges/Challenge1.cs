namespace Cryptopals.Challenges
{
    // Challenge 1: Convert a hex string to base64. The result should be "SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t"
    public class Challenge1
    {
        public static string Run()
        {
            var hexString  =
                "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
            return Converters.HexToBase64(hexString);
        }
    }
}