using System.ComponentModel.DataAnnotations;
using System.Collections;
namespace Core.Helpers.CustomValidateAttributes;

public class MinLengthRequiredAttribute: ValidationAttribute
{
    private readonly int _minElements;
    
    public MinLengthRequiredAttribute(int minElements)
    {
        _minElements = minElements;
    }

    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }
        var list = value as IList;
        return list?.Count >= _minElements;
    }
    
    public override string FormatErrorMessage(string name)
    {
        return string.Format(this.ErrorMessageString, name);
    }
}