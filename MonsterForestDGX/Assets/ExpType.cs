
public enum ExpType
{
    Cast, Hit, Kill
}

static class ExpTypeMethods
{
    public static int GetExp(this ExpType expType)
    {
        switch (expType)
        {
            case ExpType.Cast:
                return 1;
            case ExpType.Hit:
                return 10;
            case ExpType.Kill:
                return 50;
            default:
                return 0;
        }
     }
}
