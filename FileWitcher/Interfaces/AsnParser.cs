using System;
using System.Collections.Generic;
using FileWitcher.Structures;

namespace FileWitcher.Interfaces
{
    public class AsnParser : IAsnParser
    {
        public int ParseAsnFile(string filePath)
        {
            var boxesCount = 0;

            using (FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
            using (BufferedStream bs = new(fs))
            using (StreamReader sr = new(bs))
            {
                Box? box = null;
                string? line;

                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length > 0)
                    {
                        var keyword = parts[0];

                        switch (keyword)
                        {
                            case "HDR":
                                if (box != null)
                                {
                                    // Store current Box instead of just logging
                                    Console.WriteLine($"LogLine: {box?.Identifier} with {box?.Contents?.Count ?? 0} items in it.");
                                    boxesCount++;
                                }

                                box = new Box
                                {
                                    SupplierIdentifier = parts[1],
                                    Identifier = parts[2],
                                    Contents = []
                                };
                                break;
                            case "LINE":
                                if (box != null)
                                {
                                    var content = new Box.Content
                                    {
                                        PoNumber = parts[1],
                                        Isbn = parts[2],
                                        Quantity = int.Parse(parts[3])
                                    };
                                    box.Contents?.Add(content);
                                }
                                break;
                            default:
                                Console.WriteLine($"WarningLine: Potentially corupted file at {filePath}.");
                                break;
                        }
                    }
                }
            }

            return boxesCount;
        }
    }
}