using Microsoft.AspNetCore.Components;

namespace INSS.Forms.Components.Abstract
{
    public abstract class RequiredAttributeBase : ComponentBase
    {
        [Parameter]
        [EditorRequired] // This makes the parameter mandatory
        public string Name { get; set; } = string.Empty;

        private string? _elementId;

        public string GenerateElementId()
        {
            return _elementId ??= $"inss-form-element-id-{Name}-{Guid.NewGuid():N}";
        }

        public string GenerateElementName()
        {
            return $"inss-form-element-name-{Name}";
        }
    }
}
