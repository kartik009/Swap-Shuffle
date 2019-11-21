using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SwapShuffle.Validator
{
    public class PasswordValidationBehavior :Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{5,}$";

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChange;
            base.OnAttachedTo(bindable);

        }

        void HandleTextChange(object sender,TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = Regex.IsMatch(e.NewTextValue, passwordRegex);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChange;
            base.OnDetachingFrom(bindable);
        }
    }
}
