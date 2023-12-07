using System;
using FileWitcher.Structures;
using FileWitcher.Interfaces;
using NUnit.Framework;

namespace FileWitcher.Tests;

[TestFixture]
public class AsnParserTests
{
    [TestCase(7)]
    public void ParseAsnFile_ShouldParseCorrectly(int expectedResult)
    {
        var filePath = "../../../../queue/data.txt";

        var asnParser = new AsnParser();
        var result = asnParser.ParseAsnFile(filePath);

        Assert.NotNull(result,"Some file was read");

        Assert.AreEqual(result, expectedResult,"Some file was read");
    }
}