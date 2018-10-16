namespace HardwareCheckoutSystemAdmin.Models
{
  // Level1 is admin with higher level permission. 
  // Level with small number includes levels with higher numbers
  public enum Permission
  {
    Other,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
  }
}