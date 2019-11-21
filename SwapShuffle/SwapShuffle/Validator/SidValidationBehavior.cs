using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SwapShuffle.Validator
{
    public class SidValidationBehavior : Behavior<Entry>
    {
        const string Reg = @"^[1-9]{9}";



        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChange;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChange(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = Regex.IsMatch(e.NewTextValue, Reg);
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;

        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChange;
            base.OnDetachingFrom(bindable);
        }
    }
}
