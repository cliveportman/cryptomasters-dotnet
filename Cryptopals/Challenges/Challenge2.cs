namespace Cryptopals.Challenges
{
    // Challenge 2: Given two hex encoded strings, XOR them together and return the result "746865206b696420646f6e277420706c6179"
    // @todo: Turn this into a test.
    public class Challenge2
    {
        public static string Run()
        {
            // Take the provided strings and convert them to byte arrays
            var hex1 = "1c0111001f010100061a024b53535009181c";
            var bytes1 = Converters.HexToBytes(hex1);
            var hex2 = "686974207468652062756c6c277320657965";
            var bytes2 = Converters.HexToBytes(hex2);
            
            // XOR the two byte arrays together and print the result
            var resultBytes = Comparators.Xor(bytes1, bytes2);
            return Converters.BytesToHex(resultBytes).ToLower();
        }
    }
}