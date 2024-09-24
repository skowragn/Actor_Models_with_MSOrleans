using System.ComponentModel;

namespace CarsManager.Orleans.Web.Model;
public enum CarCategoryEnumViewModel
{
    [Description("Hatchback Car")]
    Hatchback = 1,
    [Description("SUV Car")]
    SUV = 2,
    [Description("Sport Car")]
    Sport = 3,
    [Description("Other")]
    Other = 4
}
