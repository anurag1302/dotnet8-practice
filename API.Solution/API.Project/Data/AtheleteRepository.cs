namespace API.Project.Data
{
    public class AtheleteRepository
    {
        public static List<Athlete> GetAthletes()
        {
            return
            [
                new Athlete
                {
                    Id=101,
                    Name="John",
                    Sports="Basketball"
                },
                new Athlete
                {
                    Id=102,
                    Name="Doe",
                    Sports="Baseball"
                },
                new Athlete
                {
                    Id=103,
                    Name="Mary",
                    Sports="Football"
                },
                new Athlete
                {
                    Id=104,
                    Name="Sheela",
                    Sports="Tennis"
                }
            ];
        }
    }
}
