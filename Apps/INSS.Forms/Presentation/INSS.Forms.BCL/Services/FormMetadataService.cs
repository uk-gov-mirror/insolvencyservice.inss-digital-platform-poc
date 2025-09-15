using INSS.Forms.Domain.Models.Composite;
using INSS.Forms.Domain.Models.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace INSS.Forms.BCL.Services
{
    /// <inheritdoc />
    public class FormMetadataService : IFormMetadataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Provides metadata-related services for forms, including navigation and related operations.
        /// </summary>
        /// <param name="navigationManager">An instance of <see cref="NavigationManager"/> used to manage navigation within the application.</param>
        public FormMetadataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public FormMetadata? CreateFromQueryString()
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            var uri = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            var query = QueryHelpers.ParseQuery(request.QueryString.ToString());
            if(query.Count == 0)
            {
                return null;
            }

            var metadata = new FormMetadata
            {
                Username = query.TryGetValue("Username", out var username) && !StringValues.IsNullOrEmpty(username) ? username.ToString() : string.Empty,
                FormId = query.TryGetValue("Form", out var formId) && Guid.TryParse(formId, out var fid) ? fid : Guid.Empty,
                FormInstanceId = query.TryGetValue("FormInstanceId", out var formsInstanceId) && Guid.TryParse(formsInstanceId, out var fiid) ? fiid : Guid.NewGuid(),
                FormSetInstanceId = query.TryGetValue("FormSetInstanceId", out var formsSetInstanceId) && Guid.TryParse(formsSetInstanceId, out var fsid) ? fsid : Guid.NewGuid(),
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
            else
                metadata.DigitalService = DigitalServiceType.Unknown;

            return metadata;
        }

    }
}
