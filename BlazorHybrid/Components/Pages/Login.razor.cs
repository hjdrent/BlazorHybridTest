using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using Microsoft.Maui.Storage; // Add this for SecureStorage

#if __ANDROID__
using BlazorHybrid.Services;
#endif

namespace BlazorHybrid.Components.Pages
{
    public partial class Login
    {
        [Inject] private IPublicClientApplication PublicClientApp { get; set; }

#if __ANDROID__
        [Inject] private IActivityProvider activityProvider { get; set; }
#endif
        private string userName;
        private bool isAuthenticated;

        private async Task SignIn()
        {
            try
            {
#if WINDOWS
                // Clear any existing accounts before signing in
                var accounts = await PublicClientApp.GetAccountsAsync();
                foreach (var account in accounts)
                {
                    await PublicClientApp.RemoveAsync(account);
                }

                // TODO: chrome://net-internals/#hsts to clear HSTS settings => Delete domain security policies => localhost
                // Proceed with interactive sign-in
                var authResult = await PublicClientApp
                    .AcquireTokenInteractive(new[] { "User.Read" })
                    .WithPrompt(Prompt.SelectAccount)
                    .ExecuteAsync();

                userName = authResult.Account.Username;
                isAuthenticated = true;
                // Store the token securely
                await SecureStorage.SetAsync("authToken", authResult.AccessToken);
#endif
#if __ANDROID__
                // MainThread vanwege Sign-in error code 50199
                // Failure reason For security reasons, user confirmation is required for this request.
                // Please repeat the request allowing user interaction.
                // op Azure
                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    // TESTJE
                    var acts = activityProvider.GetAllActivities();
                    foreach (var act in acts)
                    {
                        Console.WriteLine($"Activity: {act.LocalClassName}");
                    }

                    // TODO: Deactivate Automatisch Blokkeren en activate USB Debugging (USB foutopsporing) on Android device
                    // Clear any existing accounts before signing in
                    var accounts = await PublicClientApp.GetAccountsAsync();
                    foreach (var account in accounts)
                    {
                        await PublicClientApp.RemoveAsync(account);
                    }

                    var authResult = await PublicClientApp
                        .AcquireTokenInteractive(new[] { "User.Read" })
                        .WithPrompt(Prompt.SelectAccount)
                        .WithParentActivityOrWindow(() =>
                            activityProvider.GetCurrentActivity()) // Make sure this is correctly implemented
                        .ExecuteAsync();

                    if (authResult != null)
                    {
                        // User is authenticated
                        var username = authResult.Account.Username;
                        Console.WriteLine($"User {username} has been authenticated.");
                        // You can now store user details or navigate to a different page in your app
                        isAuthenticated = true;
                        // Store the token securely
                        await SecureStorage.SetAsync("authToken", authResult.AccessToken);
                    }
                });
#endif
            }
            catch (MsalUiRequiredException ex)
            {
                Console.WriteLine($"MSAL error: {ex.Message}");
                // Handle the case where user interaction is needed
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Authentication error: {ex.Message}");
                // Handle other exceptions
            }
        }

        private async Task SignOut()
        {
            var accounts = await PublicClientApp.GetAccountsAsync();
            foreach (var account in accounts)
            {
                await PublicClientApp.RemoveAsync(account);
            }

            isAuthenticated = false;
            userName = string.Empty;
        }
    }
}
