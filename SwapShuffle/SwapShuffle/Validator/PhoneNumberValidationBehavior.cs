using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SwapShuffle.Validator
{
    public class PhoneNumberValidationBehavior :Behavior<Entry>
    {
        Regex rx1 = new Regex("^[1-9]{10}");

        const string reg = @"^[1-9]{10}";
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChange;
            base.OnAttachedTo(bindable);

        }

        void HandleTextChange(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = Regex.IsMatch(e.NewTextValue, reg);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged += HandleTextChange;
            base.OnDetachingFrom(bindable);
        }
    }
}
