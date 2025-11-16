using MudBlazor;

namespace PostOfficeFrontendProject__all_interactive.Service
{
    public class NotificationService
    {
        private readonly ISnackbar _snackbar;

        public NotificationService(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }

        public void ShowError(string message, int timeoutMs = 5000)
        {
            _snackbar.Add(message, Severity.Error, config =>
            {
                config.VisibleStateDuration = timeoutMs;  // مدت زمان نمایش (ms)
                config.ShowCloseIcon = true;  // دکمه بستن
                config.SnackbarVariant = Variant.Filled;  // optional: ظاهر پررنگ‌تر
            });
        }

        public void ShowSuccess(string message, int timeoutMs = 3000)
        {
            _snackbar.Add(message, Severity.Success, config =>
            {
                config.VisibleStateDuration = timeoutMs;
                config.SnackbarVariant = Variant.Filled;
            });
        }
    }
}
