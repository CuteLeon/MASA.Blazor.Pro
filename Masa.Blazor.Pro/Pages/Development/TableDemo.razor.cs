namespace Masa.Blazor.Pro.Pages.Development;

public partial class TableDemo : ProCompontentBase
{
    protected override void OnInitialized()
    {
        MasaBlazor.Breakpoint.OnUpdate += OnPropertyChanged;
        MasaBlazor.Application.PropertyChanged += OnPropertyChanged;
    }

    private Task OnPropertyChanged()
    {
        if (NavHelper.CurrentUri.EndsWith("development/tabledemo"))
        {
            InvokeAsync(StateHasChanged);
        }
        return Task.CompletedTask;
    }

    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        OnPropertyChanged();
    }

    public void Dispose()
    {
        MasaBlazor.Breakpoint.OnUpdate -= OnPropertyChanged;
        MasaBlazor.Application.PropertyChanged -= OnPropertyChanged;
    }
}
