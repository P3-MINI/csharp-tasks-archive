namespace Lab04.Models;

[Flags]
public enum Priority
{
    Standard = 0,
    Express = 1 << 0,
    Fragile = 1 << 1
}
