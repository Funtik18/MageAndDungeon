using MoreMountains.NiceVibrations;
public class VibrationButton : Vibration
{
	public override void Vibrate()
	{
		if(SaveLoadManager.IsVibration)
		{
			MMVibrationManager.Haptic(HapticTypes.Selection);
		}
	}
}
