using System.Text;
using Cryptopals.Helpers;

namespace Cryptopals.Challenges
{
    // Challenge 6: Provided with a text file, find out which line has been encrypted with a single character XOR
    public class Challenge6
    {
        private const int MinKeySize = 2;
        private const int MaxKeySize = 40;
        private const int NumberOfHammingBlocks = 20;

        public static string Run()
        {
            // Trying out ReadAllLines to avoid the need to manually split the string on new lines
            try
            {
                // Utility created for handling file path
                var lines = File.ReadAllLines(Path.Combine(Utility.Assets, "challenge-6.txt"));
                // The lines are base64 encoded - convert them to bytes
                var bytes = lines.Select(Convert.FromBase64String).SelectMany(x => x).ToArray();

                // Loop through the key sizes 
                var keySizes = Enumerable.Range(MinKeySize, MaxKeySize - MinKeySize + 1);
                // For each key size, make some comparisons with consecutive blocks and output the average hamming distance for each key size
                var keySizeDistances = keySizes.Select(keySize =>
                {
                    var hammingDistances = new List<int>();
                    // Loop through the hamming blocks and get a hamming distance
                    for (var i = 0; i < NumberOfHammingBlocks; i++)
                    {
                        var block1 = bytes.Skip(keySize * i).Take(keySize).ToArray();
                        var block2 = bytes.Skip(keySize * (i + 1)).Take(keySize).ToArray();
                        hammingDistances.Add(Comparators.HammingDistance(block1, block2));
                    }
                    // Then get the average for the key size
                    var average = hammingDistances.Average();
                    return new
                    {
                        KeySize = keySize,
                        Distance = (float)average / keySize // Hamming distance normalized by key size
                    };
                }).OrderBy(x => x.Distance).ToList();
                
                // Print out the results, the more blocks we use the more accurate the results should be. Seems to be 29 is the best key size.
                // foreach (var keySizeDistance in keySizeDistances)
                // {
                //     Console.WriteLine(keySizeDistance.KeySize + ": " + keySizeDistance.Distance);
                // }
                
                // Get the smallest key size
                var keySize = keySizeDistances.First().KeySize;
                Console.WriteLine("The keysize producing the lowest average Hamming distance is " + keySize);
                
                // We now have the key size and can proceed with the rest of the challenge...
                // Which means decrypting the text using the key size
                
                // 5. Now that you probably know the KEYSIZE: break the ciphertext into blocks of KEYSIZE length.
                // Start by taking the text and chunking it up into blocks of length keySize
                var blocksOfKeySize = Converters.ChunkBytes(bytes, keySize);      
                // 6. Now transpose the blocks: make a block that is the first byte of every block, and a block that is the second byte of every block, and so on.
                // Transpose the blocks, e.g. put the first bytes of each block into an array, then the second byte of each block into another array, etc.
                // @todo Check this
                var transposedBlocks = Enumerable.Range(0, keySize)
                .Select(i => blocksOfKeySize.Select(block => block.ElementAtOrDefault(i)).ToArray());
                
                Console.WriteLine("The number of blocks is " + blocksOfKeySize.Count());
                // The number of transposed blocks should match the keysize - add a test for this?
                Console.WriteLine("The number of transposed blocks is " + transposedBlocks.Count());
                
                // 7. Solve each block as if it was single-character XOR. You already have code to do this.
                
                // 8. For each block, the single-byte XOR key that produces the best looking histogram is the repeating-key XOR key byte for that block. Put them together and you have the key.
                
                
                
                
                // // Split the bytes into blocks of keySize
                // var blocksOfKeySize = bytes.Select((b, i) => new { b, i })
                //     .GroupBy(x => x.i / keySize)
                //     .Select(g => g.Select(x => x.b).ToArray());
                // // Transpose the blocks, e.g. put the first bytes of each block into an array, then the second byte of each block into another array, etc.
                // var transposedBlocks = Enumerable.Range(0, keySize)
                //     .Select(i => blocksOfKeySize.Select(block => block.ElementAtOrDefault(i)).ToArray());
                // // Solve each block as if it was a single character XOR
                // var key = transposedBlocks.Select(block => Decrypt.SingleCharacterXor(Encoding.UTF8.GetString(block))).Select(result => result).ToArray();
                // //Console.WriteLine(key);
                
            }
            // Catching exceptions and providing a more helpful message (more a learning exercise than anything else)
            catch (Exception ex)
            {
                return ex switch
                {
                    FileNotFoundException => "The file was not found: " + ex.Message,
                    IOException => "An I/O error occurred: " + ex.Message,
                    _ => "Error reading file: " + ex.Message
                };
            }

            return "";
        }
    }
}