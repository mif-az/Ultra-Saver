namespace Ultra_Saver;
public struct ApplianceEfficiency
{
    public static float GetEfficiency(ApplianceType Type)
    {
        switch (Type)
        {
            case ApplianceType.GAS_STOVE: return 0.4f;
            case ApplianceType.INDUCTION_STOVE: return 0.84f;
            case ApplianceType.ELECTRIC_COIL_STOVE: return 0.74f;
            case ApplianceType.MICROWAVE: return 0.5f;
            case ApplianceType.OVEN: return 0.9f;
            default: return -1;

        }
    }
}

public enum ApplianceType
{
    GAS_STOVE,
    INDUCTION_STOVE,
    ELECTRIC_COIL_STOVE,
    MICROWAVE,
    OVEN
}