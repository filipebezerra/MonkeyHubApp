namespace MonkeyHubApp.Backend.DataObjects
{
    public class Content : BaseDataObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Tag Tag { get; set; }
        public string Banner { get; set; }
        public string Url { get; set; }
    }
}