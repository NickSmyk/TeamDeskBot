using System.ComponentModel;
using System.Reflection;
using TeamDeskBot.Services;

namespace TeamDeskBot.Models.Interactions;

public class EditUserInteraction : BaseInteraction
{
    private User User { get; }
    private readonly ApiRequestsService _apiRequestsService;
    private string FieldName { get; set; }

    public EditUserInteraction(ApiRequestsService apiRequestsService, User user)
    {
        _apiRequestsService = apiRequestsService;
        this.User = user;
    }

    //TODO: WORK -> don't forget to fix it
    public override string FinishInteraction()
    {
        //_apiRequestsService.AddUser(this.Result).GetAwaiter().GetResult();
        return "User edited";
    }

    protected override void InitStages()
    {
        AddNewStage(text => this.FieldName = text, "field to edit");
        AddNewStage(EditField, "New value");
    }

    private void EditField(string value)
    {
        PropertyInfo? property = this.User.GetType().GetProperty(this.FieldName);
        if (property is null)
        {
            return;
        }
        
        Type type = property.PropertyType;
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        object? newValue = converter.ConvertFromString(value);
        property?.SetValue(this.User, newValue, null);
    }

}