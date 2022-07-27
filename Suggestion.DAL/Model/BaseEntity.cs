namespace Suggestion.BL.Model
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        public string? ModifiedBy { get; set; }

    }


}
