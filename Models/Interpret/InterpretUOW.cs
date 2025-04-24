namespace medienVerwaltungDbSolution.Models.Interpret
{
    public class InterpretUOW
    {
        public SessionType Session { get; set; }
        public Interpret Interpret { get; set; } = new();
    }
}