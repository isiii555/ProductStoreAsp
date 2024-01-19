namespace ProductStoreAsp.Models.Entities
{
    public class BaseEntity 
    {
        public int Id { get; set; }

        public DateTime CreationTime {  get; set; }
        public DateTime ModifiedTime {  get; set; }
    }
}
