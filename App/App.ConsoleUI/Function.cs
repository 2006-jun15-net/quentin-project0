namespace App.ConsoleUI
{
    public class Function
    {
        public string Verb { get; set; }
        public string Domain { get; set; }
        public string Info { get; set; }
        public string[] QueryFields { get; set; }
        public bool MultiQuery = false;
    }
}