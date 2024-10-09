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
                
                // Logging the results, the more blocks we use the more accurate the results should be. Seems to be 29 is the best key size.
                // foreach (var keySizeDistance in keySizeDistances)
                // {
                //     Console.WriteLine(keySizeDistance.KeySize + ": " + keySizeDistance.Distance);
                // }
                
                // Get the key size - it'll be the first one in the list
                var keySize = keySizeDistances.First().KeySize;
                Console.WriteLine("The keysize producing the lowest average Hamming distance is " + keySize);
                
                // We now have the key size and can proceed with the rest of the challenge...
                // Which means decrypting the text using the key size
                // Start by taking the text and chunking it up into blocks of length keySize
                var blocksOfKeySize = Converters.ChunkBytes(bytes, keySize);
                // Transpose the blocks, e.g. put the first bytes of each block into an array, then the second byte of each block into another array, etc.
                var transposedBlocks = Converters.TransposeArraysOfBytes(blocksOfKeySize);
                
                // Just some logging - the number of transposed blocks should match the key size...
                // Console.WriteLine("The number of blocks is " + blocksOfKeySize.Count());
                // Console.WriteLine("The number of transposed blocks is " + transposedBlocks.Count());
                
                // Convert to hex-encoded strings and XOR to get the key characters
                var keyCharacters = transposedBlocks
                    .Select(block => 
                    {
                        var hexBlock = Converters.BytesToHex(block);
                        // I had to update the XOR character list to DEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+:,.-_()!$ @;"
                        var decryptionResult = Decrypt.SingleCharacterXor(hexBlock);
                        //Console.WriteLine($"Block: {hexBlock}, Character: {decryptionResult.Character}, Score: {decryptionResult.Score}");
                        return decryptionResult.Character;
                    })
                    .ToArray();
                // Then put them together and we have the key.
                var key = new string(keyCharacters);
                Console.WriteLine("The key is: " + key);
                
                // Decrypt the encrypted text with the key.
                var hexKey = Converters.BytesToHex(Encoding.ASCII.GetBytes(key));
                var decrypted = Decrypt.DecryptRepeatingKey(Converters.BytesToHex(bytes), hexKey);
                return decrypted;

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
            
        }
    }
}