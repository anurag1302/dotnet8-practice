namespace API.Project.Stuffs
{
    public class TransientMessage : IMessage
    {
        public string GetMessage()
        {
            return "Transient";
        }
    }
}
