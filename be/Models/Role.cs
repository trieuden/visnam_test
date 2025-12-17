namespace be.Models
{
    public class Role
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string name { get; set; } = string.Empty;
        public ICollection<User>? users { get; set; }
    }
}