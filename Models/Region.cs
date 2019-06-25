using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Alliance_for_Life.Models
{
    public enum GeoRegion
    {
        [Display(Name = "Region: 1")]
        Region1  = 1,
        [Display(Name = "Region: 2")]
        Region2 = 2,
        [Display(Name = "Region: 3")]
        Region3 = 3,
        [Display(Name = "Region: 4")]
        Region4 = 4,
        [Display(Name = "Region: 5")]
        Region5 = 5,
        [Display(Name = "Region: 6")]
        Region6 = 6,
        [Display(Name = "Region: 7")]
        Region7 = 7,
        [Display(Name = "Region: 8")]
        Region8 = 8,
        [Display(Name = "Region: 9")]
        Region9 = 9 
    }
}

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Name : enu.ToString();
    }

    public static string GetDescription(this Enum enu)
    {
        var attr = GetDisplayAttribute(enu);
        return attr != null ? attr.Description : enu.ToString();
    }

    private static DisplayAttribute GetDisplayAttribute(object value)
    {
        Type type = value.GetType();
        if (!type.IsEnum)
        {
            throw new ArgumentException(string.Format("Type {0} is not an enum", type));
        }

        // Get the enum field.
        var field = type.GetField(value.ToString());
        return field == null ? null : field.GetCustomAttribute<DisplayAttribute>();
    }
}