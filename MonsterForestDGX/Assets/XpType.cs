
public enum XpType
{
    Cast, Hit, Kill
}

static class XpTypeMethods
{
    public static int GetXp(this XpType xpType)
    {
        return xpType switch
        {
            XpType.Cast => 1,
            XpType.Hit => 10,
            XpType.Kill => 50,
            _ => 0,
        };
    }
}
