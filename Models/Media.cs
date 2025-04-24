namespace medienVerwaltungDbSolution.Models
{
    abstract public class Media
    {
        public string? InterpretFullName { get; set; }
        public int InterpretID { get; set; }
        public string? Location { get; set; }
        public string? Title { get; set; }
    }
}