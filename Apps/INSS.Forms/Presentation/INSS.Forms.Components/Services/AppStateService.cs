namespace INSS.Forms.Components.Services
{
    public class AppStateService
    {
        public AppState? Data { get; private set; }

        public void Set(AppState data)
        {
            Data = data;
        }
    }
}
