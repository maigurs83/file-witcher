using System;
using FileWitcher.Structures;
using FileWitcher.Interfaces;
using NUnit.Framework;

[TestFixture]
public class FileWitcherTests
{
    [Test]
    public void ParseASNFile_ShouldParseCorrectly()
    {
        // Arrange
        var filePath = "./path_to_test_file.txt";
        var expectedBoxes = new List<Box>
        {
            new() {
                SupplierIdentifier = "TRSP117",
                Identifier = "6874454I",
                Contents = new List<Box.Content>
                {
                    new Box.Content { PoNumber = "P000001661", Isbn = "9781465121550", Quantity = 12 },
                    new Box.Content { PoNumber = "P000001661", Isbn = "9925151267712", Quantity = 2 },
                    new Box.Content { PoNumber = "P000001661", Isbn = "9651216865465", Quantity = 1 },
                }
            }
        };

        var asnParser = new AsnParser();
        var result = asnParser.ParseAsnFile(filePath);

        Assert.AreEqual(expectedBoxes, result, new BoxComparer());
    }
}

public class BoxComparer : IEqualityComparer<Box>
{
    public bool Equals(Box x, Box y)
    {
        if (x == null && y == null) return true;
        if (x == null || y == null) return false;

        if (x.SupplierIdentifier != y.SupplierIdentifier) return false;
        if (x.Identifier != y.Identifier) return false;

        if (x.Contents.Count != y.Contents.Count) return false;

        for (int i = 0; i < x.Contents.Count; i++)
        {
            if (x.Contents[i].PoNumber != y.Contents[i].PoNumber) return false;
            if (x.Contents[i].Isbn != y.Contents[i].Isbn) return false;
            if (x.Contents[i].Quantity != y.Contents[i].Quantity) return false;
        }

        return true;
    }

    public int GetHashCode(Box obj)
    {
        return obj.GetHashCode();
    }
}
