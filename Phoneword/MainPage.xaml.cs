using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System;

namespace Phoneword;

public partial class MainPage : ContentPage
{
    private string _translatedNumber = ""; 

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnTranslateClicked(object sender, EventArgs e)
    {
        string enteredText = PhoneNumberEntry.Text;
        if (!string.IsNullOrEmpty(enteredText))
        {
            _translatedNumber = ToNumber(enteredText);
            TranslatedLabel.Text = $"Номер: {_translatedNumber}";

            CallButton.IsEnabled = !string.IsNullOrWhiteSpace(_translatedNumber);
        }
        else
        {
            TranslatedLabel.Text = "Введите слово!";
            CallButton.IsEnabled = false;
        }
    }

    private async void OnCallClicked(object sender, EventArgs e)
    {
        if (PhoneDialer.Default.IsSupported)
        {
            PhoneDialer.Default.Open(_translatedNumber);
        }
        else
        {
            await DisplayAlert("Недоступно", "Звонки не поддерживаются на этом устройстве", "OK");
        }
    }

    private string ToNumber(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return "";

        raw = raw.ToUpperInvariant();
        var result = new System.Text.StringBuilder();

        foreach (char c in raw)
        {
            if (" -0123456789".Contains(c))
            {
                result.Append(c);
            }
            else
            {
                var resultChar = TranslateToNumber(c);
                result.Append(resultChar);
            }
        }
        return result.ToString();
    }

    private int TranslateToNumber(char letter)
    {
        if ("ABC".Contains(letter)) return 2;
        if ("DEF".Contains(letter)) return 3;
        if ("GHI".Contains(letter)) return 4;
        if ("JKL".Contains(letter)) return 5;
        if ("MNO".Contains(letter)) return 6;
        if ("PQRS".Contains(letter)) return 7;
        if ("TUV".Contains(letter)) return 8;
        if ("WXYZ".Contains(letter)) return 9;
        return 0;
    }
}