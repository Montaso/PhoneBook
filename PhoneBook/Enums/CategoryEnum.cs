using System.ComponentModel;

public enum CategoryEnum
    {
        [Description("Służbowy")]
        Business = 1,
        [Description("Prywatny")]
        Private = 2,
        [Description("Inny")]
        Other = 3
    }