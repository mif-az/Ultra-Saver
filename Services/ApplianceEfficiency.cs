namespace Ultra_Saver;
struct ApplianceEfficiency
{
    public ApplianceEfficiency() { }
    public static float GetEfficiency(ApplianceType type){
        switch(type){
            case ApplianceType.GAS_STOVE: return 0.4f;
            case ApplianceType.INDUCTION_STOVE: return 0.84f;
            case ApplianceType.ELECTRIC_COIL_STOVE: return 0.74f;
            case ApplianceType.MICROWAVE: return 0.5f;
            case ApplianceType.OVEN: return 0.9f;
            default : return 1;

        }
    }
}

public enum ApplianceType{
    GAS_STOVE,
    INDUCTION_STOVE,
    ELECTRIC_COIL_STOVE,
    MICROWAVE,
    OVEN
}