namespace INSS.Forms.BCL.Services
{
    public interface IPropertyValidator
    {
        IList<System.ComponentModel.DataAnnotations.ValidationResult> ValidateProperties<T>(T model, IEnumerable<string> propertyNames);
        IList<System.ComponentModel.DataAnnotations.ValidationResult> ValidateAllProperties<T>(T model);
    }
}
