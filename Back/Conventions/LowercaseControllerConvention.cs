using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Back.Conventions;

public class LowercaseControllerConvention: IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        controller.ControllerName = controller.ControllerName.ToLower();
    }
}
