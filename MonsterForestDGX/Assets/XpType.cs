
public enum XpType
{
    Cast, Hit, Kill
}

static class XpTypeMethods
{
    public static int GetXp(this XpType xpType)
    {
        switch (xpType)
        {
            case XpType.Cast:
                return 1;
            case XpType.Hit:
                return 10;
            case XpType.Kill:
                return 50;
            default:
                return 0;
        }
     }
}
