namespace FileWitcher.Structures
{
    public class Box
    {
        public required string SupplierIdentifier { get; set; }
        public required string Identifier { get; set; }
        public IList<Content>? Contents { get; set; }

        public class Content
        {
            public required string PoNumber { get; set; }
            public required string Isbn { get; set; }
            public int Quantity { get; set; }
        }
    }
}