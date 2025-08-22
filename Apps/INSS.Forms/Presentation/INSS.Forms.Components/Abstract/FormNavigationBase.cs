using INSS.Forms.Domain.Models.Abstract;
using INSS.Forms.Domain.Models.Forms;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace INSS.Forms.Components.Abstract
{
    /// <summary>
    /// Provides base functionality for paginated forms in Blazor, including navigation and page visibility logic.
    /// </summary>
    public abstract class FormNavigationBase : ComponentBase
    {
        private LastAction lastAction = LastAction.Next;

        [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
        [Inject] protected HttpClient HttpClient { get; set; } = default!;

        public enum LastAction
        {
            Back,
            Next
        }

        required public FormBase AllFormData
        {
            get; set;
        }

        /// <summary>
        /// Determines whether the specified page is currently visible.
        /// </summary>
        /// <param name="pageName">The name of the page to check.</param>
        /// <param name="forceHide">Optionally pass in logic that will force the page to be hidden if true.</param>
        /// <returns><c>true</c> if the page is currently visible; otherwise, <c>false</c>.</returns>
        public bool PageVisibility(string pageName, Func<bool>? forceHide = null)
        {
            int index = Array.IndexOf(PageNames, pageName);
            var visible = index == currentPageIndex;

            if (visible)
            {
                if (forceHide != null && forceHide())
                {
                    visible = false;

                    if (lastAction == LastAction.Back)
                    {
                        _ = Back();
                    }
                    else
                    {
                        _ = Next();
                    }
                }
            }

            return visible;
        }

        /// <summary>
        /// Gets an <see cref="EventCallback"/> that triggers the <see cref="Next"/> method.
        /// </summary>
        public EventCallback OnNext => EventCallback.Factory.Create(this, Next);

        /// <summary>
        /// Navigates to the next page or to a specific page if a page name is provided.
        /// If on the last page, navigates to a predefined URL.
        /// </summary>
        /// <param name="pageName">Optional. The name of the page to navigate to.</param>
        public async Task Next(string pageName = "")
        {
            lastAction = LastAction.Next;

            if (pageName.Length > 0)
            {
                int index = Array.IndexOf(PageNames, pageName);
                if (index >= 0)
                {
                    currentPageIndex = index;
                    await InvokeAsync(StateHasChanged);
                    return;
                }
            }

            if (currentPageIndex == PageNames.Length - 1)
            {

                bool success = await PostFormDataAsync("https://localhost:8001/forms").ConfigureAwait(false);

                if(success)
                {
                    NavigationManager?.NavigateTo(AllFormData.FormMetadata.ReturnUrl, forceLoad: true);
                }
            }
            if (currentPageIndex < PageNames.Length - 1)
            {
                currentPageIndex++;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Navigates to the next page in the sequence.
        /// </summary>
        public async Task Next()
        {
            await Next(string.Empty);
        }

        /// <summary>
        /// Navigates to the previous page in the sequence, if not already at the first page.
        /// </summary>
        public async Task Back()
        {
            lastAction = LastAction.Back;

            if (currentPageIndex > 0)
            {
                currentPageIndex--;
            }

            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// Gets or sets the array of page names representing the form pages.
        /// </summary>
        public string[] PageNames { get; set; } = [];

        /// <summary>
        /// Gets a value indicating whether the current page is the last page.
        /// </summary>
        public bool IsLastPage => currentPageIndex == PageNames.Length - 1;

        /// <summary>
        /// Gets a value indicating whether the current page is the first page.
        /// </summary>
        public bool IsFirstPage => currentPageIndex == 0;

        public bool IsCurrentPage(string pageName)
        {
            return PageVisibility(pageName);
        }

        public async Task<bool> PostFormDataAsync(string apiUrl)
        {
            if (AllFormData == null || string.IsNullOrWhiteSpace(apiUrl))
                return false;

            try
            {
                var serializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // <-- This enables camel case
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonStringEnumConverter() },
                    TypeInfoResolver = new DefaultJsonTypeInfoResolver
                    {
                        Modifiers =
                        {
                            typeInfo =>
                            {
                                if (typeInfo.Type == typeof(FormBase))
                                {
                                    typeInfo.PolymorphismOptions = new JsonPolymorphismOptions
                                    {
                                        DerivedTypes =
                                        {
                                            new JsonDerivedType(typeof(AboutYou), "AboutYou"),
                                            new JsonDerivedType(typeof(CompanyDetails), "CompanyDetails"),
                                            new JsonDerivedType(typeof(IndividualsDebts), "IndividualsDebts"),
                                            new JsonDerivedType(typeof(IndividualsIncome), "IndividualsIncome"),
                                        },
                                    };
                                }
                            }
                        }
                    }
                };

                var response = await HttpClient.PostAsJsonAsync(apiUrl, AllFormData, serializerOptions);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                // Optionally log or handle error
                return false;
            }
        }
        /// <summary>
        /// The index of the currently visible page.
        /// </summary>
        private int currentPageIndex = 0;
    }
}
