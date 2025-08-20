using INSS.Forms.Domain.Models.Composite;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace INSS.Forms.Components.Abstract
{
    public abstract class QueryStringBase : ComponentBase
    {
        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;

        protected FormMetadata MetaDataFromLauncher { get; set; } = new FormMetadata();

        protected override void OnInitialized()
        {
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
            IDictionary<string, string> QueryStringValues = QueryHelpers.ParseQuery(uri.Query)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            AssignMetadata(QueryStringValues);
        }

        private void AssignMetadata(IDictionary<string, string> query)
        {
            if (query.TryGetValue("Username", out var username))
                MetaDataFromLauncher.Username = username;

            if (query.TryGetValue("Form", out var formId) && Guid.TryParse(formId, out var fid))
                MetaDataFromLauncher.FormId = fid;

            if (query.TryGetValue("ReturnUrl", out var returnUrl))
                MetaDataFromLauncher.ReturnUrl = returnUrl;

            MetaDataFromLauncher.FormInstanceId = Guid.NewGuid();
        }
    }
}