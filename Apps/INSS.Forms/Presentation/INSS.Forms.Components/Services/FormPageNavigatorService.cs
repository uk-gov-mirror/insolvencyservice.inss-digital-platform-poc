using INSS.Forms.Components.Abstract;
using INSS.Forms.Domain.Models.Abstract;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace INSS.Forms.Components.Services
{
    /// <inheritdoc />
    public class FormPageNavigatorService : IFormPageNavigatorService
    {
        private LastAction lastAction = LastAction.Next;

        private readonly NavigationManager _navigationManager;
        private readonly IFormApiClient _apiClient;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormPageNavigatorService"/> class, providing navigation, API client, and
        /// configuration services.
        /// </summary>
        /// <param name="navigationManager">The <see cref="NavigationManager"/> instance used to manage navigation within the application.</param>
        /// <param name="apiClient">The <see cref="IFormApiClient"/> instance used to interact with external APIs.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> instance used to access application configuration settings.</param>
        public FormPageNavigatorService(NavigationManager navigationManager, IFormApiClient apiClient, IConfiguration configuration)
        {
            _navigationManager = navigationManager;
            _apiClient = apiClient;
            _configuration = configuration;
            OnPageChange = () => { };
            OnReadyToSave = () => Task.CompletedTask;
        }

        /// <summary>
        /// Represents the last navigational action performed.
        /// </summary>
        /// <remarks>This enumeration is used to indicate whether the last action was moving to the
        /// previous item or advancing to the next item in a sequence.</remarks>
        public enum LastAction
        {
            Previous,
            Next
        }

        /// <inheritdoc />
        public event Action OnPageChange;

        /// <inheritdoc />
        public event Func<Task> OnReadyToSave;

        /// <inheritdoc />
        public List<string> PageNames { get; set; } = new List<string>();

        /// <inheritdoc />
        public int CurrentPageIndex { get; set; } = 0;

        /// <inheritdoc />
        public bool IsFirstPage => CurrentPageIndex == 0;

        /// <inheritdoc />
        public bool IsLastPage => PageNames.Count > 0 && CurrentPageIndex == PageNames.Count - 1;

        /// <inheritdoc />
        public string CurrentPageName => PageNames[CurrentPageIndex];

        /// <inheritdoc />
        public bool IsCurrentPage(string pageName)
        {
            return PageVisibility(pageName);
        }

        /// <inheritdoc />
        public bool PageVisibility(string pageName, bool forceHide = false)
        {
            var index = PageNames.IndexOf(pageName);
            var visible = index == CurrentPageIndex;

            if (visible)
            {
                if (forceHide)
                {

                    visible = false;

                    if (lastAction == LastAction.Previous)
                    {
                        PreviousPage();
                    }
                    else
                    {
                        NextPage();
                    }
                }
            }

            return visible;
        }

        /// <inheritdoc />
        public void PreviousPage()
        {
            lastAction = LastAction.Previous;

            if (CurrentPageIndex > 0)
            {
                CurrentPageIndex--;
                OnPageChange.Invoke();
            }
        }

        /// <inheritdoc />
        public void NextPage(string pageName = "")
        {
            lastAction = LastAction.Next;

            if (pageName.Length > 0)
            {
                var index = PageNames.IndexOf(pageName);
                if (index >= 0)
                {
                    CurrentPageIndex = index;
                    OnPageChange.Invoke();

                    return;
                }
            }

            if (CurrentPageIndex < PageNames.Count - 1)
            {
                CurrentPageIndex++;
            }

            OnPageChange.Invoke();
        }


        /// <inheritdoc />
        public void NextPage()
        {
            NextPage(string.Empty);
        }

        public async Task SubmitFormAsync()
        {
            await OnReadyToSave.Invoke();
        }
    }
}
