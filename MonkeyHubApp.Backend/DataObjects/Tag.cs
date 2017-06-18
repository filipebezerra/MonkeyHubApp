namespace MonkeyHubApp.Backend.DataObjects
{
    public class Tag : BaseDataObject
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
    }
}