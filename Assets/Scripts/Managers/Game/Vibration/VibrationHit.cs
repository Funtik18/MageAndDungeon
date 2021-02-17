using MoreMountains.NiceVibrations;

public class VibrationHit : Vibration
{
	public HapticTypes types;
	public override void Vibrate()
	{
		if(SaveLoadManager.IsVibration)
		{
			MMVibrationManager.Haptic(types);
		}
	}
}