
// using Ultra_Saver.Models;

// namespace Ultra_Saver;

// public static class RecipeCostCalculator
// {
//     private static readonly IEnergyCostAlgorithm _algorithm;

//     public static RecipeCostCalculator(IEnergyCostAlgorithm algorithm) => _algorithm = algorithm;

//     public static double GetTotalPrice(UserModel user)
//     {
//         if (user.ElectricityPrice > 0)
//         {
//             return _algorithm.TotalEnergy * user.ElectricityPrice;
//         }
//         else if (user.GasPrice > 0)
//         {
//             return _algorithm.GasVolumeUsed * user.GasPrice;
//         }
//         else
//         {
//             return _algorithm.TotalEnergy;
//         }
//     }
// }