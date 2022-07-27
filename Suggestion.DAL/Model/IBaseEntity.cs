namespace Suggestion.BL.Model
{
    public interface IBaseEntity
    {
        string? CreatedBy { get; set; }
        DateTime DateCreated { get; set; }
        DateTime DateModified { get; set; }

        string? ModifiedBy { get; set; }
    }
}