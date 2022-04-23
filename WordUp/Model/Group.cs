namespace WordUp.Model
{
    public class Group : IHierarchy
    {
        public IHierarchy Parent { get; set; }
        public string Name { get; set; }
    }
}
