using Cryptopals.Helpers;

namespace Cryptopals.Challenges
{
    /* Challenge 8: Provided with a Hex-encoded text file containing multiple cipher texts,
     * identify which one has been encrypted with ECB.
     * The issue with ECB is it's stateless and deterministic,
     * so the same 16-byte plaintext will always provide the same 16-byte cipher text.
     *
     * The plan:
     * AES-128 breaks text into 16-byte blocks before encrypting. And because it's deterministic, if any
     * two blocks are the same the resulting cipher text will be identical.
     * So we're looking for 16-byte duplicates. Just loop through every cipher text, break it into 16-byte chunks
     * and search those chunks for duplicates. The more duplicates there are, the more likely that cipher text
     * is encrypted with AES-128 in ECB mode.
     *
     * Any problems:
     * I didn't realise arrays were reference types in C#. So the comparisons were against memory addresses rather than values.
     */
    public abstract class Challenge8
    {
        public static string Run()
        {
            // Grab the texts from the source file and convert from hex to bytes
            var lines = File.ReadAllLines(Path.Combine(Utility.Assets, "challenge-8.txt"));
            var linesAsBytes = lines.Select(Converters.HexToBytes).ToArray();
            
            foreach (var lineBytes in linesAsBytes)
            {
                var chunks = Converters.ChunkBytes(lineBytes, 16).ToArray();
                
                // Group the chunks to find duplicates. Convert the byte arrays to strings and compare them because
                // arrays are reference types in C# so you'll be comparing memory addresses, not the actual values.
                var groups = chunks.GroupBy(BitConverter.ToString).Where(g => g.Count() > 1).ToArray();
                if (groups.Length <= 0) continue;
                Console.WriteLine($"ECB detected in line {Array.IndexOf(linesAsBytes, lineBytes)}.");
                Console.WriteLine("Line:");
                Console.WriteLine(BitConverter.ToString(lineBytes));
                Console.WriteLine("Duplicate chunk:");
                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.Key} - Count: {group.Count()}");
                }
                return "Is ECB";
            }

            return "Not ECB";
        }
    }
}