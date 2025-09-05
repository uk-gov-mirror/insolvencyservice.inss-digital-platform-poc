namespace INSS.Forms.Components.Services
{
    public class AppStateService
    {
        public AppState? Data { get; set; } = new AppState();

        public void Set(AppState data)
        {
            Data = data;
        }
    }
}
