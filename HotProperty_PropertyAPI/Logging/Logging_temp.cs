namespace HotProperty_PropertyAPI.Logging
{
    public class Logging_temp : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.WriteLine("ERROR - " + message);
            }
            else
            {
                Console.WriteLine(message);
            }    
        }
    }
}
