using System;

namespace BlazorTestApp.Data
{
    public class AppState
    {
        public string CodeVerifier { get; set; }
        private string _accessToken;
        public string TokenResponse
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                NotifyStateHasChanged();
            }
        }

        private string _authorizationCode;

        public string AuthorizationCode
        {
            get => _authorizationCode;
            set
            {
                _authorizationCode = value;
                NotifyStateHasChanged();
            }
        }

        public event Action OnChange;
        private void NotifyStateHasChanged()
        {
            OnChange?.Invoke();
        }
    }
}
