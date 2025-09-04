using INSS.Forms.Components.Abstract;
using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace INSS.Forms.Components.Services
{
    /// <inheritdoc />
    public class FormMetadataService : IFormMetadataService
    {
        private readonly NavigationManager _navigationManager;

        /// <summary>
        /// Provides metadata-related services for forms, including navigation and related operations.
        /// </summary>
        /// <param name="navigationManager">An instance of <see cref="NavigationManager"/> used to manage navigation within the application.</param>
        public FormMetadataService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        /// <inheritdoc />
        public FormMetadata CreateFromQueryString()
        {
            var uri = _navigationManager.ToAbsoluteUri(_navigationManager.Uri);
            var query = QueryHelpers.ParseQuery(uri.Query);

            var metadata = new FormMetadata
            {
                Username = query.TryGetValue("Username", out var username) && !StringValues.IsNullOrEmpty(username) ? username.ToString() : string.Empty,
                FormId = query.TryGetValue("Form", out var formId) && Guid.TryParse(formId, out var fid) ? fid : Guid.Empty,
                FormSetInstanceId = query.TryGetValue("FormSetInstanceId", out var formsInstanceId) && Guid.TryParse(formsInstanceId, out var fsid) ? fsid : Guid.NewGuid()
            };

            if (query.TryGetValue("ReturnUrl", out var returnUrl))
            {
                var uriBuilder = new UriBuilder(returnUrl!);
                var pathSegments = uriBuilder.Path.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();

                // Find if FormSetInstanceId is already present as a segment and update it, otherwise add it.
                var fsidString = metadata.FormSetInstanceId.ToString();
                var fsidIndex = pathSegments.FindIndex(s => Guid.TryParse(s, out _));
                if (fsidIndex >= 0)
                {
                    pathSegments[fsidIndex] = fsidString;
                }
                else
                {
                    pathSegments.Add(fsidString);
                }

                uriBuilder.Path = "/" + string.Join("/", pathSegments);
                metadata.ReturnUrl = uriBuilder.Uri.ToString();
            }

            // Determine the DigitalService based on the ReturnUrl content.
            var returnUrlLower = metadata.ReturnUrl?.ToLowerInvariant() ?? string.Empty;
            if (returnUrlLower.Contains("dro"))
                metadata.DigitalService = DigitalServiceType.Dro;
            else if (returnUrlLower.Contains("dcrs"))
                metadata.DigitalService = DigitalServiceType.Dcrs;
//            else
//                throw new InvalidOperationException("Return URL does not contain a valid digital service name.");

            return metadata;
        }

    }
}
