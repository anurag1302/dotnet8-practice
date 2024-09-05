namespace API.Project.Stuffs
{
    public class SingletonMessage : IMessage
    {
        public string GetMessage()
        {
            return "Singleton";
        }
    }
}
