namespace firstProgram.Models
{
    public class FeesDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gst { get; set; }    //sensitive data
        public int Amount { get; set; }
    }
}
